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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using MediaRenamer.Common;
using MediaRenamer.Movies;
using MediaRenamer.Series;
using Microsoft.Win32;
using System.Resources;

namespace MediaRenamer {
    public partial class mainForm : Form {
        public static mainForm instance = null;
        public RenameDrop dropform = new RenameDrop();

        ResourceManager resources = null;

        Thread parseSeriesThread;
        Thread parseMoviesThread;

        public mainForm() {
            InitializeComponent();

            resources = new ResourceManager(typeof(mainForm));
            mainForm.instance = this;
        }

        private void mainForm_Load(object sender, EventArgs e) {
            System.Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = Application.ProductName + " v" + v.ToString();

            notifyIcon.Visible = Settings.GetValueAsBool(SettingKeys.SysTrayIcon);
            optionSysTray.Checked = Settings.GetValueAsBool(SettingKeys.SysTrayIcon);
            this.ShowInTaskbar = !Settings.GetValueAsBool(SettingKeys.SysTrayIcon);

            optionStartMinimized.Checked = Settings.GetValueAsBool(SettingKeys.StartMinimized);
            if (Settings.GetValueAsBool(SettingKeys.StartMinimized)) {
                if (Settings.GetValueAsBool(SettingKeys.SysTrayIcon)) {
                    this.WindowState = FormWindowState.Minimized;
                    this.Visible = !Settings.GetValueAsBool(SettingKeys.StartMinimized);
                }
                else {
                    this.WindowState = FormWindowState.Minimized;
                }
            }

            option_moveMovies.Checked = Settings.GetValueAsBool(SettingKeys.MoveMovies);
            option_movieTargetLocation.Text = Settings.GetValueAsString(SettingKeys.MovieLocation);

            displayDropTarget(Settings.GetValueAsBool(SettingKeys.DisplayDropTarget));
            optionDropTarget.Checked = Settings.GetValueAsBool(SettingKeys.DisplayDropTarget);

            String movieFmt = Settings.GetValueAsString(SettingKeys.MovieFormat);
            if (movieFmt != String.Empty) {
                option_movieFormat.Text = movieFmt;
            }

            String seriesFmt = Settings.GetValueAsString(SettingKeys.SeriesFormat);
            if (seriesFmt != String.Empty) {
                option_seriesFormat.Text = seriesFmt;
            }

            Object[] items = null;
            items = Settings.GetValueAsObject<Object[]>(SettingKeys.MoviePaths);
            if (items != null) {
                movieScanPath.Items.AddRange(items);
            }
            items = Settings.GetValueAsObject<Object[]>(SettingKeys.SeriesPaths);
            if (items != null) {
                seriesScanPath.Items.AddRange(items);
            }

            String parserName = Settings.GetValueAsString(SettingKeys.SeriesParser);
            if (parserName == String.Empty) {
                parserName = OnlineParserTVDB.parserName;
                Settings.SetValue(SettingKeys.SeriesParser, OnlineParserEPW.parserName);
            }
            optionSeriesParser.Items.Add(OnlineParserEPW.parserName);
            optionSeriesParser.Items.Add(OnlineParserTVDB.parserName);
            optionSeriesParser.SelectedItem = parserName;

            parserName = Settings.GetValueAsString(SettingKeys.MoviesParser);
            if (parserName == String.Empty) {
                parserName = OnlineParserTMDB.parserName;
                Settings.SetValue(SettingKeys.MoviesParser, OnlineParserTMDB.parserName);
            }
            optionMovieParser.Items.Add(OnlineParserTMDB.parserName);
            optionMovieParser.Items.Add(OnlineParserIMDB.parserName);
            optionMovieParser.SelectedItem = parserName;

            /*if (VistaGlass.IsGlassSupported()) {
                VistaGlass.Margins marg = new VistaGlass.Margins();
                //marg.Top = panelTop.Height;
                //marg.Left = panelTop.Width;

                //panelTop.BackColor = Color.Black;
                VistaGlass.ExtendGlassFrame(this.Handle, ref marg);
            }*/

        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (Settings.GetValueAsBool(SettingKeys.SysTrayIcon)) {
                if (e.CloseReason == CloseReason.UserClosing) {
                    e.Cancel = true;
                    this.Hide();
                }
            }

            if (!e.Cancel) {
                if (parseSeriesThread != null) {
                    parseSeriesThread.Abort();
                }
                if (parseMoviesThread != null) {
                    parseMoviesThread.Abort();
                }
            }
        }

