namespace MediaRenamer
{
    partial class mainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSeries = new System.Windows.Forms.TabPage();
            this.tvRenameEpisodes = new System.Windows.Forms.Button();
            this.tvSelNone = new System.Windows.Forms.Button();
            this.tvSelAll = new System.Windows.Forms.Button();
            this.scanSeriesList = new System.Windows.Forms.ListView();
            this.headSeries = new System.Windows.Forms.ColumnHeader();
            this.headSeason = new System.Windows.Forms.ColumnHeader();
            this.headEpisode = new System.Windows.Forms.ColumnHeader();
            this.headTitle = new System.Windows.Forms.ColumnHeader();
            this.headFilename = new System.Windows.Forms.ColumnHeader();
            this.headNewname = new System.Windows.Forms.ColumnHeader();
            this.contextRename = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextOptionRename = new System.Windows.Forms.ToolStripMenuItem();
            this.groupSeries = new System.Windows.Forms.GroupBox();
            this.btnSeriesBrowse = new System.Windows.Forms.Button();
            this.scanSeriesProgressbar = new System.Windows.Forms.ProgressBar();
            this.btnSeriesScan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.seriesScanPath = new System.Windows.Forms.ComboBox();
            this.btnSeriesScanStop = new System.Windows.Forms.Button();
            this.tabMovies = new System.Windows.Forms.TabPage();
            this.movieRename = new System.Windows.Forms.Button();
            this.movieSelNone = new System.Windows.Forms.Button();
            this.movieSelAll = new System.Windows.Forms.Button();
            this.scanMovieList = new System.Windows.Forms.ListView();
            this.headMovie = new System.Windows.Forms.ColumnHeader();
            this.headYear = new System.Windows.Forms.ColumnHeader();
            this.headMoviefile = new System.Windows.Forms.ColumnHeader();
            this.headNewfile = new System.Windows.Forms.ColumnHeader();
            this.groupMovies = new System.Windows.Forms.GroupBox();
            this.btnMoviesBrowse = new System.Windows.Forms.Button();
            this.scanMovieProgressbar = new System.Windows.Forms.ProgressBar();
            this.btnMovieScan = new System.Windows.Forms.Button();
            this.moviePathLabel = new System.Windows.Forms.Label();
            this.movieScanPath = new System.Windows.Forms.ComboBox();
            this.btnMovieScanStop = new System.Windows.Forms.Button();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.optionGroupOthers = new System.Windows.Forms.GroupBox();
            this.optionDropTarget = new System.Windows.Forms.CheckBox();
            this.optionSysTray = new System.Windows.Forms.CheckBox();
            this.optionStartMinimized = new System.Windows.Forms.CheckBox();
            this.optionGroupMovies = new System.Windows.Forms.GroupBox();
            this.optionMovieParser = new System.Windows.Forms.ComboBox();
            this.labelMovieSource = new System.Windows.Forms.Label();
            this.btnMovePath = new System.Windows.Forms.Button();
            this.option_movieTargetLocation = new System.Windows.Forms.TextBox();
            this.option_moveMovies = new System.Windows.Forms.CheckBox();
            this.option_movieFormat = new System.Windows.Forms.TextBox();
            this.contextProposals = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labelMovieOutpu = new System.Windows.Forms.Label();
            this.optionGroupSeries = new System.Windows.Forms.GroupBox();
            this.optionSeriesParser = new System.Windows.Forms.ComboBox();
            this.labelTVSource = new System.Windows.Forms.Label();
            this.option_seriesFormat = new System.Windows.Forms.TextBox();
            this.labelTVOutput = new System.Windows.Forms.Label();
            this.tabWatch = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_watchType = new System.Windows.Forms.Label();
            this.label_watchPath = new System.Windows.Forms.Label();
            this.addWatchType = new System.Windows.Forms.ComboBox();
            this.addWatchFolder = new System.Windows.Forms.Button();
            this.addWatchPath = new System.Windows.Forms.TextBox();
            this.watchedFolders = new System.Windows.Forms.ListBox();
            this.runWatcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopWatcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteSelectedWatcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.sysTrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sysTrayOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.displayDropTargetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sysTrayExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAppAbout = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl.SuspendLayout();
            this.tabSeries.SuspendLayout();
            this.contextRename.SuspendLayout();
            this.groupSeries.SuspendLayout();
            this.tabMovies.SuspendLayout();
            this.groupMovies.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.optionGroupOthers.SuspendLayout();
            this.optionGroupMovies.SuspendLayout();
            this.optionGroupSeries.SuspendLayout();
            this.sysTrayMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabSeries);
            this.tabControl.Controls.Add(this.tabMovies);
            this.tabControl.Controls.Add(this.tabOptions);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabSeries
            // 
            this.tabSeries.Controls.Add(this.tvRenameEpisodes);
            this.tabSeries.Controls.Add(this.tvSelNone);
            this.tabSeries.Controls.Add(this.tvSelAll);
            this.tabSeries.Controls.Add(this.scanSeriesList);
            this.tabSeries.Controls.Add(this.groupSeries);
            resources.ApplyResources(this.tabSeries, "tabSeries");
            this.tabSeries.Name = "tabSeries";
            this.tabSeries.UseVisualStyleBackColor = true;
            // 
            // tvRenameEpisodes
            // 
            resources.ApplyResources(this.tvRenameEpisodes, "tvRenameEpisodes");
            this.tvRenameEpisodes.Name = "tvRenameEpisodes";
            this.tvRenameEpisodes.UseVisualStyleBackColor = true;
            this.tvRenameEpisodes.Click += new System.EventHandler(this.tvRenameEpisodes_Click);
            // 
            // tvSelNone
            // 
            resources.ApplyResources(this.tvSelNone, "tvSelNone");
            this.tvSelNone.Name = "tvSelNone";
            this.tvSelNone.UseVisualStyleBackColor = true;
            this.tvSelNone.Click += new System.EventHandler(this.tvSelNone_Click);
            // 
            // tvSelAll
            // 
            resources.ApplyResources(this.tvSelAll, "tvSelAll");
            this.tvSelAll.Name = "tvSelAll";
            this.tvSelAll.UseVisualStyleBackColor = true;
            this.tvSelAll.Click += new System.EventHandler(this.tvSelAll_Click);
            // 
            // scanSeriesList
            // 
            resources.ApplyResources(this.scanSeriesList, "scanSeriesList");
            this.scanSeriesList.CheckBoxes = true;
            this.scanSeriesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.headSeries,
            this.headSeason,
            this.headEpisode,
            this.headTitle,
            this.headFilename,
            this.headNewname});
            this.scanSeriesList.ContextMenuStrip = this.contextRename;
            this.scanSeriesList.FullRowSelect = true;
            this.scanSeriesList.Name = "scanSeriesList";
            this.scanSeriesList.ShowGroups = false;
            this.scanSeriesList.UseCompatibleStateImageBehavior = false;
            this.scanSeriesList.View = System.Windows.Forms.View.Details;
            // 
            // headSeries
            // 
            resources.ApplyResources(this.headSeries, "headSeries");
            // 
            // headSeason
            // 
            resources.ApplyResources(this.headSeason, "headSeason");
            // 
            // headEpisode
            // 
            resources.ApplyResources(this.headEpisode, "headEpisode");
            // 
            // headTitle
            // 
            resources.ApplyResources(this.headTitle, "headTitle");
            // 
            // headFilename
            // 
            resources.ApplyResources(this.headFilename, "headFilename");
            // 
            // headNewname
            // 
            resources.ApplyResources(this.headNewname, "headNewname");
            // 
            // contextRename
            // 
            this.contextRename.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextOptionRename});
            this.contextRename.Name = "contextRename";
            resources.ApplyResources(this.contextRename, "contextRename");
            this.contextRename.Opening += new System.ComponentModel.CancelEventHandler(this.contextRename_Opening);
            // 
            // contextOptionRename
            // 
            this.contextOptionRename.Name = "contextOptionRename";
            resources.ApplyResources(this.contextOptionRename, "contextOptionRename");
            this.contextOptionRename.Click += new System.EventHandler(this.contextOptionRename_Click);
            // 
            // groupSeries
            // 
            resources.ApplyResources(this.groupSeries, "groupSeries");
            this.groupSeries.Controls.Add(this.btnSeriesBrowse);
            this.groupSeries.Controls.Add(this.scanSeriesProgressbar);
            this.groupSeries.Controls.Add(this.btnSeriesScan);
            this.groupSeries.Controls.Add(this.label1);
            this.groupSeries.Controls.Add(this.seriesScanPath);
            this.groupSeries.Controls.Add(this.btnSeriesScanStop);
            this.groupSeries.Name = "groupSeries";
            this.groupSeries.TabStop = false;
            this.groupSeries.Enter += new System.EventHandler(this.groupSeries_Enter);
            // 
            // btnSeriesBrowse
            // 
            resources.ApplyResources(this.btnSeriesBrowse, "btnSeriesBrowse");
            this.btnSeriesBrowse.Name = "btnSeriesBrowse";
            this.btnSeriesBrowse.UseVisualStyleBackColor = true;
            this.btnSeriesBrowse.Click += new System.EventHandler(this.btnSeriesBrowse_Click);
            // 
            // scanSeriesProgressbar
            // 
            resources.ApplyResources(this.scanSeriesProgressbar, "scanSeriesProgressbar");
            this.scanSeriesProgressbar.Maximum = 0;
            this.scanSeriesProgressbar.Name = "scanSeriesProgressbar";
            // 
            // btnSeriesScan
            // 
            resources.ApplyResources(this.btnSeriesScan, "btnSeriesScan");
            this.btnSeriesScan.Name = "btnSeriesScan";
            this.btnSeriesScan.Tag = "";
            this.btnSeriesScan.UseVisualStyleBackColor = true;
            this.btnSeriesScan.Click += new System.EventHandler(this.btnSeriesScan_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // seriesScanPath
            // 
            resources.ApplyResources(this.seriesScanPath, "seriesScanPath");
            this.seriesScanPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.seriesScanPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.seriesScanPath.FormattingEnabled = true;
            this.seriesScanPath.Name = "seriesScanPath";
            this.seriesScanPath.SelectedIndexChanged += new System.EventHandler(this.seriesScanPath_TextUpdate);
            this.seriesScanPath.Leave += new System.EventHandler(this.seriesScanPath_Leave);
            this.seriesScanPath.TextChanged += new System.EventHandler(this.seriesScanPath_TextChanged);
            // 
            // btnSeriesScanStop
            // 
            resources.ApplyResources(this.btnSeriesScanStop, "btnSeriesScanStop");
            this.btnSeriesScanStop.Name = "btnSeriesScanStop";
            this.btnSeriesScanStop.UseVisualStyleBackColor = true;
            this.btnSeriesScanStop.Click += new System.EventHandler(this.btnSeriesScanStop_Click);
            // 
            // tabMovies
            // 
            this.tabMovies.Controls.Add(this.movieRename);
            this.tabMovies.Controls.Add(this.movieSelNone);
            this.tabMovies.Controls.Add(this.movieSelAll);
            this.tabMovies.Controls.Add(this.scanMovieList);
            this.tabMovies.Controls.Add(this.groupMovies);
            resources.ApplyResources(this.tabMovies, "tabMovies");
            this.tabMovies.Name = "tabMovies";
            this.tabMovies.UseVisualStyleBackColor = true;
            // 
            // movieRename
            // 
            resources.ApplyResources(this.movieRename, "movieRename");
            this.movieRename.Name = "movieRename";
            this.movieRename.UseVisualStyleBackColor = true;
            this.movieRename.Click += new System.EventHandler(this.movieRename_Click);
            // 
            // movieSelNone
            // 
            resources.ApplyResources(this.movieSelNone, "movieSelNone");
            this.movieSelNone.Name = "movieSelNone";
            this.movieSelNone.UseVisualStyleBackColor = true;
            this.movieSelNone.Click += new System.EventHandler(this.movieSelNone_Click);
            // 
            // movieSelAll
            // 
            resources.ApplyResources(this.movieSelAll, "movieSelAll");
            this.movieSelAll.Name = "movieSelAll";
            this.movieSelAll.UseVisualStyleBackColor = true;
            this.movieSelAll.Click += new System.EventHandler(this.movieSelAll_Click);
            // 
            // scanMovieList
            // 
            resources.ApplyResources(this.scanMovieList, "scanMovieList");
            this.scanMovieList.CheckBoxes = true;
            this.scanMovieList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.headMovie,
            this.headYear,
            this.headMoviefile,
            this.headNewfile});
            this.scanMovieList.ContextMenuStrip = this.contextRename;
            this.scanMovieList.FullRowSelect = true;
            this.scanMovieList.Name = "scanMovieList";
            this.scanMovieList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.scanMovieList.UseCompatibleStateImageBehavior = false;
            this.scanMovieList.View = System.Windows.Forms.View.Details;
            // 
            // headMovie
            // 
            resources.ApplyResources(this.headMovie, "headMovie");
            // 
            // headYear
            // 
            resources.ApplyResources(this.headYear, "headYear");
            // 
            // headMoviefile
            // 
            resources.ApplyResources(this.headMoviefile, "headMoviefile");
            // 
            // headNewfile
            // 
            resources.ApplyResources(this.headNewfile, "headNewfile");
            // 
            // groupMovies
            // 
            resources.ApplyResources(this.groupMovies, "groupMovies");
            this.groupMovies.Controls.Add(this.btnMoviesBrowse);
            this.groupMovies.Controls.Add(this.scanMovieProgressbar);
            this.groupMovies.Controls.Add(this.btnMovieScan);
            this.groupMovies.Controls.Add(this.moviePathLabel);
            this.groupMovies.Controls.Add(this.movieScanPath);
            this.groupMovies.Controls.Add(this.btnMovieScanStop);
            this.groupMovies.Name = "groupMovies";
            this.groupMovies.TabStop = false;
            // 
            // btnMoviesBrowse
            // 
            resources.ApplyResources(this.btnMoviesBrowse, "btnMoviesBrowse");
            this.btnMoviesBrowse.Name = "btnMoviesBrowse";
            this.btnMoviesBrowse.UseVisualStyleBackColor = true;
            this.btnMoviesBrowse.Click += new System.EventHandler(this.btnPathMovies_Click);
            // 
            // scanMovieProgressbar
            // 
            resources.ApplyResources(this.scanMovieProgressbar, "scanMovieProgressbar");
            this.scanMovieProgressbar.Maximum = 0;
            this.scanMovieProgressbar.Name = "scanMovieProgressbar";
            // 
            // btnMovieScan
            // 
            resources.ApplyResources(this.btnMovieScan, "btnMovieScan");
            this.btnMovieScan.Name = "btnMovieScan";
            this.btnMovieScan.UseVisualStyleBackColor = true;
            this.btnMovieScan.Click += new System.EventHandler(this.btnMovieScan_Click);
            // 
            // moviePathLabel
            // 
            resources.ApplyResources(this.moviePathLabel, "moviePathLabel");
            this.moviePathLabel.Name = "moviePathLabel";
            // 
            // movieScanPath
            // 
            resources.ApplyResources(this.movieScanPath, "movieScanPath");
            this.movieScanPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.movieScanPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.movieScanPath.FormattingEnabled = true;
            this.movieScanPath.Name = "movieScanPath";
            this.movieScanPath.SelectedIndexChanged += new System.EventHandler(this.movieScanPath_TextUpdate);
            this.movieScanPath.Leave += new System.EventHandler(this.movieScanPath_Leave);
            this.movieScanPath.TextChanged += new System.EventHandler(this.movieScanPath_TextUpdate);
            // 
            // btnMovieScanStop
            // 
            resources.ApplyResources(this.btnMovieScanStop, "btnMovieScanStop");
            this.btnMovieScanStop.Name = "btnMovieScanStop";
            this.btnMovieScanStop.UseVisualStyleBackColor = true;
            this.btnMovieScanStop.Click += new System.EventHandler(this.btnMovieScanStop_Click);
            // 
            // tabOptions
            // 
            resources.ApplyResources(this.tabOptions, "tabOptions");
            this.tabOptions.Controls.Add(this.optionGroupOthers);
            this.tabOptions.Controls.Add(this.optionGroupMovies);
            this.tabOptions.Controls.Add(this.optionGroupSeries);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // optionGroupOthers
            // 
            resources.ApplyResources(this.optionGroupOthers, "optionGroupOthers");
            this.optionGroupOthers.Controls.Add(this.optionDropTarget);
            this.optionGroupOthers.Controls.Add(this.optionSysTray);
            this.optionGroupOthers.Controls.Add(this.optionStartMinimized);
            this.optionGroupOthers.Name = "optionGroupOthers";
            this.optionGroupOthers.TabStop = false;
            // 
            // optionDropTarget
            // 
            resources.ApplyResources(this.optionDropTarget, "optionDropTarget");
            this.optionDropTarget.Name = "optionDropTarget";
            this.optionDropTarget.UseVisualStyleBackColor = true;
            this.optionDropTarget.CheckedChanged += new System.EventHandler(this.optionDropTarget_CheckedChanged);
            // 
            // optionSysTray
            // 
            resources.ApplyResources(this.optionSysTray, "optionSysTray");
            this.optionSysTray.Name = "optionSysTray";
            this.optionSysTray.UseVisualStyleBackColor = true;
            this.optionSysTray.CheckedChanged += new System.EventHandler(this.optionSysTray_CheckedChanged);
            // 
            // optionStartMinimized
            // 
            resources.ApplyResources(this.optionStartMinimized, "optionStartMinimized");
            this.optionStartMinimized.Name = "optionStartMinimized";
            this.optionStartMinimized.UseVisualStyleBackColor = true;
            this.optionStartMinimized.CheckedChanged += new System.EventHandler(this.optionStartMinimized_CheckedChanged);
            // 
            // optionGroupMovies
            // 
            resources.ApplyResources(this.optionGroupMovies, "optionGroupMovies");
            this.optionGroupMovies.Controls.Add(this.optionMovieParser);
            this.optionGroupMovies.Controls.Add(this.labelMovieSource);
            this.optionGroupMovies.Controls.Add(this.btnMovePath);
            this.optionGroupMovies.Controls.Add(this.option_movieTargetLocation);
            this.optionGroupMovies.Controls.Add(this.option_moveMovies);
            this.optionGroupMovies.Controls.Add(this.option_movieFormat);
            this.optionGroupMovies.Controls.Add(this.labelMovieOutpu);
            this.optionGroupMovies.Name = "optionGroupMovies";
            this.optionGroupMovies.TabStop = false;
            // 
            // optionMovieParser
            // 
            this.optionMovieParser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.optionMovieParser.FormattingEnabled = true;
            resources.ApplyResources(this.optionMovieParser, "optionMovieParser");
            this.optionMovieParser.Name = "optionMovieParser";
            this.optionMovieParser.SelectedIndexChanged += new System.EventHandler(this.optionMovieParser_SelectedIndexChanged);
            // 
            // labelMovieSource
            // 
            resources.ApplyResources(this.labelMovieSource, "labelMovieSource");
            this.labelMovieSource.Name = "labelMovieSource";
            // 
            // btnMovePath
            // 
            resources.ApplyResources(this.btnMovePath, "btnMovePath");
            this.btnMovePath.Name = "btnMovePath";
            this.btnMovePath.UseVisualStyleBackColor = true;
            this.btnMovePath.Click += new System.EventHandler(this.btnMovePath_Click);
            // 
            // option_movieTargetLocation
            // 
            resources.ApplyResources(this.option_movieTargetLocation, "option_movieTargetLocation");
            this.option_movieTargetLocation.Name = "option_movieTargetLocation";
            this.option_movieTargetLocation.TextChanged += new System.EventHandler(this.option_movieTargetLocation_TextChanged);
            // 
            // option_moveMovies
            // 
            resources.ApplyResources(this.option_moveMovies, "option_moveMovies");
            this.option_moveMovies.Name = "option_moveMovies";
            this.option_moveMovies.UseVisualStyleBackColor = true;
            this.option_moveMovies.CheckedChanged += new System.EventHandler(this.option_moveMovies_CheckedChanged);
            // 
            // option_movieFormat
            // 
            this.option_movieFormat.AutoCompleteCustomSource.AddRange(new string[] {
            resources.GetString("option_movieFormat.AutoCompleteCustomSource")});
            this.option_movieFormat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.option_movieFormat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.option_movieFormat.ContextMenuStrip = this.contextProposals;
            resources.ApplyResources(this.option_movieFormat, "option_movieFormat");
            this.option_movieFormat.Name = "option_movieFormat";
            this.option_movieFormat.Leave += new System.EventHandler(this.option_movieFormat_Leave);
            this.option_movieFormat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.option_movieFormat_MouseDown);
            // 
            // contextProposals
            // 
            this.contextProposals.Name = "contextProposals";
            resources.ApplyResources(this.contextProposals, "contextProposals");
            this.contextProposals.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextProposals_ItemClicked);
            this.contextProposals.Opening += new System.ComponentModel.CancelEventHandler(this.contextProposals_Opening);
            // 
            // labelMovieOutpu
            // 
            resources.ApplyResources(this.labelMovieOutpu, "labelMovieOutpu");
            this.labelMovieOutpu.Name = "labelMovieOutpu";
            // 
            // optionGroupSeries
            // 
            resources.ApplyResources(this.optionGroupSeries, "optionGroupSeries");
            this.optionGroupSeries.Controls.Add(this.optionSeriesParser);
            this.optionGroupSeries.Controls.Add(this.labelTVSource);
            this.optionGroupSeries.Controls.Add(this.option_seriesFormat);
            this.optionGroupSeries.Controls.Add(this.labelTVOutput);
            this.optionGroupSeries.Name = "optionGroupSeries";
            this.optionGroupSeries.TabStop = false;
            // 
            // optionSeriesParser
            // 
            this.optionSeriesParser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.optionSeriesParser.FormattingEnabled = true;
            resources.ApplyResources(this.optionSeriesParser, "optionSeriesParser");
            this.optionSeriesParser.Name = "optionSeriesParser";
            this.optionSeriesParser.SelectedIndexChanged += new System.EventHandler(this.optionSeriesParser_SelectedIndexChanged);
            // 
            // labelTVSource
            // 
            resources.ApplyResources(this.labelTVSource, "labelTVSource");
            this.labelTVSource.Name = "labelTVSource";
            // 
            // option_seriesFormat
            // 
            this.option_seriesFormat.AutoCompleteCustomSource.AddRange(new string[] {
            resources.GetString("option_seriesFormat.AutoCompleteCustomSource"),
            resources.GetString("option_seriesFormat.AutoCompleteCustomSource1"),
            resources.GetString("option_seriesFormat.AutoCompleteCustomSource2"),
            resources.GetString("option_seriesFormat.AutoCompleteCustomSource3"),
            resources.GetString("option_seriesFormat.AutoCompleteCustomSource4"),
            resources.GetString("option_seriesFormat.AutoCompleteCustomSource5")});
            this.option_seriesFormat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.option_seriesFormat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.option_seriesFormat.ContextMenuStrip = this.contextProposals;
            resources.ApplyResources(this.option_seriesFormat, "option_seriesFormat");
            this.option_seriesFormat.Name = "option_seriesFormat";
            this.option_seriesFormat.Leave += new System.EventHandler(this.option_seriesFormat_Leave);
            this.option_seriesFormat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.option_seriesFormat_MouseDown);
            // 
            // labelTVOutput
            // 
            resources.ApplyResources(this.labelTVOutput, "labelTVOutput");
            this.labelTVOutput.Name = "labelTVOutput";
            // 
            // tabWatch
            // 
            resources.ApplyResources(this.tabWatch, "tabWatch");
            this.tabWatch.Name = "tabWatch";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label_watchType
            // 
            resources.ApplyResources(this.label_watchType, "label_watchType");
            this.label_watchType.Name = "label_watchType";
            // 
            // label_watchPath
            // 
            resources.ApplyResources(this.label_watchPath, "label_watchPath");
            this.label_watchPath.Name = "label_watchPath";
            // 
            // addWatchType
            // 
            resources.ApplyResources(this.addWatchType, "addWatchType");
            this.addWatchType.Name = "addWatchType";
            // 
            // addWatchFolder
            // 
            resources.ApplyResources(this.addWatchFolder, "addWatchFolder");
            this.addWatchFolder.Name = "addWatchFolder";
            // 
            // addWatchPath
            // 
            resources.ApplyResources(this.addWatchPath, "addWatchPath");
            this.addWatchPath.Name = "addWatchPath";
            // 
            // watchedFolders
            // 
            resources.ApplyResources(this.watchedFolders, "watchedFolders");
            this.watchedFolders.Name = "watchedFolders";
            // 
            // runWatcherToolStripMenuItem
            // 
            this.runWatcherToolStripMenuItem.Name = "runWatcherToolStripMenuItem";
            resources.ApplyResources(this.runWatcherToolStripMenuItem, "runWatcherToolStripMenuItem");
            // 
            // stopWatcherToolStripMenuItem
            // 
            this.stopWatcherToolStripMenuItem.Name = "stopWatcherToolStripMenuItem";
            resources.ApplyResources(this.stopWatcherToolStripMenuItem, "stopWatcherToolStripMenuItem");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // deleteSelectedWatcherToolStripMenuItem
            // 
            this.deleteSelectedWatcherToolStripMenuItem.Name = "deleteSelectedWatcherToolStripMenuItem";
            resources.ApplyResources(this.deleteSelectedWatcherToolStripMenuItem, "deleteSelectedWatcherToolStripMenuItem");
            // 
            // sysTrayMenu
            // 
            this.sysTrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sysTrayOpen,
            this.displayDropTargetToolStripMenuItem,
            this.toolStripSeparator1,
            this.sysTrayExit});
            this.sysTrayMenu.Name = "sysTrayMenu";
            resources.ApplyResources(this.sysTrayMenu, "sysTrayMenu");
            this.sysTrayMenu.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.sysTrayMenu_MouseDoubleClick);
            this.sysTrayMenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sysTrayMenu_MouseDoubleClick);
            // 
            // sysTrayOpen
            // 
            resources.ApplyResources(this.sysTrayOpen, "sysTrayOpen");
            this.sysTrayOpen.Name = "sysTrayOpen";
            this.sysTrayOpen.Click += new System.EventHandler(this.sysTrayOpen_Click);
            // 
            // displayDropTargetToolStripMenuItem
            // 
            this.displayDropTargetToolStripMenuItem.Name = "displayDropTargetToolStripMenuItem";
            resources.ApplyResources(this.displayDropTargetToolStripMenuItem, "displayDropTargetToolStripMenuItem");
            this.displayDropTargetToolStripMenuItem.Click += new System.EventHandler(this.displayDropTargetToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // sysTrayExit
            // 
            this.sysTrayExit.Name = "sysTrayExit";
            resources.ApplyResources(this.sysTrayExit, "sysTrayExit");
            this.sysTrayExit.Click += new System.EventHandler(this.sysTrayExit_Click);
            // 
            // btnAppAbout
            // 
            resources.ApplyResources(this.btnAppAbout, "btnAppAbout");
            this.btnAppAbout.Name = "btnAppAbout";
            this.btnAppAbout.UseVisualStyleBackColor = true;
            this.btnAppAbout.Click += new System.EventHandler(this.btnAppAbout_Click);
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.sysTrayMenu;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // mainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAppAbout);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.tabControl);
            this.Name = "mainForm";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.SizeChanged += new System.EventHandler(this.mainForm_SizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabSeries.ResumeLayout(false);
            this.contextRename.ResumeLayout(false);
            this.groupSeries.ResumeLayout(false);
            this.groupSeries.PerformLayout();
            this.tabMovies.ResumeLayout(false);
            this.groupMovies.ResumeLayout(false);
            this.groupMovies.PerformLayout();
            this.tabOptions.ResumeLayout(false);
            this.tabOptions.PerformLayout();
            this.optionGroupOthers.ResumeLayout(false);
            this.optionGroupOthers.PerformLayout();
            this.optionGroupMovies.ResumeLayout(false);
            this.optionGroupMovies.PerformLayout();
            this.optionGroupSeries.ResumeLayout(false);
            this.optionGroupSeries.PerformLayout();
            this.sysTrayMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSeries;
        private System.Windows.Forms.TabPage tabMovies;
        private System.Windows.Forms.TabPage tabWatch;
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox optionGroupSeries;
        private System.Windows.Forms.TextBox option_seriesFormat;
        private System.Windows.Forms.Label labelTVOutput;
        private System.Windows.Forms.ContextMenuStrip contextProposals;
        private System.Windows.Forms.Label labelTVSource;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ListBox watchedFolders;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button addWatchFolder;
        private System.Windows.Forms.TextBox addWatchPath;
        private System.Windows.Forms.Label label_watchType;
        private System.Windows.Forms.Label label_watchPath;
        private System.Windows.Forms.ComboBox addWatchType;
        private System.Windows.Forms.GroupBox groupMovies;
        private System.Windows.Forms.ComboBox movieScanPath;
        private System.Windows.Forms.ProgressBar scanMovieProgressbar;
        private System.Windows.Forms.Button btnMovieScan;
        private System.Windows.Forms.Label moviePathLabel;
        private System.Windows.Forms.GroupBox groupSeries;
        private System.Windows.Forms.ComboBox seriesScanPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSeriesScan;
        private System.Windows.Forms.ProgressBar scanSeriesProgressbar;
        private System.Windows.Forms.ContextMenuStrip contextRename;
        private System.Windows.Forms.ToolStripMenuItem contextOptionRename;
        private System.Windows.Forms.GroupBox optionGroupMovies;
        private System.Windows.Forms.TextBox option_movieFormat;
        private System.Windows.Forms.Label labelMovieOutpu;
        private System.Windows.Forms.GroupBox optionGroupOthers;
        private System.Windows.Forms.CheckBox optionStartMinimized;
        private System.Windows.Forms.CheckBox optionSysTray;
        private System.Windows.Forms.ContextMenuStrip sysTrayMenu;
        private System.Windows.Forms.ToolStripMenuItem sysTrayExit;
        private System.Windows.Forms.ToolStripMenuItem sysTrayOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ListView scanSeriesList;
        private System.Windows.Forms.ColumnHeader headFilename;
        private System.Windows.Forms.ColumnHeader headSeries;
        private System.Windows.Forms.ColumnHeader headSeason;
        private System.Windows.Forms.ColumnHeader headEpisode;
        private System.Windows.Forms.ColumnHeader headTitle;
        private System.Windows.Forms.ColumnHeader headNewname;
        private System.Windows.Forms.ListView scanMovieList;
        private System.Windows.Forms.ColumnHeader headMovie;
        private System.Windows.Forms.ColumnHeader headYear;
        private System.Windows.Forms.ColumnHeader headMoviefile;
        private System.Windows.Forms.ColumnHeader headNewfile;
        private System.Windows.Forms.ToolStripMenuItem runWatcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopWatcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedWatcherToolStripMenuItem;
        private System.Windows.Forms.Button tvRenameEpisodes;
        private System.Windows.Forms.Button tvSelNone;
        private System.Windows.Forms.Button tvSelAll;
        private System.Windows.Forms.Button movieRename;
        private System.Windows.Forms.Button movieSelNone;
        private System.Windows.Forms.Button movieSelAll;
        private System.Windows.Forms.Button btnSeriesBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnMoviesBrowse;
        private System.Windows.Forms.ToolStripMenuItem displayDropTargetToolStripMenuItem;
        private System.Windows.Forms.CheckBox optionDropTarget;
        private System.Windows.Forms.CheckBox option_moveMovies;
        private System.Windows.Forms.Button btnMovePath;
        private System.Windows.Forms.TextBox option_movieTargetLocation;
        private System.Windows.Forms.Button btnAppAbout;
        private System.Windows.Forms.ComboBox optionSeriesParser;
        private System.Windows.Forms.Button btnSeriesScanStop;
        private System.Windows.Forms.Button btnMovieScanStop;
        private System.Windows.Forms.Label labelMovieSource;
        private System.Windows.Forms.ComboBox optionMovieParser;
        private System.Windows.Forms.Button btnHelp;
    }
}

