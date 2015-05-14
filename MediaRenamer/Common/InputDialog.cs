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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MediaRenamer.Common {
    public partial class InputDialog : Form {
        public String value = "";

        public InputDialog() {
            InputDialogConstruct("Enter your data", "Question:", "");
        }
        public InputDialog(String q) {
            InputDialogConstruct(q, "Question:", "");
        }
        public InputDialog(String q, String title) {
            InputDialogConstruct(q, title, "");
        }
        public InputDialog(String q, String title, String def) {
            InputDialogConstruct(q, title, def);
        }

        private void InputDialogConstruct(String q, String title, String def) {
            InitializeComponent();
            this.Text = title;
            this.question.Text = q;
            this.answerBox.Text = def;
        }

        private void btnOk_Click(object sender, EventArgs e) {
            this.value = answerBox.Text;
        }
    }
}
