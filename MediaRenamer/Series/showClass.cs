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

namespace MediaRenamer.Series {
    /// <summary>
    /// Zusammenfassung für showClass.
    /// </summary>
    public class showClass : IComparable<showClass>, IEquatable<showClass> {
        public String ID = "";
        public String Name = "";
        public int Year = 0;
        public String Lang = "";

        #region IComparable<showClass> Member

        public int CompareTo(showClass show) {
            if (this.Name == show.Name) {
                if (this.Year == show.Year) {
                    if (this.Lang == show.Lang) {
                        return 0;
                    }
                    else {
                        return this.Lang.CompareTo(show.Lang);
                    }
                }
                else {
                    return this.Year.CompareTo(show.Year);
                }
            }
            else {
                return this.Name.CompareTo(show.Name);
            }
        }

        #endregion

        #region IEquatable<showClass> Member

        public bool Equals(showClass other) {
            return this.Name == other.Name &&
                this.Year == other.Year &&
                this.Lang == other.Lang &&
                this.ID == other.ID;
        }

        #endregion

        public override String ToString() {
            String str = this.Name;
            str += "(";
            if (this.Year > 0) {
                str += this.Year.ToString();
                if (this.Lang != String.Empty) {
                    str += ",";
                }
            }
            if (this.Lang != String.Empty) {
                str += this.Lang;
            }
            str += ")";
            return str;
        }
    }
}
