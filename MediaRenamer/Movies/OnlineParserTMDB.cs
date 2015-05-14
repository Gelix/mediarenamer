using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using MediaRenamer.Common;

namespace MediaRenamer.Movies {
    class OnlineParserTMDB: OnlineParserBase {

        public static String parserName = "TheMovieDB.org";

        private String searchUrl = "http://api.themoviedb.org/2.0/Movie.search?title={0}&api_key=57983e31fb435df4df77afb854740ea9";

        override public void getMovieData(ref Movie movie) {
            movieCache = String.Format(baseCache, MD5.createHash(movie.title), movie.year);
            WebClient cli = new WebClient();

            if (!File.Exists(movieCache)) {
                cli.DownloadFile(String.Format(searchUrl, movie.title), movieCache);
            }

            if (File.Exists(movieCache)) {
                XmlDocument xml = new XmlDocument();
                List<movieData> movies = new List<movieData>();
                DateTime dt = File.GetLastWriteTime(movieCache);
                if (DateTime.Now.Subtract(dt).TotalDays > 3) {
                    // Log.Add( i18n.t( "oparse_older", ep.series) );
                    File.Delete(movieCache);
                    cli.DownloadFile(String.Format(searchUrl, movie.title), movieCache);
                }
                xml.Load(movieCache);

                XmlNodeList nodes = xml.GetElementsByTagName("movie");
                foreach (XmlNode node in nodes) {
                    Int32 year = 0;
                    if (node.SelectSingleNode("release") != null) {
                        if (!String.IsNullOrEmpty(node.SelectSingleNode("release").InnerText))
                        {
                            year = Int32.Parse(node.SelectSingleNode("release").InnerText.Substring(0, 4));
                        }
                    }
                    else {
                        year = movie.year;
                    }
                    movieData md = null;
                    
                    md = new movieData();
                    md.Year = year;
                    if (node.SelectSingleNode("release") != null) { md.Name = node.SelectSingleNode("title").InnerText; }
                    movies.Add(md);
                    if (node.SelectSingleNode("alternative_title") != null)
                    {
                        if (node.SelectSingleNode("alternative_title").InnerText != string.Empty)
                        {
                            md = new movieData();
                            md.Year = year;
                            md.Name = node.SelectSingleNode("alternative_title").InnerText;
                            movies.Add(md);
                        }
                    }
                }

                movieData selectedMovie = this.chooseMovie(movie, movies);
                setMovieTitle(ref movie, selectedMovie);
            }
        }
    }
}
