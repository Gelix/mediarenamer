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
using System.Windows.Forms;
using MediaRenamer.Common;
using System.Collections.Generic;

namespace MediaRenamer.Movies {
    /// <summary>
    /// Zusammenfassung für OnlineParser.
    /// </summary>
    abstract public class OnlineParserBase {
        internal String movieCache = @"";
        internal String baseCache = "";
        internal String movePrefix = ", A, An, The, Le, Die, Der, Das";

        public OnlineParserBase() {
            String parserName = Settings.GetValueAsString(SettingKeys.MoviesParser);

            String cacheDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                @"\" + Application.ProductName + @"\movies\" + parserName + @"\";
            if (!Directory.Exists(cacheDir)) Directory.CreateDirectory(cacheDir);
            baseCache = cacheDir + "{0}_{1}.xml";
            movieCache = String.Format(baseCache, "movie", "base");
        }

        abstract public void getMovieData(ref Movie movie);

        internal void setMovieTitle(ref Movie movie, movieData movieData) {
            movie.title = movieData.Name;
            movie.year = movieData.Year;
        }

        internal movieData chooseMovie(Movie m, List<movieData> movies) {
            movieData md = new movieData();
            md.Name = m.title;
            md.Year = m.year;
            if (movies.Count == 0) return md;
            if (movies.Count == 1) return movies[0];

            foreach (movieData movie in movies) {
                if (m.compareTitles(movie)) return movie;
            }

            SelectMovie movieDlg = new SelectMovie();
            movieDlg.Text = String.Format("Select movie for {0} ({1})", m.title, m.year);
            movieDlg.setMovieData(m);

            foreach (movieData movie in movies) {
                movieDlg.addMovie(movie);
            }

            if (movieDlg.ShowDialog() == DialogResult.OK) {
                md = movieDlg.selectedMovie;
            }
            movieDlg.Dispose();
            return md;
        }
        
    }
}
