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
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MediaRenamer;
using MediaRenamer.Common;
using System.Threading;

namespace MediaRenamer.Movies {
    /// <summary>
    /// Zusammenfassung für MovieParser.
    /// </summary>
    public class Parser: ParserBase {

        public Parser(String folder)
            : base(folder) {
        }

        public event ListMovieHandler ListMovie;
        protected virtual void OnListMovie(Movie m) {
            ListMovieHandler handler = ListMovie;
            if (handler != null) {
                handler(m);
            }
        }

        override internal void scanFolder(String folder) {
            String[] elements;
            elements = Directory.GetFileSystemEntries(folder);
            for (int i = 0; i < elements.Length; i++) {
                if (isValidExt(elements[i])) {
                    FileAttributes fAttr = File.GetAttributes(elements[i]);
                    if ((fAttr | FileAttributes.ReadOnly)
                        == fAttr) {
                        //MessageBox.Show("File is write protected: \n"+elements[i]);
                    }
                    else {
                        Movie movie = Movie.parseFile(elements[i]);
                        if (movie.needRenaming()) {
                            OnListMovie(movie);
                        }
                    }
                }

                OnScanProgress(i, elements.Length - 1);
            }

            OnScanDone();
        }
    }
}
