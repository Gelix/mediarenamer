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
using System.Windows.Forms;

namespace MediaRenamer.Common {
    /// <summary>
    /// Zusammenfassung für Log.
    /// </summary>
    public class Log {
        public static void Add(string text) {
            //Form frm = mainForm.instance;
            //MessageBox.Show(frm.Name);
            //mainForm frm = (mainForm.instance as mainForm);
        }

        public static void Error(string p, Exception E) {
            MessageBox.Show(p + "\n" +
                E.InnerException + "\n" +
                E.Message + "\n" +
                E.Source + "\n" +
                E.StackTrace, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
