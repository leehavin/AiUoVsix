namespace AiUoVsix.Command.NugetPublish
{
    partial class AddSourceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSourceForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxMode = new System.Windows.Forms.ComboBox();
            this.txtSymbolKey = new System.Windows.Forms.TextBox();
            this.txtSymbolSource = new System.Windows.Forms.TextBox();
            this.txtNugetKey = new System.Windows.Forms.TextBox();
            this.txtNugetSource = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nuget Source：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nuget Key：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Symbol Source：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Symbol Key：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "符号模式：";
            // 
            // cbxMode
            // 
            this.cbxMode.FormattingEnabled = true;
            this.cbxMode.Items.AddRange(new object[] {
            "不生成符号包",
            "生成符号包",
            "嵌入符号包"});
            this.cbxMode.Location = new System.Drawing.Point(158, 202);
            this.cbxMode.Name = "cbxMode";
            this.cbxMode.Size = new System.Drawing.Size(355, 23);
            this.cbxMode.TabIndex = 5;
            // 
            // txtSymbolKey
            // 
            this.txtSymbolKey.Location = new System.Drawing.Point(162, 155);
            this.txtSymbolKey.Name = "txtSymbolKey";
            this.txtSymbolKey.Size = new System.Drawing.Size(351, 25);
            this.txtSymbolKey.TabIndex = 6;
            // 
            // txtSymbolSource
            // 
            this.txtSymbolSource.Location = new System.Drawing.Point(162, 109);
            this.txtSymbolSource.Name = "txtSymbolSource";
            this.txtSymbolSource.Size = new System.Drawing.Size(351, 25);
            this.txtSymbolSource.TabIndex = 7;
            // 
            // txtNugetKey
            // 
            this.txtNugetKey.Location = new System.Drawing.Point(162, 63);
            this.txtNugetKey.Name = "txtNugetKey";
            this.txtNugetKey.Size = new System.Drawing.Size(351, 25);
            this.txtNugetKey.TabIndex = 8;
            // 
            // txtNugetSource
            // 
            this.txtNugetSource.Location = new System.Drawing.Point(162, 17);
            this.txtNugetSource.Name = "txtNugetSource";
            this.txtNugetSource.Size = new System.Drawing.Size(351, 25);
            this.txtNugetSource.TabIndex = 9;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(295, 242);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 39);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(413, 242);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 39);
            this.button2.TabIndex = 11;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AddSourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 301);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtNugetSource);
            this.Controls.Add(this.txtNugetKey);
            this.Controls.Add(this.txtSymbolSource);
            this.Controls.Add(this.txtSymbolKey);
            this.Controls.Add(this.cbxMode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddSourceForm";
            this.Text = "AddSourceForm";
            this.Load += new System.EventHandler(this.AddSourceForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxMode;
        private System.Windows.Forms.TextBox txtSymbolKey;
        private System.Windows.Forms.TextBox txtSymbolSource;
        private System.Windows.Forms.TextBox txtNugetKey;
        private System.Windows.Forms.TextBox txtNugetSource;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button button2;
    }
}