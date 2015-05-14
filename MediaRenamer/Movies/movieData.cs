using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MediaRenamer.Movies {
    public class movieData {
        private String _name;
        public int Year = 0;

        public String Name {
            get {
                return _name;
            }
            set {
                String val = value;
                val = val.Trim();
                Regex reg = new Regex("&#([0-9]+);");
                MatchCollection mcol = null;
                mcol = reg.Matches(val);
                if (mcol.Count > 0) {
                    foreach (Match m in mcol) {
                        Char c = (Char)Int32.Parse(m.Groups[1].Captures[0].Value);
                        val = val.Replace(
                            m.Groups[0].Captures[0].Value,
                            c.ToString()
                            );
                    }
                }
                val = val.Replace("\"", "");
                val = val.Replace("&quot;", "");
                val = val.Replace("&amp;", "&");
                val = val.Replace("(V)", "");
                val = val.Replace("  ", " ");
                _name = val;
            }
        }

        public override String ToString() {
            return String.Format("{0} ({1})", Name, Year);
        }
    }
}
