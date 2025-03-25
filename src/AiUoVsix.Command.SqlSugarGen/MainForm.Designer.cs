namespace AiUoVsix.Command.SqlSugarGen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxDbs = new System.Windows.Forms.ComboBox();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.cbxPartial = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnOpenOutput = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.cbxSubPath = new System.Windows.Forms.CheckBox();
            this.cbxConfigId = new System.Windows.Forms.CheckBox();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.btnGen = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cbxAutoSelect = new System.Windows.Forms.CheckBox();
            this.cbxUseCache = new System.Windows.Forms.CheckBox();
            this.cbxFilter = new System.Windows.Forms.ComboBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.bgwGen = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择数据库：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "保存路径：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Partial类：";
            // 
            // cbxDbs
            // 
            this.cbxDbs.FormattingEnabled = true;
            this.cbxDbs.Location = new System.Drawing.Point(127, 21);
            this.cbxDbs.Name = "cbxDbs";
            this.cbxDbs.Size = new System.Drawing.Size(308, 23);
            this.cbxDbs.TabIndex = 3;
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(127, 58);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(308, 25);
            this.txtOutputPath.TabIndex = 4;
            // 
            // cbxPartial
            // 
            this.cbxPartial.FormattingEnabled = true;
            this.cbxPartial.Items.AddRange(new object[] {
            "无操作",
            "空Partial类",
            "test",
            "删除Partial类"});
            this.cbxPartial.Location = new System.Drawing.Point(127, 98);
            this.cbxPartial.Name = "cbxPartial";
            this.cbxPartial.Size = new System.Drawing.Size(308, 23);
            this.cbxPartial.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(450, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 32);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnOpenOutput
            // 
            this.btnOpenOutput.Location = new System.Drawing.Point(450, 54);
            this.btnOpenOutput.Name = "btnOpenOutput";
            this.btnOpenOutput.Size = new System.Drawing.Size(87, 32);
            this.btnOpenOutput.TabIndex = 7;
            this.btnOpenOutput.Text = "浏览";
            this.btnOpenOutput.UseVisualStyleBackColor = true;
            this.btnOpenOutput.Click += new System.EventHandler(this.btnOpenOutput_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(547, 16);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(87, 32);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "修改";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(644, 16);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(87, 32);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(555, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "命名空间";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(667, 58);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(272, 25);
            this.txtNamespace.TabIndex = 11;
            // 
            // cbxSubPath
            // 
            this.cbxSubPath.AutoSize = true;
            this.cbxSubPath.Location = new System.Drawing.Point(462, 100);
            this.cbxSubPath.Name = "cbxSubPath";
            this.cbxSubPath.Size = new System.Drawing.Size(104, 19);
            this.cbxSubPath.TabIndex = 12;
            this.cbxSubPath.Text = "使用子目录";
            this.cbxSubPath.UseVisualStyleBackColor = true;
            // 
            // cbxConfigId
            // 
            this.cbxConfigId.AutoSize = true;
            this.cbxConfigId.Location = new System.Drawing.Point(590, 100);
            this.cbxConfigId.Name = "cbxConfigId";
            this.cbxConfigId.Size = new System.Drawing.Size(163, 19);
            this.cbxConfigId.TabIndex = 13;
            this.cbxConfigId.Text = "使用SugarConfigId";
            this.cbxConfigId.UseVisualStyleBackColor = true;
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.BackColor = System.Drawing.Color.Tomato;
            this.btnSaveConfig.Location = new System.Drawing.Point(811, 90);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(128, 38);
            this.btnSaveConfig.TabIndex = 14;
            this.btnSaveConfig.Text = "保存配置";
            this.btnSaveConfig.UseVisualStyleBackColor = false;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(27, 143);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(90, 38);
            this.btnGen.TabIndex = 15;
            this.btnGen.Text = "生成代码";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(127, 143);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(90, 38);
            this.btnQuery.TabIndex = 16;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cbxAutoSelect
            // 
            this.cbxAutoSelect.AutoSize = true;
            this.cbxAutoSelect.Location = new System.Drawing.Point(253, 153);
            this.cbxAutoSelect.Name = "cbxAutoSelect";
            this.cbxAutoSelect.Size = new System.Drawing.Size(89, 19);
            this.cbxAutoSelect.TabIndex = 17;
            this.cbxAutoSelect.Text = "匹配文件";
            this.cbxAutoSelect.UseVisualStyleBackColor = true;
            // 
            // cbxUseCache
            // 
            this.cbxUseCache.AutoSize = true;
            this.cbxUseCache.Location = new System.Drawing.Point(373, 153);
            this.cbxUseCache.Name = "cbxUseCache";
            this.cbxUseCache.Size = new System.Drawing.Size(89, 19);
            this.cbxUseCache.TabIndex = 18;
            this.cbxUseCache.Text = "使用缓存";
            this.cbxUseCache.UseVisualStyleBackColor = true;
            // 
            // cbxFilter
            // 
            this.cbxFilter.FormattingEnabled = true;
            this.cbxFilter.Location = new System.Drawing.Point(480, 151);
            this.cbxFilter.Name = "cbxFilter";
            this.cbxFilter.Size = new System.Drawing.Size(240, 23);
            this.cbxFilter.TabIndex = 19;
            this.cbxFilter.TextChanged += new System.EventHandler(this.cbxFilter_TextChanged);
            this.cbxFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbxFilter_KeyDown);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(753, 143);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(90, 38);
            this.btnSelectAll.TabIndex = 20;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Location = new System.Drawing.Point(849, 143);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(90, 38);
            this.btnSelectNone.TabIndex = 21;
            this.btnSelectNone.Text = "取消";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 185);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(951, 484);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(872, 24);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(67, 15);
            this.lblResult.TabIndex = 23;
            this.lblResult.Text = "执行结果";
            // 
            // bgwGen
            // 
            this.bgwGen.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwGen_DoWork);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 669);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSelectNone);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.cbxFilter);
            this.Controls.Add(this.cbxUseCache);
            this.Controls.Add(this.cbxAutoSelect);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnGen);
            this.Controls.Add(this.btnSaveConfig);
            this.Controls.Add(this.cbxConfigId);
            this.Controls.Add(this.cbxSubPath);
            this.Controls.Add(this.txtNamespace);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnOpenOutput);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbxPartial);
            this.Controls.Add(this.txtOutputPath);
            this.Controls.Add(this.cbxDbs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "实体生成器";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxDbs;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.ComboBox cbxPartial;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnOpenOutput;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.CheckBox cbxSubPath;
        private System.Windows.Forms.CheckBox cbxConfigId;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.CheckBox cbxAutoSelect;
        private System.Windows.Forms.CheckBox cbxUseCache;
        private System.Windows.Forms.ComboBox cbxFilter;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectNone;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblResult;
        private System.ComponentModel.BackgroundWorker bgwGen;
    }
}