        private void contextProposals_Opening(object sender, CancelEventArgs e) {
            ContextMenuStrip cms = (sender as ContextMenuStrip);
            if (option_seriesFormat.Focused) {
                cms.Items.Clear();
                cms.Items.Add("<series>");
                cms.Items.Add("<season>");
                cms.Items.Add("<season2>");
                cms.Items.Add("<episode>");
                cms.Items.Add("<episode2>");
                cms.Items.Add("<title: >");
                cms.Items.Add("<title>");
            }
            if (option_movieFormat.Focused) {
                cms.Items.Clear();
                cms.Items.Add("<moviename>");
                cms.Items.Add("<year: >");
                cms.Items.Add("<year>");
                cms.Items.Add("<disk: >");
                cms.Items.Add("<disk>");
                cms.Items.Add("<lang: >");
                cms.Items.Add("<lang>");
            }
        }

        private void contextProposals_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            ContextMenuStrip cms = (sender as ContextMenuStrip);
            if (option_seriesFormat.Focused) {
                option_seriesFormat.SelectedText = e.ClickedItem.Text;
            }
            if (option_movieFormat.Focused) {
                option_movieFormat.SelectedText = e.ClickedItem.Text;
            }
        }

        private bool validPath(String path) {
            if (path == String.Empty) {
                MessageBox.Show(resources.GetString("folderChoose"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!Directory.Exists(path)) {
                MessageBox.Show(resources.GetString("folderMissing"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnMovieScan_Click(object sender, EventArgs e) {
            if (!validPath(movieScanPath.Text)) return;

            btnMovieScan.Visible = false;
            btnMovieScanStop.Visible = true;
            movieScanPath.Enabled = false;
            btnMoviesBrowse.Enabled = false;
            optionGroupMovies.Enabled = false;

            storeMoviePath(movieScanPath.Text);
            scanMovieList.Items.Clear();

            MediaRenamer.Movies.Parser mparse = new MediaRenamer.Movies.Parser(movieScanPath.Text);
            mparse.ScanProgress += new ScanProgressHandler(movie_ScanProgress);
            mparse.ListMovie += new ListMovieHandler(movie_ListMovie);
            mparse.ScanDone += new ScanDone(movie_ScanDone);

            parseMoviesThread = new Thread(new ThreadStart(mparse.startScan));
            parseMoviesThread.Start();
        }

        void movie_ScanDone() {
            if (InvokeRequired) {
                Invoke(new ScanDone(movie_ScanDone), null);
                return;
            }
            btnMovieScan.Visible = true;
            btnMovieScanStop.Visible = false;
            movieScanPath.Enabled = true;
            btnMoviesBrowse.Enabled = true;
            optionGroupMovies.Enabled = true;
        }

        private void storeMoviePath(string p) {
            if (movieScanPath.Items.IndexOf(p) == -1) {
                movieScanPath.Items.Insert(0, p);
                Object[] items = new Object[movieScanPath.Items.Count];
                movieScanPath.Items.CopyTo(items, 0);
                Settings.SetValue(SettingKeys.MoviePaths, items);
            }
        }

        void movie_ListMovie(Movie m) {
            if (InvokeRequired) {
                Invoke(new ListMovieHandler(movie_ListMovie), m);
                return;
            }
            ListViewItem node = new ListViewItem(m.title);
            node.SubItems.Add(m.year.ToString());
            FileInfo fi = new FileInfo(m.filename);
            node.SubItems.Add(fi.Name);
            node.SubItems.Add(m.modifiedName());
            node.Tag = m;

            scanMovieList.Items.Add(node);
        }

        void movie_ScanProgress(int pos, int max) {
            if (InvokeRequired) {
                Invoke(new ScanProgressHandler(movie_ScanProgress), pos, max);
                return;
            }

            scanMovieProgressbar.Maximum = max;
            scanMovieProgressbar.Value = pos;
        }

        private void btnSeriesScan_Click(object sender, EventArgs e) {
            if (!validPath(seriesScanPath.Text)) return;

            btnSeriesScanStop.Visible = true;
            btnSeriesScan.Visible = false;
            seriesScanPath.Enabled = false;
            btnSeriesBrowse.Enabled = false;
            optionGroupSeries.Enabled = false;

            storeSeriesPath(seriesScanPath.Text);
            scanSeriesList.Items.Clear();

            MediaRenamer.Series.Parser tparse = new MediaRenamer.Series.Parser(seriesScanPath.Text);
            tparse.ScanProgress += new ScanProgressHandler(series_ScanProgress);
            tparse.ListEpisode += new ListEpisodeHandler(series_ListEpisode);
            tparse.ScanDone += new ScanDone(series_ScanDone);

            parseSeriesThread = new Thread(new ThreadStart(tparse.startScan));
            parseSeriesThread.Start();
        }

        void series_ScanDone() {
            if (InvokeRequired) {
                Invoke(new ScanDone(series_ScanDone), null);
                return;
            }

            btnSeriesScan.Visible = true;
            btnSeriesScanStop.Visible = false;
            seriesScanPath.Enabled = true;
            btnSeriesBrowse.Enabled = true;
            optionGroupSeries.Enabled = true;
        }

        private void storeSeriesPath(string p) {
            if (seriesScanPath.Items.IndexOf(p) == -1) {
                seriesScanPath.Items.Insert(0, p);

                Object[] items = new Object[seriesScanPath.Items.Count];
                seriesScanPath.Items.CopyTo(items, 0);
                Settings.SetValue(SettingKeys.SeriesPaths, items);
            }
        }

        void series_ListEpisode(Episode ep) {
            if (InvokeRequired) {
                Invoke(new ListEpisodeHandler(series_ListEpisode), ep);
                return;
            }
            ListViewItem node = new ListViewItem(ep.series);
            node.SubItems.Add(ep.season.ToString());
            String episodes = "";
            for (int i = 0; i < ep.episodes.Length; i++) {
                episodes += ep.episodes[i].ToString();
                if (i < (ep.episodes.Length - 1)) {
                    episodes += ", ";
                }
            }
            node.SubItems.Add(episodes);
            node.SubItems.Add(ep.title);
            FileInfo fi = new FileInfo(ep.filename);
            node.SubItems.Add(fi.Name);
            node.SubItems.Add(ep.modifiedName());
            node.Tag = ep;

            scanSeriesList.Items.Add(node);
        }

        void series_ScanProgress(int pos, int max) {
            if (InvokeRequired) {
                Invoke(new ScanProgressHandler(series_ScanProgress), pos, max);
                return;
            }

            scanSeriesProgressbar.Maximum = max;
            scanSeriesProgressbar.Value = pos;
        }

        private void contextOptionRename_Click(object sender, EventArgs e) {
            if (tabControl.SelectedTab == tabSeries) {
                renameSelectedSeries();
            }
            if (tabControl.SelectedTab == tabMovies) {
                renameSelectedMovies();
            }
        }

        private void contextRename_Opening(object sender, CancelEventArgs e) {
            if (tabControl.SelectedTab == tabSeries) {
                contextOptionRename.Enabled = (scanSeriesList.CheckedItems.Count > 0);
                contextOptionRename.Text = resources.GetString("contextRenameEpisode");
            }
            if (tabControl.SelectedTab == tabMovies) {
                contextOptionRename.Enabled = (scanMovieList.CheckedItems.Count > 0);
                contextOptionRename.Text = resources.GetString("contextRenameMovie");
            }
        }

        private void renameSelectedMovies() {
            if (scanMovieList.CheckedItems.Count > 0) {
                Movie m;
                for (int i = scanMovieList.CheckedItems.Count - 1; i >= 0; i--) {
                    m = (Movie)scanMovieList.CheckedItems[i].Tag;
                    m.renameMovie();
                    scanMovieList.CheckedItems[i].Remove();
                }
            }
        }

        private void renameSelectedSeries() {
            if (scanSeriesList.CheckedItems.Count > 0) {
                Episode ep;
                for (int i = scanSeriesList.CheckedItems.Count - 1; i >= 0; i--) {
                    ep = (Episode)scanSeriesList.CheckedItems[i].Tag;
                    ep.renameEpisode();
                    scanSeriesList.CheckedItems[i].Remove();
                }
            }
        }

        private void scanMovieList_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter) {
                renameSelectedMovies();
            }
        }

        private void scanSeriesList_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter) {
                renameSelectedSeries();
            }
        }

        private void option_movieFormat_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if (!option_movieFormat.Focused)
                    option_movieFormat.Focus();
            }
        }

