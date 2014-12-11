namespace FileRenameTool
{
    partial class FRTForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.thmbBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.convertPathTextField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.inputFolderTb = new System.Windows.Forms.TextBox();
            this.chooseInDirButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.thumbnailFilesFormat = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.createThmbButton = new System.Windows.Forms.Button();
            this.stopThmbButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.infoLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.reloadDirectoryButton = new System.Windows.Forms.Button();
            this.overwriteThmbCb = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pubCycleGb = new System.Windows.Forms.GroupBox();
            this.sunCb = new System.Windows.Forms.CheckBox();
            this.satCb = new System.Windows.Forms.CheckBox();
            this.thuCb = new System.Windows.Forms.CheckBox();
            this.monCb = new System.Windows.Forms.CheckBox();
            this.friCb = new System.Windows.Forms.CheckBox();
            this.wedCb = new System.Windows.Forms.CheckBox();
            this.tueCb = new System.Windows.Forms.CheckBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.changeIssue = new System.Windows.Forms.Button();
            this.changeIssueAndFollowing = new System.Windows.Forms.Button();
            this.thmbSizeCb = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.applySpecSuffixButton = new System.Windows.Forms.Button();
            this.issueSpecSuffixTb = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.jumpToFirstIssueButton = new System.Windows.Forms.Button();
            this.jumpToLastIssueButton = new System.Windows.Forms.Button();
            this.jumpToPrevIssueButton = new System.Windows.Forms.Button();
            this.jumpToNextIssueButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.outputFolderTb = new System.Windows.Forms.TextBox();
            this.chooseOutDirButton = new System.Windows.Forms.Button();
            this.copyFilesButton = new System.Windows.Forms.Button();
            this.saveWorker = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveXMLButton = new System.Windows.Forms.Button();
            this.moveFilesCb = new System.Windows.Forms.CheckBox();
            this.createTopLevelCb = new System.Windows.Forms.CheckBox();
            this.yearLevelCb = new System.Windows.Forms.CheckBox();
            this.overwriteCb = new System.Windows.Forms.CheckBox();
            this.useInputFolderButton = new System.Windows.Forms.Button();
            this.summaryLabel = new System.Windows.Forms.Label();
            this.issuePrefixTb = new System.Windows.Forms.TextBox();
            this.issueSuffixTb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.useIssueAsFilePrefixCb = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.thumbsWidthCb = new System.Windows.Forms.ComboBox();
            this.prevPageButton = new System.Windows.Forms.Button();
            this.nextPageButton = new System.Windows.Forms.Button();
            this.nPagesLabel = new System.Windows.Forms.Label();
            this.currentPageTb = new System.Windows.Forms.TextBox();
            this.firstPageButton = new System.Windows.Forms.Button();
            this.lastPageButton = new System.Windows.Forms.Button();
            this.reloadPageButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.pagingPanel = new System.Windows.Forms.Panel();
            this.warningLabel = new System.Windows.Forms.Label();
            this.imView = new SebisControls.MyImageViewer();
            this.pubCycleGb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pagingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // thmbBackgroundWorker
            // 
            this.thmbBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.thmbBackgroundWorker_DoWork);
            this.thmbBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.thmbBackgroundWorker_ProgressChanged);
            this.thmbBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.thmbBackgroundWorker_RunWorkerCompleted);
            // 
            // convertPathTextField
            // 
            this.convertPathTextField.Location = new System.Drawing.Point(129, 6);
            this.convertPathTextField.Name = "convertPathTextField";
            this.convertPathTextField.Size = new System.Drawing.Size(655, 20);
            this.convertPathTextField.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "GraphicsMagick path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Input Folder:";
            // 
            // inputFolderTb
            // 
            this.inputFolderTb.Location = new System.Drawing.Point(129, 36);
            this.inputFolderTb.Name = "inputFolderTb";
            this.inputFolderTb.Size = new System.Drawing.Size(655, 20);
            this.inputFolderTb.TabIndex = 4;
            this.inputFolderTb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.inputFolderTb_KeyUp);
            // 
            // chooseInDirButton
            // 
            this.chooseInDirButton.AutoSize = true;
            this.chooseInDirButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chooseInDirButton.Location = new System.Drawing.Point(790, 36);
            this.chooseInDirButton.Name = "chooseInDirButton";
            this.chooseInDirButton.Size = new System.Drawing.Size(26, 23);
            this.chooseInDirButton.TabIndex = 5;
            this.chooseInDirButton.Text = "...";
            this.chooseInDirButton.UseVisualStyleBackColor = true;
            this.chooseInDirButton.Click += new System.EventHandler(this.chooseInputDirectoryButton_Click);
            // 
            // thumbnailFilesFormat
            // 
            this.thumbnailFilesFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.thumbnailFilesFormat.FormattingEnabled = true;
            this.thumbnailFilesFormat.Items.AddRange(new object[] {
            "jpg",
            "png",
            "gif"});
            this.thumbnailFilesFormat.Location = new System.Drawing.Point(90, 65);
            this.thumbnailFilesFormat.Name = "thumbnailFilesFormat";
            this.thumbnailFilesFormat.Size = new System.Drawing.Size(66, 21);
            this.thumbnailFilesFormat.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Thumbs format:";
            // 
            // createThmbButton
            // 
            this.createThmbButton.AutoSize = true;
            this.createThmbButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.createThmbButton.Location = new System.Drawing.Point(312, 65);
            this.createThmbButton.Name = "createThmbButton";
            this.createThmbButton.Size = new System.Drawing.Size(101, 23);
            this.createThmbButton.TabIndex = 35;
            this.createThmbButton.Text = "Create thumbnails";
            this.createThmbButton.UseVisualStyleBackColor = true;
            this.createThmbButton.Click += new System.EventHandler(this.createThmbButton_Click);
            // 
            // stopThmbButton
            // 
            this.stopThmbButton.AutoSize = true;
            this.stopThmbButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.stopThmbButton.Location = new System.Drawing.Point(419, 65);
            this.stopThmbButton.Name = "stopThmbButton";
            this.stopThmbButton.Size = new System.Drawing.Size(39, 23);
            this.stopThmbButton.TabIndex = 37;
            this.stopThmbButton.Text = "Stop";
            this.stopThmbButton.UseVisualStyleBackColor = true;
            this.stopThmbButton.Click += new System.EventHandler(this.stopThmbButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(464, 65);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(329, 20);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 38;
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(464, 92);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(35, 13);
            this.infoLabel.TabIndex = 39;
            this.infoLabel.Text = "label3";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(488, 421);
            this.flowLayoutPanel1.TabIndex = 40;
            this.flowLayoutPanel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.flowLayoutPanel1_Scroll);
            this.flowLayoutPanel1.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            this.flowLayoutPanel1.Resize += new System.EventHandler(this.flowLayoutPanel1_Resize);
            // 
            // reloadDirectoryButton
            // 
            this.reloadDirectoryButton.Location = new System.Drawing.Point(822, 36);
            this.reloadDirectoryButton.Name = "reloadDirectoryButton";
            this.reloadDirectoryButton.Size = new System.Drawing.Size(111, 23);
            this.reloadDirectoryButton.TabIndex = 41;
            this.reloadDirectoryButton.Text = "Reload directory";
            this.reloadDirectoryButton.UseVisualStyleBackColor = true;
            this.reloadDirectoryButton.Click += new System.EventHandler(this.reloadDirectoryButton_Click);
            // 
            // overwriteThmbCb
            // 
            this.overwriteThmbCb.AutoSize = true;
            this.overwriteThmbCb.Location = new System.Drawing.Point(799, 67);
            this.overwriteThmbCb.Name = "overwriteThmbCb";
            this.overwriteThmbCb.Size = new System.Drawing.Size(71, 17);
            this.overwriteThmbCb.TabIndex = 42;
            this.overwriteThmbCb.Text = "Overwrite";
            this.overwriteThmbCb.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Thumbnail display width:";
            // 
            // pubCycleGb
            // 
            this.pubCycleGb.Controls.Add(this.sunCb);
            this.pubCycleGb.Controls.Add(this.satCb);
            this.pubCycleGb.Controls.Add(this.thuCb);
            this.pubCycleGb.Controls.Add(this.monCb);
            this.pubCycleGb.Controls.Add(this.friCb);
            this.pubCycleGb.Controls.Add(this.wedCb);
            this.pubCycleGb.Controls.Add(this.tueCb);
            this.pubCycleGb.Location = new System.Drawing.Point(738, 110);
            this.pubCycleGb.Name = "pubCycleGb";
            this.pubCycleGb.Size = new System.Drawing.Size(238, 69);
            this.pubCycleGb.TabIndex = 46;
            this.pubCycleGb.TabStop = false;
            this.pubCycleGb.Text = "Publication Cycle";
            // 
            // sunCb
            // 
            this.sunCb.AutoSize = true;
            this.sunCb.Checked = true;
            this.sunCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sunCb.Location = new System.Drawing.Point(186, 42);
            this.sunCb.Name = "sunCb";
            this.sunCb.Size = new System.Drawing.Size(45, 17);
            this.sunCb.TabIndex = 7;
            this.sunCb.Text = "Sun";
            this.sunCb.UseVisualStyleBackColor = true;
            // 
            // satCb
            // 
            this.satCb.AutoSize = true;
            this.satCb.Checked = true;
            this.satCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.satCb.Location = new System.Drawing.Point(139, 42);
            this.satCb.Name = "satCb";
            this.satCb.Size = new System.Drawing.Size(42, 17);
            this.satCb.TabIndex = 6;
            this.satCb.Text = "Sat";
            this.satCb.UseVisualStyleBackColor = true;
            // 
            // thuCb
            // 
            this.thuCb.AutoSize = true;
            this.thuCb.Checked = true;
            this.thuCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.thuCb.Location = new System.Drawing.Point(18, 42);
            this.thuCb.Name = "thuCb";
            this.thuCb.Size = new System.Drawing.Size(45, 17);
            this.thuCb.TabIndex = 5;
            this.thuCb.Text = "Thu";
            this.thuCb.UseVisualStyleBackColor = true;
            // 
            // monCb
            // 
            this.monCb.AutoSize = true;
            this.monCb.Checked = true;
            this.monCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.monCb.Location = new System.Drawing.Point(18, 19);
            this.monCb.Name = "monCb";
            this.monCb.Size = new System.Drawing.Size(47, 17);
            this.monCb.TabIndex = 4;
            this.monCb.Text = "Mon";
            this.monCb.UseVisualStyleBackColor = true;
            // 
            // friCb
            // 
            this.friCb.AutoSize = true;
            this.friCb.Checked = true;
            this.friCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.friCb.Location = new System.Drawing.Point(80, 42);
            this.friCb.Name = "friCb";
            this.friCb.Size = new System.Drawing.Size(37, 17);
            this.friCb.TabIndex = 3;
            this.friCb.Text = "Fri";
            this.friCb.UseVisualStyleBackColor = true;
            // 
            // wedCb
            // 
            this.wedCb.AutoSize = true;
            this.wedCb.Checked = true;
            this.wedCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.wedCb.Location = new System.Drawing.Point(139, 19);
            this.wedCb.Name = "wedCb";
            this.wedCb.Size = new System.Drawing.Size(49, 17);
            this.wedCb.TabIndex = 2;
            this.wedCb.Text = "Wed";
            this.wedCb.UseVisualStyleBackColor = true;
            // 
            // tueCb
            // 
            this.tueCb.AutoSize = true;
            this.tueCb.Checked = true;
            this.tueCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tueCb.Location = new System.Drawing.Point(80, 19);
            this.tueCb.Name = "tueCb";
            this.tueCb.Size = new System.Drawing.Size(45, 17);
            this.tueCb.TabIndex = 1;
            this.tueCb.Text = "Tue";
            this.tueCb.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(6, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 48;
            // 
            // changeIssue
            // 
            this.changeIssue.AutoSize = true;
            this.changeIssue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.changeIssue.Location = new System.Drawing.Point(6, 67);
            this.changeIssue.Name = "changeIssue";
            this.changeIssue.Size = new System.Drawing.Size(156, 23);
            this.changeIssue.TabIndex = 49;
            this.changeIssue.Text = "Change date for current issue";
            this.toolTip1.SetToolTip(this.changeIssue, "Changes the date for all images of the currently selected issue");
            this.changeIssue.UseVisualStyleBackColor = true;
            this.changeIssue.Click += new System.EventHandler(this.changeIssue_Click);
            // 
            // changeIssueAndFollowing
            // 
            this.changeIssueAndFollowing.AutoSize = true;
            this.changeIssueAndFollowing.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.changeIssueAndFollowing.Location = new System.Drawing.Point(168, 67);
            this.changeIssueAndFollowing.Name = "changeIssueAndFollowing";
            this.changeIssueAndFollowing.Size = new System.Drawing.Size(226, 23);
            this.changeIssueAndFollowing.TabIndex = 50;
            this.changeIssueAndFollowing.Text = "Change date for current and following issues";
            this.toolTip1.SetToolTip(this.changeIssueAndFollowing, "Changes the date for all images of the currently selected issue and all following" +
        " issues according to the publication cycle");
            this.changeIssueAndFollowing.UseVisualStyleBackColor = true;
            this.changeIssueAndFollowing.Click += new System.EventHandler(this.changeIssueAndFollowing_Click);
            // 
            // thmbSizeCb
            // 
            this.thmbSizeCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.thmbSizeCb.FormattingEnabled = true;
            this.thmbSizeCb.Location = new System.Drawing.Point(129, 163);
            this.thmbSizeCb.Name = "thmbSizeCb";
            this.thmbSizeCb.Size = new System.Drawing.Size(99, 21);
            this.thmbSizeCb.TabIndex = 52;
            this.thmbSizeCb.SelectedIndexChanged += new System.EventHandler(this.thmbSizeCb_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 231);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.imView);
            this.splitContainer1.Panel2.Controls.Add(this.applySpecSuffixButton);
            this.splitContainer1.Panel2.Controls.Add(this.issueSpecSuffixTb);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.jumpToFirstIssueButton);
            this.splitContainer1.Panel2.Controls.Add(this.jumpToLastIssueButton);
            this.splitContainer1.Panel2.Controls.Add(this.jumpToPrevIssueButton);
            this.splitContainer1.Panel2.Controls.Add(this.jumpToNextIssueButton);
            this.splitContainer1.Panel2.Controls.Add(this.dateTimePicker1);
            this.splitContainer1.Panel2.Controls.Add(this.changeIssueAndFollowing);
            this.splitContainer1.Panel2.Controls.Add(this.changeIssue);
            this.splitContainer1.Size = new System.Drawing.Size(978, 421);
            this.splitContainer1.SplitterDistance = 488;
            this.splitContainer1.TabIndex = 53;
            // 
            // applySpecSuffixButton
            // 
            this.applySpecSuffixButton.AutoSize = true;
            this.applySpecSuffixButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.applySpecSuffixButton.Location = new System.Drawing.Point(423, 13);
            this.applySpecSuffixButton.Name = "applySpecSuffixButton";
            this.applySpecSuffixButton.Size = new System.Drawing.Size(43, 23);
            this.applySpecSuffixButton.TabIndex = 81;
            this.applySpecSuffixButton.Text = "Apply";
            this.toolTip1.SetToolTip(this.applySpecSuffixButton, "Applies the issue specific suffix to all images of this issue");
            this.applySpecSuffixButton.UseVisualStyleBackColor = true;
            this.applySpecSuffixButton.Click += new System.EventHandler(this.applySpecSuffixButton_Click);
            // 
            // issueSpecSuffixTb
            // 
            this.issueSpecSuffixTb.Location = new System.Drawing.Point(319, 15);
            this.issueSpecSuffixTb.Name = "issueSpecSuffixTb";
            this.issueSpecSuffixTb.Size = new System.Drawing.Size(98, 20);
            this.issueSpecSuffixTb.TabIndex = 80;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(211, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 80;
            this.label9.Text = "Issue specific suffix:";
            // 
            // jumpToFirstIssueButton
            // 
            this.jumpToFirstIssueButton.AutoSize = true;
            this.jumpToFirstIssueButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.jumpToFirstIssueButton.Location = new System.Drawing.Point(6, 38);
            this.jumpToFirstIssueButton.Name = "jumpToFirstIssueButton";
            this.jumpToFirstIssueButton.Size = new System.Drawing.Size(100, 23);
            this.jumpToFirstIssueButton.TabIndex = 70;
            this.jumpToFirstIssueButton.Text = "Jump to first issue";
            this.toolTip1.SetToolTip(this.jumpToFirstIssueButton, "Selects and focuses the previous issue according to the current selection");
            this.jumpToFirstIssueButton.UseVisualStyleBackColor = true;
            this.jumpToFirstIssueButton.Click += new System.EventHandler(this.jumpToFirstIssueButton_Click);
            // 
            // jumpToLastIssueButton
            // 
            this.jumpToLastIssueButton.AutoSize = true;
            this.jumpToLastIssueButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.jumpToLastIssueButton.Location = new System.Drawing.Point(352, 38);
            this.jumpToLastIssueButton.Name = "jumpToLastIssueButton";
            this.jumpToLastIssueButton.Size = new System.Drawing.Size(100, 23);
            this.jumpToLastIssueButton.TabIndex = 69;
            this.jumpToLastIssueButton.Text = "Jump to last issue";
            this.toolTip1.SetToolTip(this.jumpToLastIssueButton, "Selects and focuses the next issue according to the current selection");
            this.jumpToLastIssueButton.UseVisualStyleBackColor = true;
            this.jumpToLastIssueButton.Click += new System.EventHandler(this.jumpToLastIssueButton_Click);
            // 
            // jumpToPrevIssueButton
            // 
            this.jumpToPrevIssueButton.AutoSize = true;
            this.jumpToPrevIssueButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.jumpToPrevIssueButton.Location = new System.Drawing.Point(112, 38);
            this.jumpToPrevIssueButton.Name = "jumpToPrevIssueButton";
            this.jumpToPrevIssueButton.Size = new System.Drawing.Size(124, 23);
            this.jumpToPrevIssueButton.TabIndex = 53;
            this.jumpToPrevIssueButton.Text = "Jump to previous issue";
            this.toolTip1.SetToolTip(this.jumpToPrevIssueButton, "Selects and focuses the previous issue according to the current selection");
            this.jumpToPrevIssueButton.UseVisualStyleBackColor = true;
            this.jumpToPrevIssueButton.Click += new System.EventHandler(this.jumpToPrevIssueButton_Click);
            // 
            // jumpToNextIssueButton
            // 
            this.jumpToNextIssueButton.AutoSize = true;
            this.jumpToNextIssueButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.jumpToNextIssueButton.Location = new System.Drawing.Point(242, 38);
            this.jumpToNextIssueButton.Name = "jumpToNextIssueButton";
            this.jumpToNextIssueButton.Size = new System.Drawing.Size(104, 23);
            this.jumpToNextIssueButton.TabIndex = 52;
            this.jumpToNextIssueButton.Text = "Jump to next issue";
            this.toolTip1.SetToolTip(this.jumpToNextIssueButton, "Selects and focuses the next issue according to the current selection");
            this.jumpToNextIssueButton.UseVisualStyleBackColor = true;
            this.jumpToNextIssueButton.Click += new System.EventHandler(this.jumpToNextIssueButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "Output Folder:";
            // 
            // outputFolderTb
            // 
            this.outputFolderTb.Location = new System.Drawing.Point(76, 112);
            this.outputFolderTb.Name = "outputFolderTb";
            this.outputFolderTb.Size = new System.Drawing.Size(423, 20);
            this.outputFolderTb.TabIndex = 55;
            this.toolTip1.SetToolTip(this.outputFolderTb, "The output folder where the resulting images are copied to");
            // 
            // chooseOutDirButton
            // 
            this.chooseOutDirButton.AutoSize = true;
            this.chooseOutDirButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chooseOutDirButton.Location = new System.Drawing.Point(505, 110);
            this.chooseOutDirButton.Name = "chooseOutDirButton";
            this.chooseOutDirButton.Size = new System.Drawing.Size(26, 23);
            this.chooseOutDirButton.TabIndex = 56;
            this.chooseOutDirButton.Text = "...";
            this.chooseOutDirButton.UseVisualStyleBackColor = true;
            this.chooseOutDirButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // copyFilesButton
            // 
            this.copyFilesButton.AutoSize = true;
            this.copyFilesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.copyFilesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyFilesButton.Location = new System.Drawing.Point(661, 112);
            this.copyFilesButton.Name = "copyFilesButton";
            this.copyFilesButton.Size = new System.Drawing.Size(72, 23);
            this.copyFilesButton.TabIndex = 57;
            this.copyFilesButton.Text = "Copy files";
            this.copyFilesButton.UseVisualStyleBackColor = true;
            this.copyFilesButton.Click += new System.EventHandler(this.copyFilesButton_Click);
            // 
            // saveWorker
            // 
            this.saveWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.saveWorker_DoWork);
            // 
            // saveXMLButton
            // 
            this.saveXMLButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveXMLButton.Location = new System.Drawing.Point(652, 141);
            this.saveXMLButton.Name = "saveXMLButton";
            this.saveXMLButton.Size = new System.Drawing.Size(81, 23);
            this.saveXMLButton.TabIndex = 60;
            this.saveXMLButton.Text = "Save XML";
            this.toolTip1.SetToolTip(this.saveXMLButton, "Save the XML file for the input folder to remember edit state");
            this.saveXMLButton.UseVisualStyleBackColor = true;
            this.saveXMLButton.Click += new System.EventHandler(this.saveXMLButton_Click);
            // 
            // moveFilesCb
            // 
            this.moveFilesCb.AutoSize = true;
            this.moveFilesCb.Location = new System.Drawing.Point(567, 139);
            this.moveFilesCb.Name = "moveFilesCb";
            this.moveFilesCb.Size = new System.Drawing.Size(74, 17);
            this.moveFilesCb.TabIndex = 66;
            this.moveFilesCb.Text = "Move files";
            this.toolTip1.SetToolTip(this.moveFilesCb, "Files are not copied but moved into the output directory (use this option for sim" +
        "ple renaming)");
            this.moveFilesCb.UseVisualStyleBackColor = true;
            // 
            // createTopLevelCb
            // 
            this.createTopLevelCb.AutoSize = true;
            this.createTopLevelCb.Checked = true;
            this.createTopLevelCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.createTopLevelCb.Location = new System.Drawing.Point(321, 138);
            this.createTopLevelCb.Name = "createTopLevelCb";
            this.createTopLevelCb.Size = new System.Drawing.Size(129, 17);
            this.createTopLevelCb.TabIndex = 67;
            this.createTopLevelCb.Text = "Create top level folder";
            this.toolTip1.SetToolTip(this.createTopLevelCb, "Create an additional top level folder with the name of the input folder");
            this.createTopLevelCb.UseVisualStyleBackColor = true;
            // 
            // yearLevelCb
            // 
            this.yearLevelCb.AutoSize = true;
            this.yearLevelCb.Checked = true;
            this.yearLevelCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.yearLevelCb.Location = new System.Drawing.Point(456, 139);
            this.yearLevelCb.Name = "yearLevelCb";
            this.yearLevelCb.Size = new System.Drawing.Size(105, 17);
            this.yearLevelCb.TabIndex = 65;
            this.yearLevelCb.Text = "Create year level";
            this.toolTip1.SetToolTip(this.yearLevelCb, "Create a year level folder in the hierarchy");
            this.yearLevelCb.UseVisualStyleBackColor = true;
            // 
            // overwriteCb
            // 
            this.overwriteCb.AutoSize = true;
            this.overwriteCb.Checked = true;
            this.overwriteCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.overwriteCb.Location = new System.Drawing.Point(486, 163);
            this.overwriteCb.Name = "overwriteCb";
            this.overwriteCb.Size = new System.Drawing.Size(71, 17);
            this.overwriteCb.TabIndex = 81;
            this.overwriteCb.Text = "Overwrite";
            this.toolTip1.SetToolTip(this.overwriteCb, "Overwrite existing files");
            this.overwriteCb.UseVisualStyleBackColor = true;
            // 
            // useInputFolderButton
            // 
            this.useInputFolderButton.Location = new System.Drawing.Point(537, 111);
            this.useInputFolderButton.Name = "useInputFolderButton";
            this.useInputFolderButton.Size = new System.Drawing.Size(100, 22);
            this.useInputFolderButton.TabIndex = 59;
            this.useInputFolderButton.Text = "Use input folder";
            this.useInputFolderButton.UseVisualStyleBackColor = true;
            this.useInputFolderButton.Click += new System.EventHandler(this.useInputFolderButton_Click);
            // 
            // summaryLabel
            // 
            this.summaryLabel.AutoSize = true;
            this.summaryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.summaryLabel.Location = new System.Drawing.Point(4, 92);
            this.summaryLabel.Name = "summaryLabel";
            this.summaryLabel.Size = new System.Drawing.Size(59, 13);
            this.summaryLabel.TabIndex = 61;
            this.summaryLabel.Text = "info label";
            // 
            // issuePrefixTb
            // 
            this.issuePrefixTb.Location = new System.Drawing.Point(109, 135);
            this.issuePrefixTb.Name = "issuePrefixTb";
            this.issuePrefixTb.Size = new System.Drawing.Size(100, 20);
            this.issuePrefixTb.TabIndex = 62;
            // 
            // issueSuffixTb
            // 
            this.issueSuffixTb.Location = new System.Drawing.Point(215, 134);
            this.issueSuffixTb.Name = "issueSuffixTb";
            this.issueSuffixTb.Size = new System.Drawing.Size(100, 20);
            this.issueSuffixTb.TabIndex = 63;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 64;
            this.label5.Text = "Issue pre/suffix:";
            // 
            // useIssueAsFilePrefixCb
            // 
            this.useIssueAsFilePrefixCb.AutoSize = true;
            this.useIssueAsFilePrefixCb.Checked = true;
            this.useIssueAsFilePrefixCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useIssueAsFilePrefixCb.Location = new System.Drawing.Point(321, 163);
            this.useIssueAsFilePrefixCb.Name = "useIssueAsFilePrefixCb";
            this.useIssueAsFilePrefixCb.Size = new System.Drawing.Size(159, 17);
            this.useIssueAsFilePrefixCb.TabIndex = 68;
            this.useIssueAsFilePrefixCb.Text = "Use issue name as file prefix";
            this.useIssueAsFilePrefixCb.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(162, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 69;
            this.label6.Text = "Thumbs width:";
            // 
            // thumbsWidthCb
            // 
            this.thumbsWidthCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.thumbsWidthCb.FormattingEnabled = true;
            this.thumbsWidthCb.Items.AddRange(new object[] {
            "50",
            "100",
            "150",
            "200",
            "250",
            "300",
            "350",
            "400"});
            this.thumbsWidthCb.Location = new System.Drawing.Point(240, 65);
            this.thumbsWidthCb.Name = "thumbsWidthCb";
            this.thumbsWidthCb.Size = new System.Drawing.Size(66, 21);
            this.thumbsWidthCb.TabIndex = 70;
            // 
            // prevPageButton
            // 
            this.prevPageButton.AutoSize = true;
            this.prevPageButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.prevPageButton.Location = new System.Drawing.Point(45, 3);
            this.prevPageButton.Name = "prevPageButton";
            this.prevPageButton.Size = new System.Drawing.Size(58, 23);
            this.prevPageButton.TabIndex = 71;
            this.prevPageButton.Text = "Previous";
            this.prevPageButton.UseVisualStyleBackColor = true;
            this.prevPageButton.Click += new System.EventHandler(this.prevPageButton_Click);
            // 
            // nextPageButton
            // 
            this.nextPageButton.AutoSize = true;
            this.nextPageButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nextPageButton.Location = new System.Drawing.Point(109, 3);
            this.nextPageButton.Name = "nextPageButton";
            this.nextPageButton.Size = new System.Drawing.Size(39, 23);
            this.nextPageButton.TabIndex = 72;
            this.nextPageButton.Text = "Next";
            this.nextPageButton.UseVisualStyleBackColor = true;
            this.nextPageButton.Click += new System.EventHandler(this.nextPageButton_Click);
            // 
            // nPagesLabel
            // 
            this.nPagesLabel.AutoSize = true;
            this.nPagesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nPagesLabel.Location = new System.Drawing.Point(373, 8);
            this.nPagesLabel.Name = "nPagesLabel";
            this.nPagesLabel.Size = new System.Drawing.Size(20, 13);
            this.nPagesLabel.TabIndex = 73;
            this.nPagesLabel.Text = "/0";
            // 
            // currentPageTb
            // 
            this.currentPageTb.Location = new System.Drawing.Point(320, 5);
            this.currentPageTb.Name = "currentPageTb";
            this.currentPageTb.Size = new System.Drawing.Size(47, 20);
            this.currentPageTb.TabIndex = 74;
            this.currentPageTb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.currentPageTb_KeyDown);
            // 
            // firstPageButton
            // 
            this.firstPageButton.AutoSize = true;
            this.firstPageButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.firstPageButton.Location = new System.Drawing.Point(3, 3);
            this.firstPageButton.Name = "firstPageButton";
            this.firstPageButton.Size = new System.Drawing.Size(36, 23);
            this.firstPageButton.TabIndex = 75;
            this.firstPageButton.Text = "First";
            this.firstPageButton.UseVisualStyleBackColor = true;
            this.firstPageButton.Click += new System.EventHandler(this.firstPageButton_Click);
            // 
            // lastPageButton
            // 
            this.lastPageButton.AutoSize = true;
            this.lastPageButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lastPageButton.Location = new System.Drawing.Point(154, 3);
            this.lastPageButton.Name = "lastPageButton";
            this.lastPageButton.Size = new System.Drawing.Size(37, 23);
            this.lastPageButton.TabIndex = 76;
            this.lastPageButton.Text = "Last";
            this.lastPageButton.UseVisualStyleBackColor = true;
            this.lastPageButton.Click += new System.EventHandler(this.lastPageButton_Click);
            // 
            // reloadPageButton
            // 
            this.reloadPageButton.AutoSize = true;
            this.reloadPageButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.reloadPageButton.Location = new System.Drawing.Point(197, 3);
            this.reloadPageButton.Name = "reloadPageButton";
            this.reloadPageButton.Size = new System.Drawing.Size(79, 23);
            this.reloadPageButton.TabIndex = 77;
            this.reloadPageButton.Text = "Reload Page";
            this.reloadPageButton.UseVisualStyleBackColor = true;
            this.reloadPageButton.Click += new System.EventHandler(this.reloadPageButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(282, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 78;
            this.label8.Text = "Page";
            // 
            // pagingPanel
            // 
            this.pagingPanel.AutoSize = true;
            this.pagingPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pagingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pagingPanel.Controls.Add(this.firstPageButton);
            this.pagingPanel.Controls.Add(this.nPagesLabel);
            this.pagingPanel.Controls.Add(this.currentPageTb);
            this.pagingPanel.Controls.Add(this.label8);
            this.pagingPanel.Controls.Add(this.prevPageButton);
            this.pagingPanel.Controls.Add(this.reloadPageButton);
            this.pagingPanel.Controls.Add(this.nextPageButton);
            this.pagingPanel.Controls.Add(this.lastPageButton);
            this.pagingPanel.Location = new System.Drawing.Point(3, 194);
            this.pagingPanel.Name = "pagingPanel";
            this.pagingPanel.Size = new System.Drawing.Size(398, 31);
            this.pagingPanel.TabIndex = 79;
            // 
            // warningLabel
            // 
            this.warningLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.warningLabel.AutoSize = true;
            this.warningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warningLabel.ForeColor = System.Drawing.Color.Red;
            this.warningLabel.Location = new System.Drawing.Point(518, 182);
            this.warningLabel.Name = "warningLabel";
            this.warningLabel.Size = new System.Drawing.Size(35, 13);
            this.warningLabel.TabIndex = 80;
            this.warningLabel.Text = "label3";
            // 
            // imView
            // 
            this.imView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imView.Location = new System.Drawing.Point(6, 93);
            this.imView.Name = "imView";
            this.imView.Size = new System.Drawing.Size(477, 325);
            this.imView.TabIndex = 82;
            // 
            // FRTForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 652);
            this.Controls.Add(this.overwriteCb);
            this.Controls.Add(this.warningLabel);
            this.Controls.Add(this.pagingPanel);
            this.Controls.Add(this.thumbsWidthCb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.useIssueAsFilePrefixCb);
            this.Controls.Add(this.createTopLevelCb);
            this.Controls.Add(this.moveFilesCb);
            this.Controls.Add(this.yearLevelCb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.issueSuffixTb);
            this.Controls.Add(this.issuePrefixTb);
            this.Controls.Add(this.summaryLabel);
            this.Controls.Add(this.saveXMLButton);
            this.Controls.Add(this.useInputFolderButton);
            this.Controls.Add(this.copyFilesButton);
            this.Controls.Add(this.chooseOutDirButton);
            this.Controls.Add(this.outputFolderTb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.thmbSizeCb);
            this.Controls.Add(this.pubCycleGb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.overwriteThmbCb);
            this.Controls.Add(this.reloadDirectoryButton);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.stopThmbButton);
            this.Controls.Add(this.createThmbButton);
            this.Controls.Add(this.thumbnailFilesFormat);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chooseInDirButton);
            this.Controls.Add(this.inputFolderTb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.convertPathTextField);
            this.KeyPreview = true;
            this.Name = "FRTForm";
            this.Text = "File-Rename-Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FRTForm_KeyUp);
            this.pubCycleGb.ResumeLayout(false);
            this.pubCycleGb.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pagingPanel.ResumeLayout(false);
            this.pagingPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker thmbBackgroundWorker;
        private System.Windows.Forms.TextBox convertPathTextField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox inputFolderTb;
        private System.Windows.Forms.Button chooseInDirButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox thumbnailFilesFormat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button createThmbButton;
        private System.Windows.Forms.Button stopThmbButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button reloadDirectoryButton;
        private System.Windows.Forms.CheckBox overwriteThmbCb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox pubCycleGb;
        private System.Windows.Forms.CheckBox sunCb;
        private System.Windows.Forms.CheckBox satCb;
        private System.Windows.Forms.CheckBox thuCb;
        private System.Windows.Forms.CheckBox monCb;
        private System.Windows.Forms.CheckBox friCb;
        private System.Windows.Forms.CheckBox wedCb;
        private System.Windows.Forms.CheckBox tueCb;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button changeIssue;
        private System.Windows.Forms.Button changeIssueAndFollowing;
        private System.Windows.Forms.ComboBox thmbSizeCb;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox outputFolderTb;
        private System.Windows.Forms.Button chooseOutDirButton;
        private System.Windows.Forms.Button copyFilesButton;
        private System.ComponentModel.BackgroundWorker saveWorker;
        private System.Windows.Forms.Button jumpToNextIssueButton;
        private System.Windows.Forms.Button jumpToPrevIssueButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button useInputFolderButton;
        private System.Windows.Forms.Button saveXMLButton;
        private System.Windows.Forms.Label summaryLabel;
        private System.Windows.Forms.TextBox issuePrefixTb;
        private System.Windows.Forms.TextBox issueSuffixTb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox yearLevelCb;
        private System.Windows.Forms.CheckBox moveFilesCb;
        private System.Windows.Forms.CheckBox createTopLevelCb;
        private System.Windows.Forms.CheckBox useIssueAsFilePrefixCb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox thumbsWidthCb;
        private System.Windows.Forms.Button prevPageButton;
        private System.Windows.Forms.Button nextPageButton;
        private System.Windows.Forms.Label nPagesLabel;
        private System.Windows.Forms.TextBox currentPageTb;
        private System.Windows.Forms.Button firstPageButton;
        private System.Windows.Forms.Button lastPageButton;
        private System.Windows.Forms.Button reloadPageButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pagingPanel;
        private System.Windows.Forms.Button jumpToFirstIssueButton;
        private System.Windows.Forms.Button jumpToLastIssueButton;
        private System.Windows.Forms.TextBox issueSpecSuffixTb;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button applySpecSuffixButton;
        private System.Windows.Forms.Label warningLabel;
        private System.Windows.Forms.CheckBox overwriteCb;
        private SebisControls.MyImageViewer imView;
    }
}

