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
using System.Text.RegularExpressions;

namespace MediaRenamer.Common {
    public class Eregi {
        /// <summary>
        /// Replace regular expression in string
        /// </summary>
        /// <param name="pat">Search pattern</param>
        /// <param name="newStr">New String. \\0..\\n for found values</param>
        /// <param name="input">Inputstring</param>
        /// <returns>modified String</returns>
        public static String replace(String pat, String newStr, String input) {
            Regex reg = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.None);
            Match m = null;
            String newStrTmp = "";
            while (true) {
                m = reg.Match(input);
                newStrTmp = newStr;
                if (m.Success) {
                    for (int i = 0; i < m.Groups.Count; i++) {
                        newStrTmp = newStrTmp.Replace("\\" + i, m.Groups[i].Captures[0].Value);
                    }
                    input = input.Replace(m.Groups[0].Captures[0].Value, newStrTmp);
                }
                else break;
            }
            return input;
        }
    }
}