        private void option_seriesFormat_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if (!option_seriesFormat.Focused)
                    option_seriesFormat.Focus();
            }
        }


        private void sysTrayExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                this.Show();
            }
        }

        private void sysTrayOpen_Click(object sender, EventArgs e) {
            this.Show();

            // Move window to foreground
            this.TopMost = true;
            this.TopMost = false;
        }

        private void optionSysTray_CheckedChanged(object sender, EventArgs e) {
            Settings.SetValue(SettingKeys.SysTrayIcon, optionSysTray.Checked);
            notifyIcon.Visible = optionSysTray.Checked;
            this.ShowInTaskbar = !optionSysTray.Checked;
        }

        private void option_seriesFormat_Leave(object sender, EventArgs e) {
            Settings.SetValue(SettingKeys.SeriesFormat, option_seriesFormat.Text);
        }

        private void option_movieFormat_Leave(object sender, EventArgs e) {
            Settings.SetValue(SettingKeys.MovieFormat, option_movieFormat.Text);
        }

        private void seriesScanPath_TextUpdate(object sender, EventArgs e) {
            btnSeriesScan.Enabled = Directory.Exists(seriesScanPath.Text);
        }

        private void movieScanPath_TextUpdate(object sender, EventArgs e) {
            btnMovieScan.Enabled = Directory.Exists(movieScanPath.Text);
        }

        private void tvSelAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < scanSeriesList.Items.Count; i++) {
                scanSeriesList.Items[i].Checked = true;
            }
        }

        private void tvSelNone_Click(object sender, EventArgs e) {
            for (int i = 0; i < scanSeriesList.Items.Count; i++) {
                scanSeriesList.Items[i].Checked = false;
            }
        }

        private void tvRenameEpisodes_Click(object sender, EventArgs e) {
            renameSelectedSeries();
        }

        private void movieSelAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < scanMovieList.Items.Count; i++) {
                scanMovieList.Items[i].Checked = true;
            }
        }

        private void movieSelNone_Click(object sender, EventArgs e) {
            for (int i = 0; i < scanMovieList.Items.Count; i++) {
                scanMovieList.Items[i].Checked = false;
            }
        }

        private void movieRename_Click(object sender, EventArgs e) {
            renameSelectedMovies();
        }

        private void btnSeriesBrowse_Click(object sender, EventArgs e) {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                String path = folderBrowserDialog.SelectedPath;
                if (!path.EndsWith(@"\")) {
                    path += @"\";
                }
                seriesScanPath.Text = path;
            }
        }

        private void btnPathMovies_Click(object sender, EventArgs e) {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                String path = folderBrowserDialog.SelectedPath;
                if (!path.EndsWith(@"\")) {
                    path += @"\";
                }
                movieScanPath.Text = path;
            }
        }

        private void displayDropTargetToolStripMenuItem_Click(object sender, EventArgs e) {
            displayDropTarget(!dropform.Visible);
        }

        private void displayDropTarget(bool visible) {
            if (visible) {
                if (dropform.IsDisposed) {
                    dropform = new RenameDrop();
                }
                dropform.Show();
            }
            else {
                dropform.Hide();
            }
        }

        private void mainForm_SizeChanged(object sender, EventArgs e) {
            if (Settings.GetValueAsBool(SettingKeys.SysTrayIcon)) {
                if (this.WindowState == FormWindowState.Minimized) {
                    this.WindowState = FormWindowState.Normal;
                    this.Hide();
                }
            }
        }

        private void optionDropTarget_CheckedChanged(object sender, EventArgs e) {
            Settings.SetValue(SettingKeys.DisplayDropTarget, optionDropTarget.Checked);
            displayDropTarget(optionDropTarget.Checked);
        }

        private void btnMenu_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            Point[] points = new Point[3];
            points[0] = new Point(e.ClipRectangle.Right - 12, e.ClipRectangle.Top + (e.ClipRectangle.Height / 2) - 1);
            points[1] = new Point(e.ClipRectangle.Right - 7, e.ClipRectangle.Top + (e.ClipRectangle.Height / 2) - 1);
            points[2] = new Point(e.ClipRectangle.Right - 10, e.ClipRectangle.Top + (e.ClipRectangle.Height / 2) + 2);
            g.FillPolygon(Brushes.Black, points);
        }

        private void btnMovePath_Click(object sender, EventArgs e) {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            browse.Description = resources.GetString("moveFolderDescription");
            if (browse.ShowDialog() == DialogResult.OK) {
                option_movieTargetLocation.Text = browse.SelectedPath;
                Settings.SetValue(SettingKeys.MovieLocation, option_movieTargetLocation.Text);
            }
        }

        private void optionStartMinimized_CheckedChanged(object sender, EventArgs e) {
            Settings.SetValue(SettingKeys.StartMinimized, optionStartMinimized.Checked);
        }

        private void option_moveMovies_CheckedChanged(object sender, EventArgs e) {
            Settings.SetValue(SettingKeys.MoveMovies, option_moveMovies.Checked);
        }

        private void option_movieTargetLocation_TextChanged(object sender, EventArgs e) {
            Settings.SetValue(SettingKeys.MovieLocation, option_movieTargetLocation.Text);
        }

        private void seriesScanPath_Leave(object sender, EventArgs e) {
            if (seriesScanPath.Text.Length > 0 && !seriesScanPath.Text.EndsWith(@"\"))
                seriesScanPath.Text += @"\";
            btnSeriesScan.Enabled = Directory.Exists(seriesScanPath.Text);
            if (!Directory.Exists(seriesScanPath.Text)) {
                seriesScanPath.Items.Remove(seriesScanPath.SelectedItem);
                if (seriesScanPath.Items.Count > 0) {
                    seriesScanPath.SelectedIndex = 0;
                }
            }
        }

        private void movieScanPath_Leave(object sender, EventArgs e) {
            if (movieScanPath.Text.Length > 0 && !movieScanPath.Text.EndsWith(@"\"))
                movieScanPath.Text += @"\";

            btnMovieScan.Enabled = Directory.Exists(movieScanPath.Text);
            if (!Directory.Exists(movieScanPath.Text)) {
                movieScanPath.Items.Remove(movieScanPath.SelectedItem);
                if (movieScanPath.Items.Count > 0) {
                    movieScanPath.SelectedIndex = 0;
                }
            }
        }

        private void seriesScanPath_TextChanged(object sender, EventArgs e) {
            btnSeriesScan.Enabled = Directory.Exists(seriesScanPath.Text);
        }

        private void btnAppAbout_Click(object sender, EventArgs e) {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void optionSeriesParser_SelectedIndexChanged(object sender, EventArgs e) {
            Settings.SetValue(SettingKeys.SeriesParser, optionSeriesParser.SelectedItem.ToString());
        }

        private void groupSeries_Enter(object sender, EventArgs e) {

        }

        private void btnSeriesScanStop_Click(object sender, EventArgs e) {
            if (parseSeriesThread != null && parseSeriesThread.IsAlive) {
                parseSeriesThread.Abort();
                series_ScanDone();
            }
        }

        private void btnMovieScanStop_Click(object sender, EventArgs e) {
            if (parseMoviesThread != null && parseMoviesThread.IsAlive) {
                parseMoviesThread.Abort();
                movie_ScanDone();
            }
        }

        private void optionMovieParser_SelectedIndexChanged(object sender, EventArgs e) {
            Settings.SetValue(SettingKeys.MoviesParser, optionMovieParser.SelectedItem.ToString());
        }

        private void btnHelp_Click(object sender, EventArgs e) {
            MessageBox.Show(
                "Here are some tipps for using the application:\n\n" +
                "The dropTarget:\n" +
                "========================================\n" +
                "If you press Alt, Shift or Control, the dropTarget behaves differently:\n" +
                "Alt+Drop:\nRename the file locally. This means the file will just be renamed and not copied or movied to a new folder.\n\n" +
                "Ctrl+Drop:\nCopy the file with the new name to the series/movies location. The old file will remain untouched.\n\n" +
                "Shift+Drop:\nMovie the file to the series/movies location with the new name (Default).\n\n" +
                "\n", 
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void sysTrayMenu_MouseDoubleClick(object sender, MouseEventArgs e) {
            sysTrayOpen_Click(sender, e);
        }


    }
}