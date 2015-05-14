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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using MediaRenamer;
using MediaRenamer.Common;
using System.IO;
using System.Collections.Generic;

namespace MediaRenamer.Series {
    /// <summary>
    /// Zusammenfassung für SelectShow.
    /// </summary>
    public class SelectShow : System.Windows.Forms.Form {
        public showClass selectedShow = null;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListBox showList;
        private System.Windows.Forms.Button btnSkip;
        private Label labelEpisode;
        private Label labelFile;
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public SelectShow() {
            //
            // Erforderlich für die Windows Form-Designerunterstützung
            //
            InitializeComponent();

            //
            // TODO: Fügen Sie den Konstruktorcode nach dem Aufruf von InitializeComponent hinzu
            //
        }

        /// <summary>
        /// Die verwendeten Ressourcen bereinigen.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code
        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectShow));
            this.btnOk = new System.Windows.Forms.Button();
            this.showList = new System.Windows.Forms.ListBox();
            this.btnSkip = new System.Windows.Forms.Button();
            this.labelEpisode = new System.Windows.Forms.Label();
            this.labelFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.AccessibleDescription = null;
            this.btnOk.AccessibleName = null;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.BackgroundImage = null;
            this.btnOk.Font = null;
            this.btnOk.Name = "btnOk";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // showList
            // 
            this.showList.AccessibleDescription = null;
            this.showList.AccessibleName = null;
            resources.ApplyResources(this.showList, "showList");
            this.showList.BackgroundImage = null;
            this.showList.Font = null;
            this.showList.Name = "showList";
            this.showList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.showList_DrawItem);
            this.showList.SelectedIndexChanged += new System.EventHandler(this.showList_SelectedIndexChanged);
            this.showList.DoubleClick += new System.EventHandler(this.showList_DoubleClick);
            // 
            // btnSkip
            // 
            this.btnSkip.AccessibleDescription = null;
            this.btnSkip.AccessibleName = null;
            resources.ApplyResources(this.btnSkip, "btnSkip");
            this.btnSkip.BackgroundImage = null;
            this.btnSkip.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSkip.Font = null;
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // labelEpisode
            // 
            this.labelEpisode.AccessibleDescription = null;
            this.labelEpisode.AccessibleName = null;
            resources.ApplyResources(this.labelEpisode, "labelEpisode");
            this.labelEpisode.Font = null;
            this.labelEpisode.Name = "labelEpisode";
            // 
            // labelFile
            // 
            this.labelFile.AccessibleDescription = null;
            this.labelFile.AccessibleName = null;
            resources.ApplyResources(this.labelFile, "labelFile");
            this.labelFile.Font = null;
            this.labelFile.Name = "labelFile";
            // 
            // SelectShow
            // 
            this.AcceptButton = this.btnOk;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.CancelButton = this.btnSkip;
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.labelEpisode);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.showList);
            this.Controls.Add(this.btnOk);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = null;
            this.Name = "SelectShow";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SelectShow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        public void setEpisodeData(Episode ep) {
            labelEpisode.Text = "Season " + ep.season + " Episode " + ep.episode + ": " + ep.title;
            FileInfo fi = new FileInfo(ep.filename);
            if (fi.Directory.Parent != null)
            {
                labelFile.Text = fi.Directory.Parent.Name + @"\" + fi.Directory.Name + @"\" + fi.Name;
            }
            else
            {
                labelFile.Text = fi.Directory.Name + @"\" + fi.Name;
            }
        }

        public void addShows(List<showClass> shows) {
            shows.Sort();
            foreach (showClass show in shows) {
                bool exists = false;
                foreach (showClass existing in showList.Items) {
                    if (existing.Equals(show)) {
                        exists = true;
                        break;
                    }
                }
                if (!exists) {
                    this.showList.Items.Add(show);
                }

                /*
                if (!showList.Items.Contains(show))
                {
                    showList.Items.Add(show);
                }
                */
            }
        }

        private void showList_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e) {
            if (e.Index < 0 || e.Index > showList.Items.Count)
                return;
            try {
                showClass sc = (showList.Items[e.Index] as showClass);
                e.DrawBackground();
                Brush b = Brushes.Black;
                String title = "";
                if (sc.Lang == "") {
                    title = sc.Name + " (" + sc.Year.ToString() + ")";
                }
                else {
                    title = sc.Name + " (" + sc.Year.ToString() + "," + sc.Lang + ")";
                }
                e.Graphics.DrawString(title, e.Font, b, e.Bounds);
                e.DrawFocusRectangle();
            }
            catch (Exception E) {
                Log.Add("DrawItem Error:\n" + E.Message);
            }
        }

        private void SelectShow_Load(object sender, System.EventArgs e) {
            if (showList.Items.Count > 0) {
                selectedShow = showList.Items[0] as showClass;
                showList.SelectedIndex = 0;
            }
        }

        private void showList_SelectedIndexChanged(object sender, System.EventArgs e) {
            if (showList.SelectedItem != null) {
                selectedShow = (showClass)showList.SelectedItem;
            }
        }

        private void showList_DoubleClick(object sender, System.EventArgs e) {
            DialogResult = DialogResult.OK;
        }

        private void btnSkip_Click(object sender, System.EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, System.EventArgs e) {
            DialogResult = DialogResult.OK;
        }
    }
}
