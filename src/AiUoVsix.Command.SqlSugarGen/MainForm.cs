using AiUoVsix.Command.SqlSugarGen.Common;
using AiUoVsix.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AiUoVsix.Command.SqlSugarGen
{
    public partial class MainForm: Form
    {
        private bool _isDropDown = false;

        public MainForm(EnvDTEWraper dte = null)
        {
            InitializeComponent();
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
         
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            GenUtil.Options.Elements.Remove((ConnectionElement)this.cbxDbs.SelectedValue);
            this.BindCbxDbs();
            ConnectionElement connectionElement = GenUtil.Options.Elements.FirstOrDefault<ConnectionElement>();
            if (connectionElement == null)
                return;
            this.cbxDbs.SelectedItem = (object)connectionElement;
        }

        private void BindCbxDbs()
        {
            this.cbxDbs.DataSource = (object)null;
            this.cbxDbs.DataSource = (object)GenUtil.Options.Elements;
            this.cbxDbs.DisplayMember = "Name";
            this.btnDelete.Enabled = this.btnEdit.Enabled = GenUtil.Options.Elements.Count > 0;
        }

        private void btnSaveConfig_Click(object sender, System.EventArgs e)
        {
            this.BindConnection();
            GenUtil.SaveConfig();
            int num = (int)MessageBox.Show("配置信息保存成功！", "提示");
        }
         
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
    }
}
