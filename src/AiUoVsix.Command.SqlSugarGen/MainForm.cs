using AiUoVsix.Command.SqlSugarGen.Common;
using AiUoVsix.Common;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace AiUoVsix.Command.SqlSugarGen
{
    public partial class MainForm : Form
    {
        private bool _isDropDown = false;
        private Color[] _colors = new Color[3] { Color.Black, Color.Red, Color.Blue };
        public HashSet<string> Prefixs = new HashSet<string>();

        private ListViewEx lvlMain;

        public MainForm(EnvDTEWraper dte = null)
        {
            InitializeComponent();
            lvlMain = new ListViewEx();

            this.groupBox1.Controls.Add(this.lvlMain);
            this.lvlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvlMain.HideSelection = false;
            this.lvlMain.Location = new System.Drawing.Point(3, 21);
            this.lvlMain.Name = "lvlMain";
            this.lvlMain.Size = new System.Drawing.Size(945, 460);
            this.lvlMain.TabIndex = 0;
            this.lvlMain.UseCompatibleStateImageBehavior = false;

            GenUtil.Init(dte);
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            this.lblResult.Text = (string)null;
            this.cbxPartial.SelectedIndex = 0;
            this.BindCbxDbs();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            AddConnForm addConnForm = new AddConnForm((ConnectionElement)null);
            if (addConnForm.ShowDialog() != DialogResult.OK)
                return;
            ConnectionElement connectionElement = new ConnectionElement()
            {
                Name = addConnForm.ConnectionElement.Name,
                DatabaseType = addConnForm.ConnectionElement.DatabaseType,
                ConnectionString = addConnForm.ConnectionElement.ConnectionString,
                Partial = PartialMode.None
            };
            GenUtil.Options.Elements.Add(connectionElement);
            this.BindCbxDbs();
            this.cbxDbs.SelectedItem = (object)connectionElement;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            ConnectionElement selectedValue = (ConnectionElement)this.cbxDbs.SelectedValue;
            if (new AddConnForm(selectedValue).ShowDialog() != DialogResult.OK)
                return;
            this.BindCbxDbs();
            this.cbxDbs.SelectedItem = (object)selectedValue;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            GenUtil.Options.Elements.Remove((ConnectionElement)this.cbxDbs.SelectedValue);
            this.BindCbxDbs();
            ConnectionElement connectionElement = GenUtil.Options.Elements.FirstOrDefault<ConnectionElement>();
            if (connectionElement == null)
                return;
            this.cbxDbs.SelectedItem = (object)connectionElement;
        }

        /// <summary>
        /// 下拉加载数据库源信息
        /// </summary>
        private void BindCbxDbs()
        {
            this.cbxDbs.DataSource = (object)null;
            this.cbxDbs.DataSource = (object)GenUtil.Options.Elements;
            this.cbxDbs.DisplayMember = "Name";
            this.btnDelete.Enabled = this.btnEdit.Enabled = GenUtil.Options.Elements.Count > 0;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveConfig_Click(object sender, System.EventArgs e)
        {
            this.BindConnection();
            GenUtil.SaveConfig();
            int num = (int)MessageBox.Show("配置信息保存成功！", "提示");
        }

        /// <summary>
        /// 绑定链接
        /// </summary>
        /// <returns></returns>
        private ConnectionElement BindConnection()
        {
            ConnectionElement selectedValue = (ConnectionElement)this.cbxDbs.SelectedValue;
            selectedValue.OutputPath = this.txtOutputPath.Text;
            selectedValue.Namespace = this.txtNamespace.Text;
            selectedValue.UseSubPath = this.cbxSubPath.Checked;
            selectedValue.UseSugarConfigId = this.cbxConfigId.Checked;
            selectedValue.Partial = (PartialMode)this.cbxPartial.SelectedIndex;
            GenUtil.Options.DefaultElement = selectedValue.Name;
            return selectedValue;
        }

        /// <summary>
        /// 浏览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenOutput_Click(object sender, System.EventArgs e)
        {
            Process.Start("Explorer.exe", Path.Combine(GenUtil.BasePath, this.txtOutputPath.Text));
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGen_Click(object sender, System.EventArgs e)
        {
            List<string> list = lvlMain.GetSelectedItems().ToList();
            if (list.Count != 0)
            {
                SetEnabled(enabled: false);
                ConnectionElement item = BindConnection();
                lblResult.Text = "正在生成...";
                bgwGen.RunWorkerAsync((item, list));
            }
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            string text = cbxFilter.Text;
            ConnectionElement connectionElement = BindConnection();
            Dictionary<string, ListViewObjectItem> dbObjectDict = QueryTableHelper.GetDbObjectDict(connectionElement, cbxUseCache.Checked);
            if (cbxAutoSelect.Checked)
            {
                string outputPath = Path.Combine(GenUtil.BasePath, connectionElement.OutputPath);
                HashSet<string> fileTables = QueryTableHelper.GetFileTables(outputPath);
                foreach (string item in fileTables)
                {
                    if (dbObjectDict.TryGetValue(item, out var value))
                    {
                        value.Check = true;
                    }
                }
            }
            List<ListViewObjectItem> list = dbObjectDict.Values.OrderBy((ListViewObjectItem x) => x.Name).ToList();
            if (!string.IsNullOrEmpty(cbxFilter.Text) && cbxFilter.Text != "ALL")
            {
                list = list.FindAll((ListViewObjectItem x) => x.Name.StartsWith(cbxFilter.Text));
            }
            lvlMain.AddItems(list);
            List<string> list2 = lvlMain.Prefixs.ToList();
            list2.Sort();
            list2.Insert(0, "ALL");
            cbxFilter.DataSource = list2;
            cbxFilter.Text = text;
        }

        private void SetEnabled(bool enabled)
        {
            this.lvlMain.Enabled = enabled;
        }

        /// <summary>
        /// 任务生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwGen_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            dynamic argument = e.Argument;
            new GenService(argument.Item1).Execute(argument.Item2);
        }

        /// <summary>
        /// 文本变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxFilter_TextChanged(object sender, EventArgs e)
        { 
            if (!this._isDropDown)
                return;
            this._isDropDown = false;
            this.btnQuery.PerformClick();
        }

        /// <summary>
        /// 下拉检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxFilter_KeyDown(object sender, KeyEventArgs e)
        { 
            if (e.KeyCode != Keys.Return)
                return;
            this._isDropDown = false;
            this.btnQuery.PerformClick();
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAll_Click(object sender, EventArgs e) => this.lvlMain.SelectAll();

        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectNone_Click(object sender, EventArgs e) => this.lvlMain.ClearSelect();
    }
}
