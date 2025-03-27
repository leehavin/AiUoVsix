namespace AiUoVsix.Command.NugetPublish
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtConfigPath = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtNupkgs = new System.Windows.Forms.TextBox();
            this.cbxNuget = new System.Windows.Forms.ComboBox();
            this.cbxVersion = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.btnNupkg = new System.Windows.Forms.Button();
            this.btnAddNuget = new System.Windows.Forms.Button();
            this.btnVsixPublish = new System.Windows.Forms.Button();
            this.btnPublish = new System.Windows.Forms.Button();
            this.bgwMain = new System.ComponentModel.BackgroundWorker();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgwMain = new System.Windows.Forms.DataGridView();
            this.dgwcProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgwcVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgwcProjectType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgwcPublishType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgwcStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgwcProjectPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmenuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BuildDeployToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtVsixPath = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.txtVsixToken = new System.Windows.Forms.TextBox();
            this.txtVsixConfig = new System.Windows.Forms.TextBox();
            this.txtMSBuild = new System.Windows.Forms.TextBox();
            this.btnVsixProject = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtVsixExePath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVsixProject = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtDetail = new System.Windows.Forms.TextBox();
            this.chkGit = new System.Windows.Forms.CheckBox();
            this.chkRelease = new System.Windows.Forms.CheckBox();
            this.dlgAdd = new System.Windows.Forms.OpenFileDialog();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnDelSource = new System.Windows.Forms.Button();
            this.btnEditSource = new System.Windows.Forms.Button();
            this.btnAddSource = new System.Windows.Forms.Button();
            this.txtBeta = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbxMode = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwMain)).BeginInit();
            this.cmenuGrid.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "配置文件路径：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "nupkgs 路径：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nuget Source：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "发布时增加版本：";
            // 
            // txtConfigPath
            // 
            this.txtConfigPath.Location = new System.Drawing.Point(143, 6);
            this.txtConfigPath.Name = "txtConfigPath";
            this.txtConfigPath.Size = new System.Drawing.Size(579, 25);
            this.txtConfigPath.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(728, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 27);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存配置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNupkgs
            // 
            this.txtNupkgs.Location = new System.Drawing.Point(143, 37);
            this.txtNupkgs.Name = "txtNupkgs";
            this.txtNupkgs.Size = new System.Drawing.Size(579, 25);
            this.txtNupkgs.TabIndex = 6;
            // 
            // cbxNuget
            // 
            this.cbxNuget.FormattingEnabled = true;
            this.cbxNuget.Location = new System.Drawing.Point(143, 71);
            this.cbxNuget.Name = "cbxNuget";
            this.cbxNuget.Size = new System.Drawing.Size(295, 23);
            this.cbxNuget.TabIndex = 7;
            this.cbxNuget.SelectedIndexChanged += new System.EventHandler(this.cbxNuget_SelectedIndexChanged);
            // 
            // cbxVersion
            // 
            this.cbxVersion.FormattingEnabled = true;
            this.cbxVersion.Items.AddRange(new object[] {
            "不自动升级包",
            "build - 兼容Bug修复",
            "minor - 兼容增加功能",
            "major - 不兼容大更新",
            "发布beta版本"});
            this.cbxVersion.Location = new System.Drawing.Point(143, 102);
            this.cbxVersion.Name = "cbxVersion";
            this.cbxVersion.Size = new System.Drawing.Size(179, 23);
            this.cbxVersion.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnNupkg);
            this.groupBox1.Controls.Add(this.btnAddNuget);
            this.groupBox1.Controls.Add(this.btnVsixPublish);
            this.groupBox1.Controls.Add(this.btnPublish);
            this.groupBox1.Location = new System.Drawing.Point(13, 129);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(809, 64);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(730, 24);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(487, 24);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(239, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(409, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "执行进度：";
            // 
            // btnNupkg
            // 
            this.btnNupkg.Location = new System.Drawing.Point(309, 18);
            this.btnNupkg.Name = "btnNupkg";
            this.btnNupkg.Size = new System.Drawing.Size(94, 34);
            this.btnNupkg.TabIndex = 3;
            this.btnNupkg.Text = "发布nupkg";
            this.btnNupkg.UseVisualStyleBackColor = true;
            this.btnNupkg.Click += new System.EventHandler(this.btnNupkg_Click);
            // 
            // btnAddNuget
            // 
            this.btnAddNuget.Location = new System.Drawing.Point(209, 18);
            this.btnAddNuget.Name = "btnAddNuget";
            this.btnAddNuget.Size = new System.Drawing.Size(94, 34);
            this.btnAddNuget.TabIndex = 2;
            this.btnAddNuget.Text = "添加nuget";
            this.btnAddNuget.UseVisualStyleBackColor = true;
            this.btnAddNuget.Click += new System.EventHandler(this.btnAddNuget_Click);
            // 
            // btnVsixPublish
            // 
            this.btnVsixPublish.Location = new System.Drawing.Point(109, 18);
            this.btnVsixPublish.Name = "btnVsixPublish";
            this.btnVsixPublish.Size = new System.Drawing.Size(94, 34);
            this.btnVsixPublish.TabIndex = 1;
            this.btnVsixPublish.Text = "VSIX发布";
            this.btnVsixPublish.UseVisualStyleBackColor = true;
            this.btnVsixPublish.Click += new System.EventHandler(this.btnVsixPublish_Click);
            // 
            // btnPublish
            // 
            this.btnPublish.Location = new System.Drawing.Point(9, 18);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(94, 34);
            this.btnPublish.TabIndex = 0;
            this.btnPublish.Text = "NuGet发布";
            this.btnPublish.UseVisualStyleBackColor = true;
            this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
            // 
            // bgwMain
            // 
            this.bgwMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwMain_DoWork);
            this.bgwMain.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwMain_ProgressChanged);
            this.bgwMain.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwMain_RunWorkerCompleted);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPage1);
            this.tabMain.Controls.Add(this.tabPage2);
            this.tabMain.Controls.Add(this.tabPage3);
            this.tabMain.Controls.Add(this.tabPage4);
            this.tabMain.Location = new System.Drawing.Point(7, 199);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(815, 435);
            this.tabMain.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgwMain);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(807, 406);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Nuget发布";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgwMain
            // 
            this.dgwMain.AllowUserToAddRows = false;
            this.dgwMain.AllowUserToDeleteRows = false;
            this.dgwMain.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgwMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgwMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgwMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgwMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgwcProjectName,
            this.dgwcVersion,
            this.dgwcProjectType,
            this.dgwcPublishType,
            this.dgwcStatus,
            this.dgwcProjectPath});
            this.dgwMain.ContextMenuStrip = this.cmenuGrid;
            this.dgwMain.Location = new System.Drawing.Point(3, 3);
            this.dgwMain.MultiSelect = false;
            this.dgwMain.Name = "dgwMain";
            this.dgwMain.ReadOnly = true;
            this.dgwMain.RowHeadersVisible = false;
            this.dgwMain.RowHeadersWidth = 51;
            this.dgwMain.RowTemplate.Height = 27;
            this.dgwMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwMain.Size = new System.Drawing.Size(801, 400);
            this.dgwMain.TabIndex = 0; 
            // 
            // dgwcProjectName
            // 
            this.dgwcProjectName.DataPropertyName = "ProjectName";
            this.dgwcProjectName.HeaderText = "项目名称";
            this.dgwcProjectName.MinimumWidth = 6;
            this.dgwcProjectName.Name = "dgwcProjectName";
            this.dgwcProjectName.ReadOnly = true;
            this.dgwcProjectName.Width = 96;
            // 
            // dgwcVersion
            // 
            this.dgwcVersion.DataPropertyName = "Version";
            this.dgwcVersion.HeaderText = "项目版本";
            this.dgwcVersion.MinimumWidth = 6;
            this.dgwcVersion.Name = "dgwcVersion";
            this.dgwcVersion.ReadOnly = true;
            this.dgwcVersion.Width = 96;
            // 
            // dgwcProjectType
            // 
            this.dgwcProjectType.DataPropertyName = "ProjectTypeString";
            this.dgwcProjectType.HeaderText = "运行时";
            this.dgwcProjectType.MinimumWidth = 6;
            this.dgwcProjectType.Name = "dgwcProjectType";
            this.dgwcProjectType.ReadOnly = true;
            this.dgwcProjectType.Width = 81;
            // 
            // dgwcPublishType
            // 
            this.dgwcPublishType.DataPropertyName = "PublishType";
            this.dgwcPublishType.HeaderText = "发布类型";
            this.dgwcPublishType.MinimumWidth = 6;
            this.dgwcPublishType.Name = "dgwcPublishType";
            this.dgwcPublishType.ReadOnly = true;
            this.dgwcPublishType.Width = 96;
            // 
            // dgwcStatus
            // 
            this.dgwcStatus.DataPropertyName = "Status";
            this.dgwcStatus.HeaderText = "状态";
            this.dgwcStatus.MinimumWidth = 6;
            this.dgwcStatus.Name = "dgwcStatus";
            this.dgwcStatus.ReadOnly = true;
            this.dgwcStatus.Width = 66;
            // 
            // dgwcProjectPath
            // 
            this.dgwcProjectPath.DataPropertyName = "ProjectPath";
            this.dgwcProjectPath.HeaderText = "项目路径";
            this.dgwcProjectPath.MinimumWidth = 6;
            this.dgwcProjectPath.Name = "dgwcProjectPath";
            this.dgwcProjectPath.ReadOnly = true;
            this.dgwcProjectPath.Width = 96;
            // 
            // cmenuGrid
            // 
            this.cmenuGrid.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmenuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuItem,
            this.BuildDeployToolStripMenuItem});
            this.cmenuGrid.Name = "cmenuGrid";
            this.cmenuGrid.Size = new System.Drawing.Size(154, 52); 
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.DeleteToolStripMenuItem.Text = "删除";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // BuildDeployToolStripMenuItem
            // 
            this.BuildDeployToolStripMenuItem.Name = "BuildDeployToolStripMenuItem";
            this.BuildDeployToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.BuildDeployToolStripMenuItem.Text = "编译并发布";
            this.BuildDeployToolStripMenuItem.Click += new System.EventHandler(this.BuildDeployToolStripMenuItem_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtVsixPath);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.button10);
            this.tabPage2.Controls.Add(this.button9);
            this.tabPage2.Controls.Add(this.button8);
            this.tabPage2.Controls.Add(this.textBox8);
            this.tabPage2.Controls.Add(this.textBox7);
            this.tabPage2.Controls.Add(this.txtVsixToken);
            this.tabPage2.Controls.Add(this.txtVsixConfig);
            this.tabPage2.Controls.Add(this.txtMSBuild);
            this.tabPage2.Controls.Add(this.btnVsixProject);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.txtVsixExePath);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txtVsixProject);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(807, 406);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "VSIX发布";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtVsixPath
            // 
            this.txtVsixPath.Location = new System.Drawing.Point(201, 110);
            this.txtVsixPath.Name = "txtVsixPath";
            this.txtVsixPath.Size = new System.Drawing.Size(509, 25);
            this.txtVsixPath.TabIndex = 20;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 166);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 15);
            this.label13.TabIndex = 19;
            this.label13.Text = "MBuild路径";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(719, 209);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(84, 30);
            this.button10.TabIndex = 18;
            this.button10.Text = "浏览";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(719, 158);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(84, 30);
            this.button9.TabIndex = 17;
            this.button9.Text = "浏览";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(719, 107);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(84, 30);
            this.button8.TabIndex = 16;
            this.button8.Text = "浏览";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(201, 365);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(509, 25);
            this.textBox8.TabIndex = 15;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(201, 314);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(510, 25);
            this.textBox7.TabIndex = 14;
            // 
            // txtVsixToken
            // 
            this.txtVsixToken.Location = new System.Drawing.Point(201, 263);
            this.txtVsixToken.Name = "txtVsixToken";
            this.txtVsixToken.Size = new System.Drawing.Size(510, 25);
            this.txtVsixToken.TabIndex = 13;
            // 
            // txtVsixConfig
            // 
            this.txtVsixConfig.Location = new System.Drawing.Point(201, 212);
            this.txtVsixConfig.Name = "txtVsixConfig";
            this.txtVsixConfig.Size = new System.Drawing.Size(509, 25);
            this.txtVsixConfig.TabIndex = 12;
            // 
            // txtMSBuild
            // 
            this.txtMSBuild.Location = new System.Drawing.Point(201, 161);
            this.txtMSBuild.Name = "txtMSBuild";
            this.txtMSBuild.Size = new System.Drawing.Size(510, 25);
            this.txtMSBuild.TabIndex = 11;
            // 
            // btnVsixProject
            // 
            this.btnVsixProject.Location = new System.Drawing.Point(719, 56);
            this.btnVsixProject.Name = "btnVsixProject";
            this.btnVsixProject.Size = new System.Drawing.Size(84, 30);
            this.btnVsixProject.TabIndex = 10;
            this.btnVsixProject.Text = "浏览";
            this.btnVsixProject.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(719, 5);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(84, 30);
            this.button6.TabIndex = 9;
            this.button6.Text = "浏览";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 370);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(114, 15);
            this.label12.TabIndex = 8;
            this.label12.Text = "查看发布的VSIX";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 319);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(189, 15);
            this.label11.TabIndex = 7;
            this.label11.Text = "获取PersonalAccessToken";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 268);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(159, 15);
            this.label10.TabIndex = 6;
            this.label10.Text = "PersonalAccessToken";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 217);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 15);
            this.label9.TabIndex = 5;
            this.label9.Text = "VSIXPublish路径";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(157, 15);
            this.label8.TabIndex = 4;
            this.label8.Text = "PublishManifest路径";
            // 
            // txtVsixExePath
            // 
            this.txtVsixExePath.Location = new System.Drawing.Point(81, 59);
            this.txtVsixExePath.Name = "txtVsixExePath";
            this.txtVsixExePath.Size = new System.Drawing.Size(630, 25);
            this.txtVsixExePath.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "VSIX路径";
            // 
            // txtVsixProject
            // 
            this.txtVsixProject.Location = new System.Drawing.Point(81, 8);
            this.txtVsixProject.Name = "txtVsixProject";
            this.txtVsixProject.Size = new System.Drawing.Size(630, 25);
            this.txtVsixProject.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "项目路径";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtOutput);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(807, 406);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "输出";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtOutput
            // 
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Location = new System.Drawing.Point(3, 3);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(801, 400);
            this.txtOutput.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.txtDetail);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(807, 406);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "详情";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtDetail
            // 
            this.txtDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetail.Location = new System.Drawing.Point(3, 3);
            this.txtDetail.Multiline = true;
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.Size = new System.Drawing.Size(801, 400);
            this.txtDetail.TabIndex = 0;
            // 
            // chkGit
            // 
            this.chkGit.AutoSize = true;
            this.chkGit.Location = new System.Drawing.Point(561, 104);
            this.chkGit.Name = "chkGit";
            this.chkGit.Size = new System.Drawing.Size(113, 19);
            this.chkGit.TabIndex = 11;
            this.chkGit.Text = "自动提交GIT";
            this.chkGit.UseVisualStyleBackColor = true;
            // 
            // chkRelease
            // 
            this.chkRelease.AutoSize = true;
            this.chkRelease.Location = new System.Drawing.Point(680, 104);
            this.chkRelease.Name = "chkRelease";
            this.chkRelease.Size = new System.Drawing.Size(115, 19);
            this.chkRelease.TabIndex = 12;
            this.chkRelease.Text = "发布Release";
            this.chkRelease.UseVisualStyleBackColor = true;
            // 
            // dlgAdd
            // 
            this.dlgAdd.DefaultExt = "csproj";
            this.dlgAdd.FileName = "openFileDialog1";
            this.dlgAdd.Filter = "C# 项目文件|*.csproj|执行文件|*.exe";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.Location = new System.Drawing.Point(10, 639);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(249, 20);
            this.lblResult.TabIndex = 13;
            this.lblResult.Text = "请选择项目进行编译或发布";
            // 
            // btnDelSource
            // 
            this.btnDelSource.Location = new System.Drawing.Point(457, 71);
            this.btnDelSource.Name = "btnDelSource";
            this.btnDelSource.Size = new System.Drawing.Size(75, 23);
            this.btnDelSource.TabIndex = 14;
            this.btnDelSource.Text = "删除";
            this.btnDelSource.UseVisualStyleBackColor = true;
            this.btnDelSource.Click += new System.EventHandler(this.btnDelSource_Click);
            // 
            // btnEditSource
            // 
            this.btnEditSource.Location = new System.Drawing.Point(538, 71);
            this.btnEditSource.Name = "btnEditSource";
            this.btnEditSource.Size = new System.Drawing.Size(75, 23);
            this.btnEditSource.TabIndex = 15;
            this.btnEditSource.Text = "修改";
            this.btnEditSource.UseVisualStyleBackColor = true;
            this.btnEditSource.Click += new System.EventHandler(this.btnEditSource_Click);
            // 
            // btnAddSource
            // 
            this.btnAddSource.Location = new System.Drawing.Point(619, 71);
            this.btnAddSource.Name = "btnAddSource";
            this.btnAddSource.Size = new System.Drawing.Size(75, 23);
            this.btnAddSource.TabIndex = 16;
            this.btnAddSource.Text = "添加";
            this.btnAddSource.UseVisualStyleBackColor = true;
            this.btnAddSource.Click += new System.EventHandler(this.btnAddSource_Click);
            // 
            // txtBeta
            // 
            this.txtBeta.Location = new System.Drawing.Point(432, 102);
            this.txtBeta.Name = "txtBeta";
            this.txtBeta.Size = new System.Drawing.Size(100, 25);
            this.txtBeta.TabIndex = 17;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(363, 108);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 15);
            this.label14.TabIndex = 18;
            this.label14.Text = "后缀";
            // 
            // cbxMode
            // 
            this.cbxMode.FormattingEnabled = true;
            this.cbxMode.Items.AddRange(new object[] {
            "不生成符号包",
            "生成符号包",
            "嵌入符号包"});
            this.cbxMode.Location = new System.Drawing.Point(701, 72);
            this.cbxMode.Name = "cbxMode";
            this.cbxMode.Size = new System.Drawing.Size(121, 23);
            this.cbxMode.TabIndex = 19;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 672);
            this.Controls.Add(this.cbxMode);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtBeta);
            this.Controls.Add(this.btnAddSource);
            this.Controls.Add(this.btnEditSource);
            this.Controls.Add(this.btnDelSource);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.chkRelease);
            this.Controls.Add(this.chkGit);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbxVersion);
            this.Controls.Add(this.cbxNuget);
            this.Controls.Add(this.txtNupkgs);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtConfigPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Nuget发布";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwMain)).EndInit();
            this.cmenuGrid.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtConfigPath;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtNupkgs;
        private System.Windows.Forms.ComboBox cbxNuget;
        private System.Windows.Forms.ComboBox cbxVersion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker bgwMain;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnNupkg;
        private System.Windows.Forms.Button btnAddNuget;
        private System.Windows.Forms.Button btnVsixPublish;
        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtVsixProject;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox txtVsixToken;
        private System.Windows.Forms.TextBox txtVsixConfig;
        private System.Windows.Forms.TextBox txtMSBuild;
        private System.Windows.Forms.Button btnVsixProject;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtVsixExePath;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtVsixPath;
        private System.Windows.Forms.CheckBox chkGit;
        private System.Windows.Forms.CheckBox chkRelease;
        private System.Windows.Forms.DataGridView dgwMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgwcProjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgwcVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgwcProjectType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgwcPublishType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgwcStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgwcProjectPath;
        private System.Windows.Forms.ContextMenuStrip cmenuGrid;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BuildDeployToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog dlgAdd;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelSource;
        private System.Windows.Forms.Button btnEditSource;
        private System.Windows.Forms.Button btnAddSource;
        private System.Windows.Forms.TextBox txtBeta;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbxMode;
    }
}

