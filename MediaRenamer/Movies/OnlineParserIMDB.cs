using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace MediaRenamer.Movies {
    class OnlineParserIMDB: OnlineParserBase {

        public static String parserName = "IMDB.com";

        override public void getMovieData(ref Movie movie) {
            String data = loadMovieData(ref movie);

            if (data == null) return;

            String pat = "<a href=\"/title/tt([0-9]*)/\">([^<]*)</a>[^<]+<span class=\"year_type\">([^<]+)</span>";
            Regex reg = new Regex(pat);
            MatchCollection mcol = null;
            mcol = reg.Matches(data);
            List<movieData> movieNames = new List<movieData>();
            if (mcol.Count > 0) {
                foreach (Match m in mcol) {
                    movieNames.Add(parseMovie(m.Groups[2].Captures[0].Value, m.Groups[3].Captures[0].Value));
                }
            }

            if (movieNames.Count > 0)
            {
                movieData selectedMovie = this.chooseMovie(movie, movieNames);
                setMovieTitle(ref movie, selectedMovie);
            }
        }

        private movieData parseMovie(String movieName, String year) {
            movieData md = new movieData();
            md.Name = movieName;
            String pat = @"([0-9]{4})";
            Regex reg = new Regex(pat);
            Match m = null;
            m = reg.Match(year);
            if (m.Success) {
                md.Year = Int32.Parse(m.Groups[0].Captures[0].Value);
            }

            return md;
        }

        private String loadMovieData(ref Movie movie) {
            String movieName = movie.title;
            if (movie.year > 0) {
                movieName += " (" + movie.year.ToString() + ")";
            }
            movieName = movieName.Replace(" &", "");
            movieName = movieName.Replace(" And", "");
            movieName = movieName.Replace(" and", "");
            movieCache = String.Format(baseCache, movie.title, movie.year);
            String data = null;

            if (!File.Exists(String.Format(movieCache, movieName))) {
                //Log.Add( i18n.t("oparse_search", movieName) );
                String movieEnc = movieName;
                movieEnc = movieEnc.Replace("-", "+");
                movieEnc = movieEnc.Replace(" ", "+");
                movieEnc = movieEnc.Replace(movie.year.ToString(), "");

                String url = "http://www.imdb.com/search/title?";
                url += String.Format("release_date={0},", movie.year.ToString());
                url += String.Format("&title={0}", movieEnc);
                url += "&title_type=feature,tv_movie,tv_series,mini_series";
                WebClient cli = new WebClient();
                data = cli.DownloadString(url);
                StreamWriter swr = new StreamWriter(movieCache, false);
                swr.Write(data);
                swr.Close();
            }

            if (File.Exists(String.Format(movieCache, movieName))) {
                StreamReader sreader = new StreamReader(movieCache, System.Text.Encoding.Default, true);
                data = sreader.ReadToEnd();
                sreader.Close();
            }
            return data;
        }
    }
}
