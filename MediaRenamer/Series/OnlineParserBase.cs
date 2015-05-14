using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using MediaRenamer.Common;
using System.Text.RegularExpressions;

namespace MediaRenamer.Series {
    abstract public class OnlineParserBase {
        internal String cache = null;
        internal Hashtable seriesList;

        internal String seriesHash;
        internal String baseCache;
        internal String episodeCache;
        internal String searchCache;

        private String parserName = "GenericParser";
        private String parserDataCache = "";

        public OnlineParserBase() {
            this.initParser();
        }

        private void initParser() {
            parserName = Settings.GetValueAsString(SettingKeys.SeriesParser);

            String cacheDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                @"\" + Application.ProductName + @"\series\" + parserName + @"\";
            if (!Directory.Exists(cacheDir)) Directory.CreateDirectory(cacheDir);
            baseCache = cacheDir + "{0}_{1}.xml";
            parserDataCache = parserName + ".seriesdata";

            searchCache = String.Format(baseCache, "search", "data");

            Object table = Settings.GetValueAsObject<Hashtable>(parserDataCache);
            if (table != null) {
                seriesList = (Hashtable)table;
            }
            else {
                seriesList = new Hashtable();
            }
        }

        abstract public bool getSeriesData(ref showClass show, ref Episode ep);

        internal showClass chooseSeries(Episode ep, List<showClass> shows) {
            if (shows.Count == 0) return null;
            if (shows.Count == 1) {
                return shows[0];
            }
            SelectShow showDlg = new SelectShow();

            showDlg.Text = String.Format("Select series for {0}", ep.series);
            showDlg.setEpisodeData(ep);
            showDlg.addShows(shows);

            if (showDlg.ShowDialog() == DialogResult.OK) {
                return showDlg.selectedShow;
            }
            else {
                return null;
            }

        }
       public string FindAbsID(ref Episode ep) {
             try {
                showClass show = new showClass();
                show.Name = ep.series;
                String seriesOld = ep.series.ToLower();
                String title = "";

                if (seriesList.ContainsKey(seriesOld)) {
                    show = (showClass)seriesList[seriesOld];
                    ep.series = show.Name;
                }
                else if (seriesList.ContainsKey(ep.altSeries.ToLower())) {
                    show = (showClass)seriesList[ep.altSeries.ToLower()];
                    ep.series = show.Name;
                }
                else {
                    // No data found yet.
                }
                seriesHash = MD5.createHash(show.Name);
                episodeCache = String.Format(baseCache, seriesHash, ep.season);

                if (ep.special) return title;

                bool hr = getSeriesData(ref show, ref ep);

                if (show != null && show.ID != "") {
                    bool listChanged = false;
                    if (!seriesList.ContainsKey(ep.series.ToLower())) {
                        seriesList.Add(ep.series.ToLower(), show);
                        listChanged = true;
                    }
                    if (!seriesList.ContainsKey(seriesOld)) {
                        seriesList.Add(seriesOld, show);
                        listChanged = true;
                    }
                    if (!seriesList.ContainsKey(show.Name.ToLower())) {
                        seriesList.Add(show.Name.ToLower(), show);
                        listChanged = true;
                    }
                    if (listChanged)
                        Settings.SetValue(parserDataCache, seriesList);
                }
            

            return title;
             }
                                   
           catch (Exception E) {
                #if DEBUG
                Log.Error("Error fetching online data for:\n" + 
                    ep.filename + "\n" + 
                    ep.series + " " + ep.season + "x" + ep.episode + " " + ep.title,
                    E);
                #endif
                return "";
            }
         
       }

        public void getEpisodeData(ref Episode ep) {
            try {
                showClass show = new showClass();
                show.Name = ep.series;
                String seriesOld = ep.series.ToLower();

                if (seriesList.ContainsKey(seriesOld)) {
                    show = (showClass)seriesList[seriesOld];
                    ep.series = show.Name;
                }
                else if (seriesList.ContainsKey(ep.altSeries.ToLower())) {
                    show = (showClass)seriesList[ep.altSeries.ToLower()];
                    ep.series = show.Name;
                }
                else {
                    // No data found yet.
                }
                seriesHash = MD5.createHash(show.Name);
                episodeCache = String.Format(baseCache, seriesHash, ep.season);

                if (ep.special) return;

                bool hr = getSeriesData(ref show, ref ep);

                if (show != null && show.ID != "") {
                    bool listChanged = false;
                    if (!seriesList.ContainsKey(ep.series.ToLower())) {
                        seriesList.Add(ep.series.ToLower(), show);
                        listChanged = true;
                    }
                    if (!seriesList.ContainsKey(seriesOld)) {
                        seriesList.Add(seriesOld, show);
                        listChanged = true;
                    }
                    if (!seriesList.ContainsKey(show.Name.ToLower())) {
                        seriesList.Add(show.Name.ToLower(), show);
                        listChanged = true;
                    }
                    if (listChanged)
                        Settings.SetValue(parserDataCache, seriesList);
                }

                /*if (!hr)
                {
                    this.renameGeneric(ref ep);
                }*/

                MatchCollection mcol = null;
                Match m = null;
                Regex uni = new Regex("&#([0-9]+);");
                mcol = uni.Matches(ep.title);
                if (mcol.Count > 0) {
                    Regex real = new Regex("\\(([-a-zA-Z0-9.,%:;!?'\"+() ]{5,})\\)");
                    m = real.Match(ep.title);
                    if (m.Success) {
                        ep.title = m.Groups[1].Captures[0].Value;
                    }
                }
            }
            catch (Exception E) {
                #if DEBUG
                Log.Error("Error fetching online data for:\n" + 
                    ep.filename + "\n" + 
                    ep.series + " " + ep.season + "x" + ep.episode + " " + ep.title,
                    E);
                #endif
            }
        }

        internal void renameGeneric(ref Episode ep) {
            FileInfo fi = new FileInfo(ep.filename);

            String fname = fi.Name;
            Regex brack = new Regex("\\[([0-9A-Za-z-]*)\\]");
            MatchCollection mcol = brack.Matches(fname);
            if (mcol.Count > 0) {
                for (int i = 0; i < mcol.Count; i++) {
                    fname = fname.Replace(mcol[i].Groups[0].Captures[0].Value, "");
                }
            }

            Regex epId = new Regex("([0-9]{2,})");
            Match m = epId.Match(fname);
            int episodeId = 0;
            if (m.Success) {
                episodeId = Int32.Parse(m.Groups[0].Captures[0].Value);
            }

            String file = fi.Name;
            file = file.Replace(fi.Extension, "");
            file = file.Replace('.', ' ');
            file = file.Replace('_', ' ');
            file = file.Substring(file.LastIndexOf(" - ") + 3);
            ep.title = file;
        }

    }
}
