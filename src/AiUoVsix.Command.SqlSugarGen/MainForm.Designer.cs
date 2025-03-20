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
            this.button7 = new System.Windows.Forms.Button();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvlMain = new System.Windows.Forms.ListView();
            this.lblResult = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
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
            this.cbxPartial.Location = new System.Drawing.Point(127, 99);
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
            this.btnOpenOutput.Location = new System.Drawing.Point(462, 59);
            this.btnOpenOutput.Name = "btnOpenOutput";
            this.btnOpenOutput.Size = new System.Drawing.Size(75, 23);
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
            this.txtNamespace.Location = new System.Drawing.Point(645, 58);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(272, 25);
            this.txtNamespace.TabIndex = 11;
            // 
            // cbxSubPath
            // 
            this.cbxSubPath.AutoSize = true;
            this.cbxSubPath.Location = new System.Drawing.Point(462, 102);
            this.cbxSubPath.Name = "cbxSubPath";
            this.cbxSubPath.Size = new System.Drawing.Size(104, 19);
            this.cbxSubPath.TabIndex = 12;
            this.cbxSubPath.Text = "使用子目录";
            this.cbxSubPath.UseVisualStyleBackColor = true;
            // 
            // cbxConfigId
            // 
            this.cbxConfigId.AutoSize = true;
            this.cbxConfigId.Location = new System.Drawing.Point(590, 102);
            this.cbxConfigId.Name = "cbxConfigId";
            this.cbxConfigId.Size = new System.Drawing.Size(163, 19);
            this.cbxConfigId.TabIndex = 13;
            this.cbxConfigId.Text = "使用SugarConfigId";
            this.cbxConfigId.UseVisualStyleBackColor = true;
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Location = new System.Drawing.Point(811, 97);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(128, 38);
            this.btnSaveConfig.TabIndex = 14;
            this.btnSaveConfig.Text = "保存配置";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(31, 143);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(90, 38);
            this.btnGen.TabIndex = 15;
            this.btnGen.Text = "生成代码";
            this.btnGen.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(127, 143);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(90, 38);
            this.button7.TabIndex = 16;
            this.button7.Text = "查询";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(253, 154);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(89, 19);
            this.checkBox3.TabIndex = 17;
            this.checkBox3.Text = "匹配文件";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(373, 153);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(89, 19);
            this.checkBox4.TabIndex = 18;
            this.checkBox4.Text = "使用缓存";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(480, 152);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(240, 23);
            this.comboBox3.TabIndex = 19;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(753, 141);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(90, 38);
            this.button8.TabIndex = 20;
            this.button8.Text = "全选";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(849, 141);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(90, 38);
            this.button9.TabIndex = 21;
            this.button9.Text = "取消";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvlMain);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 185);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(951, 484);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            // 
            // lvlMain
            // 
            this.lvlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvlMain.HideSelection = false;
            this.lvlMain.Location = new System.Drawing.Point(3, 21);
            this.lvlMain.Name = "lvlMain";
            this.lvlMain.Size = new System.Drawing.Size(945, 460);
            this.lvlMain.TabIndex = 0;
            this.lvlMain.UseCompatibleStateImageBehavior = false;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(806, 24);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(37, 15);
            this.lblResult.TabIndex = 23;
            this.lblResult.Text = "结果";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 669);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.button7);
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
            this.Text = "实体生成器";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ListView lvlMain;
    }
}

