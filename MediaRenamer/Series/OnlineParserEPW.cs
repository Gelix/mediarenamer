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

namespace MediaRenamer.Series {
    /// <summary>
    /// Zusammenfassung für OnlineParser.
    /// </summary>
    public class OnlineParserEPW : OnlineParserBase {
        //																		id season year language
        private String detailUrl = "http://www.episodeworld.com/tools/mediarenamer/{0}/{1}/{2}/{3}/strict";
        //																		name season
        private String queryUrl = "http://www.episodeworld.com/tools/mediarenamer/{0}/{1}/";

        public static String parserName = "EpisodeWorld.com";

        override public bool getSeriesData(ref showClass show, ref Episode ep) {
            WebClient cli = new WebClient();
            XmlDocument xml = new XmlDocument();

            if (!File.Exists(episodeCache)) {
                if (show.ID != null && show.Year > 0) {
                    //I know which series - just download it
                    cli.DownloadFile(String.Format(detailUrl, show.ID, ep.season, show.Year, show.Lang),
                        episodeCache);
                    xml.Load(episodeCache);
                }
                else {
                    // Search for series
                    cli.DownloadFile(String.Format(queryUrl, ep.series, ep.season), searchCache);
                    xml.Load(searchCache);

                    List<showClass> shows = new List<showClass>();
                    XmlNodeList nodes;
                    // Shows found on episodeworld.com
                    if (xml.DocumentElement.Name == "search") {
                        nodes = xml.GetElementsByTagName("found");
                        foreach (XmlNode node in nodes) {
                            showClass sc = new showClass();
                            sc.ID = node.Attributes["id"].Value;
                            sc.Name = node.Attributes["name"].Value;
                            sc.Year = Int32.Parse(node.Attributes["year"].Value);
                            shows.Add(sc);
                        }

                        if (ep.series != ep.altSeries) {
                            // Check altSeries list as well
                            cli.DownloadFile(String.Format(queryUrl, ep.altSeries, ep.season), searchCache);
                            xml.Load(searchCache);

                            if ((xml.DocumentElement.ChildNodes.Count > 0) &&
                            (xml.DocumentElement.Name == "search")) {
                                nodes = xml.GetElementsByTagName("found");
                                foreach (XmlNode node in nodes) {
                                    showClass sc = new showClass();
                                    sc.ID = node.Attributes["id"].Value;
                                    sc.Name = node.Attributes["name"].Value;
                                    sc.Year = Int32.Parse(node.Attributes["year"].Value);
                                    shows.Add(sc);
                                }
                            }
                        }
                    }

                    if (xml.DocumentElement.Name == "series") {
                        // Found series directly.
                        FileInfo fi = new FileInfo(searchCache);
                        fi.MoveTo(episodeCache);
                    }
                    else {
                        show = chooseSeries(ep, shows);
                        if (show != null) {
                            cli.DownloadFile(
                                String.Format(detailUrl, show.ID, ep.season, show.Year, show.Lang), episodeCache
                                );
                        }
                        shows.Clear();
                    }
                }
            }

            // Check if cache is outdated
            if (File.Exists(episodeCache)) {
                DateTime dt = File.GetLastWriteTime(episodeCache);
                if (DateTime.Now.Subtract(dt).TotalDays > 3) {
                    // Log.Add( i18n.t( "oparse_older", ep.series) );
                    File.Delete(episodeCache);
                    cli.DownloadFile(String.Format(detailUrl, show.ID, ep.season, show.Year, show.Lang),
                        episodeCache);
                }
                if (File.Exists(episodeCache)) {
                    xml.Load(episodeCache);
                }

                if (xml.DocumentElement.Attributes.Count > 1) {
                    if (show.Lang == "") {
                        List<String> languages = new List<String>();
                        XmlNodeList nodes = xml.GetElementsByTagName("episode");
                        foreach (XmlNode node in nodes) {
                            String lang = node.Attributes["language"].Value;
                            if (!languages.Contains(lang)) {
                                languages.Add(lang);
                            }
                        }

                        if (languages.Count > 1) {
                            List<showClass> shows = new List<showClass>();
                            foreach (String lang in languages) {
                                showClass showTemp = new showClass();
                                showTemp.ID = xml.DocumentElement.Attributes["id"].Value;
                                showTemp.Name = xml.DocumentElement.Attributes["name"].Value;
                                showTemp.Year = Int32.Parse(xml.DocumentElement.Attributes["year"].Value);
                                showTemp.Lang = lang;
                                shows.Add(showTemp);
                            }
                            show = chooseSeries(ep, shows);
                        }
                        else {
                            show.ID = xml.DocumentElement.Attributes["id"].Value;
                            show.Name = xml.DocumentElement.Attributes["name"].Value;
                            show.Year = Int32.Parse(xml.DocumentElement.Attributes["year"].Value);
                            show.Lang = languages[0];
                        }
                    }
                    ep.language = show.Lang;
                    ep.series = show.Name;
                }
            }

            cli.Dispose();

            if (ep.language == null || ep.language == "")
                ep.language = "english";

            // Find title for episode
            XmlNodeList episodes = xml.GetElementsByTagName("episode");
            if (episodes.Count > 0) {
                bool foundEp = false;
                // Search episode
                foreach (XmlNode node in episodes) {
                    if ((node.Attributes["season"].Value == ep.season.ToString()) &&
                         (node.Attributes["episode"].Value == ep.episode.ToString()) &&
                         (node.Attributes["language"].Value == ep.language.ToString()) &&
                         (node.Attributes["special"].Value == "false")
                        ) {
                        foundEp = true;
                        ep.title = node.Attributes["title"].Value;
                        break;
                    }
                }

                if (!foundEp) {
                    // Not found - maybe special episode after all ?
                    foreach (XmlNode node in episodes) {
                        if ((node.Attributes["season"].Value == ep.season.ToString()) &&
                             (node.Attributes["episode"].Value == ep.episode.ToString()) &&
                             (node.Attributes["language"].Value == ep.language.ToString()) &&
                             (node.Attributes["special"].Value == "true")
                            ) {
                            foundEp = true;
                            ep.title = node.Attributes["title"].Value;
                            break;
                        }
                    }
                }

                if (foundEp) {
                    if (ep.episodes.Length > 1) {
                        if (ep.title.EndsWith(")")) {
                            ep.title = ep.title.Substring(0, ep.title.LastIndexOf("("));
                        }
                    }
                    ep.title = ep.title.Replace(".i.", "");
                    if (ep.title.IndexOf("aka") > 0) {
                        ep.title = Eregi.replace("\\(aka([^)]*)\\)", "", ep.title);
                    }
                    return true;
                }
            }

            return false;
            // Done
        }
    }
}
