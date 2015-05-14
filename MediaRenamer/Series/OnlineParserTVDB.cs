/**
 * Copyright 2009 Benjamin Schirmer
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Win32;
using MediaRenamer;
using MediaRenamer.Common;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using Ionic.Utils.Zip;
using System.Threading;

namespace MediaRenamer.Series {
    /// <summary>
    /// Zusammenfassung für OnlineParser.
    /// </summary>
    public class OnlineParserTVDB : OnlineParserBase {
        private const String TVDBMirrors = "TVDB.mirrors";
        private const String TVDBLanguages = "TVDB.languages";

        //                         Mirror                           ID   Language
        private String detailUrl = "{0}/api/8D6EACA70DB47D29/series/{1}/all/{2}.zip";
        //                                                                           Series
        private String queryUrl = "http://www.thetvdb.com/api/GetSeries.php?seriesname={0}&language=all";
        private String mirrorUrl = "http://www.thetvdb.com/api/8D6EACA70DB47D29/mirrors.xml";
        private String languageUrl = "http://www.thetvdb.com/api/8D6EACA70DB47D29/languages.xml";

        private String zipCache = "";

        private List<String> mirrors;
        private Hashtable languages;

        public static String parserName = "TheTVDB.com";

        public OnlineParserTVDB() {
            zipCache = String.Format(baseCache, "compressed", "data");
            zipCache = zipCache.Replace(".xml", ".zip");
            Object data;
            data = Settings.GetValueAsObject<List<String>>(TVDBMirrors);
            if (data != null)
                mirrors = (List<String>)data;
            else {
                mirrors = new List<String>();
                this.loadMirrors();
            }

            data = Settings.GetValueAsObject<Hashtable>(TVDBLanguages);
            if (data != null)
                languages = (Hashtable)data;
            else {
                languages = new Hashtable();
                this.loadLanguages();
            }
        }

        private void storeInternalData() {
            Settings.SetValue(TVDBMirrors, mirrors);
            Settings.SetValue(TVDBLanguages, languages);
        }

        private void loadMirrors() {
            /**
             * <Mirrors>
             *   <Mirror>
             *     <id>1</id>
             *     <mirrorpath>http://thetvdb.com</mirrorpath>
             *     <typemask>7</typemask>
             *   </Mirror>
             * </Mirrors>
             */
            String mirrorFile = String.Format(baseCache, "TVDB", "mirror");

            WebClient cli = new WebClient();
            cli.DownloadFile(mirrorUrl, mirrorFile);

            XmlDocument xml = new XmlDocument();
            xml.Load(mirrorFile);
            XmlNodeList nodes = xml.DocumentElement.ChildNodes;
            foreach (XmlNode node in nodes) {
                String mirror = "";
                Int32 typemask = 0;
                foreach (XmlNode subnode in node.ChildNodes) {
                    if (subnode.Name == "typemask") {
                        typemask = Int32.Parse(subnode.InnerText);
                    }
                    if (subnode.Name == "mirrorpath") {
                        mirror = subnode.InnerText;
                    }
                }
                if ((typemask | 1) == typemask) {
                    mirrors.Add(mirror);
                }
            }

            // Backup if no mirrors found.
            if (mirrors.Count == 0) {
                mirrors.Add("http://thetvdb.com");
            }

            this.storeInternalData();
        }

        private void loadLanguages() {
            /**
             * <Languages>
             *   <Language>
             *     <name>Deutsch</name>
             *     <abbreviation>de</abbreviation>
             *     <id>14</id>
             *   </Language>
             *   [...]
             * </Languages>
             */
            String languageFile = String.Format(baseCache, "TVDB", "languages");

            WebClient cli = new WebClient();
            cli.DownloadFile(languageUrl, languageFile);

            XmlDocument xml = new XmlDocument();
            xml.Load(languageFile);
            XmlNodeList nodes = xml.DocumentElement.ChildNodes;
            foreach (XmlNode node in nodes) {
                String language = "";
                String abbreviation = "";
                foreach (XmlNode subnode in node.ChildNodes) {
                    if (subnode.Name == "name") {
                        language = subnode.InnerText;
                    }
                    if (subnode.Name == "abbreviation") {
                        abbreviation = subnode.InnerText;
                    }
                }
                languages.Add(language, abbreviation);
            }

            this.storeInternalData();
        }

        private String randomMirror() {
            Random rand = new Random();
            int randNum = rand.Next(0, mirrors.Count);
            return mirrors[randNum];
        }

        override public bool getSeriesData(ref showClass show, ref Episode ep) {
            XmlDocument xml = new XmlDocument();
            String url = "";

            episodeCache = String.Format(baseCache, seriesHash, "all");

            // Fix if older information is received
            if (show.Lang.Length > 2) {
                show.Lang = (String)languages[show.Lang.ToString()];
            }

            if (show.ID == String.Empty) {
                File.Delete(episodeCache);
            }

            if (!File.Exists(episodeCache)) {
                if (show.ID != String.Empty) {
                    //I know which series - just download it
                    url = String.Format(detailUrl, randomMirror(), show.ID, show.Lang);
                    downloadAndExtract(url, show.Lang);
                    xml.Load(episodeCache);
                }
                else {
                    // Search for series
                    xml.LoadXml(searchQuery(ep.series));

                    List<showClass> shows = new List<showClass>();
                    XmlNodeList nodes = xml.GetElementsByTagName("Series");
                    // Shows found on thetvdb.com
                    if (nodes.Count != 1) {
                        foreach (XmlNode node in nodes) {
                            showClass sc = new showClass();
                            sc.ID = node.SelectSingleNode("seriesid").InnerText;
                            sc.Name = node.SelectSingleNode("SeriesName").InnerText;
                            if (node.SelectSingleNode("FirstAired") == null)
                                continue;
                            sc.Year = Int32.Parse(node.SelectSingleNode("FirstAired").InnerText.Substring(0, 4));
                            sc.Lang = node.SelectSingleNode("language").InnerText;
                            shows.Add(sc);
                        }

                        // Check altSeries list as well
                        if (ep.series != ep.altSeries) {
                            xml.LoadXml(searchQuery(ep.altSeries));

                            nodes = xml.GetElementsByTagName("Series");

                            if (nodes.Count > 1) {
                                foreach (XmlNode node in nodes) {
                                    showClass sc = new showClass();
                                    sc.ID = node.SelectSingleNode("seriesid").InnerText;
                                    sc.Name = node.SelectSingleNode("SeriesName").InnerText;
                                    if (node.SelectSingleNode("FirstAired") != null) {
                                       sc.Year = Int32.Parse(node.SelectSingleNode("FirstAired").InnerText.Substring(0, 4));
                                    }
                                    sc.Lang = node.SelectSingleNode("language").InnerText;
                                    shows.Add(sc);
                                }
                            }
                        }
                    }

                    if (nodes.Count != 1 && shows.Count == 0) {
                        while (true) {
                            InputDialog input = new InputDialog("Couldn't find the matching series - please enter a valid series name:", Application.ProductName, ep.series);
                            if (input.ShowDialog() == DialogResult.OK) {
                                String seriesName = input.value;
                                xml.LoadXml(searchQuery(seriesName));
                                nodes = xml.GetElementsByTagName("Series");
                                if (nodes.Count > 0) {
                                    foreach (XmlNode node in nodes) {
                                        showClass sc = new showClass();
                                        sc.ID = node.SelectSingleNode("seriesid").InnerText;
                                        sc.Name = node.SelectSingleNode("SeriesName").InnerText;
                                        if (node.SelectSingleNode("FirstAired") == null)
                                            continue;
                                        sc.Year = Int32.Parse(node.SelectSingleNode("FirstAired").InnerText.Substring(0, 4));
                                        sc.Lang = node.SelectSingleNode("language").InnerText;
                                        shows.Add(sc);
                                    }
                                    break;
                                }
                            }
                            else {
                                break;
                            }
                        }
                    }

                    if (nodes.Count == 1) {
                        // Found series directly.
                        show.ID = xml.SelectSingleNode("//Data/Series/seriesid").InnerText;
                        show.Name = xml.SelectSingleNode("//Data/Series/SeriesName").InnerText;
                        XmlNode showNode = xml.SelectSingleNode("//Data/Series/FirstAired");
                        if (showNode != null) {
                            show.Year = Int32.Parse(showNode.InnerText.Substring(0, 4));
                        }
                        show.Lang = xml.SelectSingleNode("//Data/Series/language").InnerText;
                        shows.Add(show);
                    }

                    show = chooseSeries(ep, shows);
                    if (show != null) {
                        ep.language = show.Lang;
                        ep.series = show.Name;
                        url = String.Format(detailUrl, randomMirror(), show.ID, ep.language);
                        downloadAndExtract(url, show.Lang);
                    }
                    shows.Clear();
                }
            }

            // Check if cache is available
            if (File.Exists(episodeCache)) {
                DateTime dt = File.GetLastWriteTime(episodeCache);
                // Check if cache is outdated
                if (DateTime.Now.Subtract(dt).TotalDays > 5) {
                    try {
                        url = String.Format(detailUrl, randomMirror(), show.ID, show.Lang);
                        downloadAndExtract(url, show.Lang);
                    }
                    catch (Exception E) {
                        Thread.Sleep(1000);
                        #if DEBUG
                        Log.Error("Could not download updated episode file for " + ep.ToString(), E);
                        #endif
                    }
                }
                if (File.Exists(episodeCache)) {
                    xml.Load(episodeCache);
                }
            }

            if (show == null) return false;
            if (ep.language == null || ep.language == "")
                if (show != null ) ep.language = show.Lang;

            show.Name = xml.SelectSingleNode("//Data/Series/SeriesName").InnerText;
            show.ID = xml.SelectSingleNode("//Data/Series/id").InnerText;
            XmlNode yearNode = xml.SelectSingleNode("//Data/Series/FirstAired");
            if (yearNode != null && yearNode.InnerText != String.Empty && yearNode.InnerText.Length >= 4) {
                show.Year = Int32.Parse(yearNode.InnerText.Substring(0, 4));
            }

            // Find episode/season for title


            // Find title for episode
            XmlNodeList episodes = xml.GetElementsByTagName("Episode");
            if (episodes.Count > 0) {
                // Search episode
                foreach (XmlNode episodeXml in episodes) {
                    String tempString = "";
                    Int32 season = Int32.Parse(episodeXml.SelectSingleNode("SeasonNumber").InnerText);
                    Int32 episode = Int32.Parse(episodeXml.SelectSingleNode("EpisodeNumber").InnerText);
                    Int32 altEpisode = 0;
                    tempString= episodeXml.SelectSingleNode("absolute_number").InnerText;
                    if (!String.IsNullOrEmpty(tempString))
                    {
                        altEpisode = Int32.Parse(tempString);
                    }
                    string title = episodeXml.SelectSingleNode("EpisodeName").InnerText;
                    if (ep.episodes.Length > 1)
                    {
                        if (title.EndsWith(")"))
                        {
                             title =  title.Substring(0,  title.LastIndexOf("("));
                        }
                    }
                     title =  title.Replace(".i.", "");
                    if (title.IndexOf("aka") > 0)
                    {
                        title = Eregi.replace("\\(aka([^)]*)\\)", "", title);
                    }
                    if (string.IsNullOrEmpty(ep.title))
                    {
                        if (ep.episode == episode && ep.season == season && ep.altEpisode == 0)
                        {
                            ep.title = title.Replace(":", "_").Replace("*", "_").Replace("?", "_").Replace("/", "_");
                            return true;
                        }
                    }

                    if (checkTitle(ep.title, title))
                    {
                        ep.title = title.Replace(":", "_").Replace("*", "_").Replace("?", "_").Replace("/", "_");
                        ep.episode = episode;
                        ep.season=season;
                        return true;
                    }
                    if (ep.altEpisode == altEpisode && ep.altEpisode != 0 )
                    {
                        ep.episode = episode;
                        ep.title = title;
                        ep.season = season;
                        return true;
                  
                    }
                }
            }

            return false;
            // Done
        }
        private bool checkTitle(string title1, string title2)
        {
            if (String.IsNullOrEmpty(title1) || string.IsNullOrEmpty(title2)) return false;

            string temptitle = title2.Replace(":", "_").Replace("*", "_").Replace(" ", "").Replace("?", "_").Replace("/", "_").Replace("-", "").Replace("'", "").Replace("!", "").Replace(",", "").Replace("(", "").Replace(")", "").ToLowerInvariant();
            string temptitle2 = title1.Replace(" ", "").Replace("-", "_").Replace("'", "_").Replace(",", "").ToLowerInvariant();
            if (!temptitle2.Contains("_")) temptitle = temptitle.Replace("_", "");
                    if (temptitle2 != temptitle)
                    {
                        temptitle = title2.Replace(" ", "").TrimEnd(' ').Replace("&", "and").ToLowerInvariant();
                    }
                    if (temptitle2 != temptitle)
                    {
                        temptitle = title2.Replace(" ", "").TrimEnd(' ').Replace("and", "&").ToLowerInvariant();
                    }
                    if (temptitle2 != temptitle)
                    {
                        temptitle = title2.Replace(" ", "").TrimEnd(' ').Replace("and", "&").ToLowerInvariant();
                    }
                    if (temptitle2 == temptitle)
                    {
                        return true;
                    }
                    if (title1.ToLowerInvariant().StartsWith("the")) {
                        if (!title2. ToLowerInvariant().StartsWith("the"))
                        {
                            title1 = title1.Substring(3);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    temptitle = title2.Replace(":", "_").Replace("*", "_").Replace(" ", "").Replace("?", "_").Replace("/", "_").Replace("-", "_").Replace("'", "_").Replace("!", "").Replace(",", "").ToLowerInvariant();
                    temptitle2 = title1.Replace(" ", "").Replace("-", "_").Replace("'", "_").Replace(",","").ToLowerInvariant();
                    if (!temptitle2.Contains("_")) temptitle = temptitle.Replace("_", "");
                    if (temptitle2 != temptitle)
                    {
                        temptitle = title2.Replace(" ", "").TrimEnd(' ').Replace("&", "and").ToLowerInvariant();
                    }
                    if (temptitle2 != temptitle)
                    {
                        temptitle = title2.Replace(" ", "").TrimEnd(' ').Replace("and", "&").ToLowerInvariant();
                    }
                    if (temptitle2 == temptitle)
                    {
                        return true;
                    }
                    return false;
        }


        private String searchQuery(string query) {
            lock (OnlineParserTVDB.parserName) {
                WebClient cli = new WebClient();
                query = Uri.EscapeDataString(query);
                String data = cli.DownloadString(String.Format(queryUrl, query));
                cli.Dispose();
                return data;
            }
        }

        private void downloadAndExtract(string url, string lang) {
            lock (OnlineParserTVDB.parserName) {
                WebClient cli = new WebClient();
                url = Uri.EscapeUriString(url);
                cli.DownloadFile(url, zipCache);
                cli.Dispose();
                extractXml(lang);
            }
        }

        private void extractXml(String lang) {
            ZipFile zipFile = ZipFile.Read(zipCache);
            File.Delete(episodeCache);
            foreach (ZipEntry entry in zipFile) {
                if (entry.FileName == lang + ".xml") {
                    FileStream stream = File.OpenWrite(episodeCache);
                    entry.Extract(stream);
                    stream.Close();
                }
            }
        }
    }
}
