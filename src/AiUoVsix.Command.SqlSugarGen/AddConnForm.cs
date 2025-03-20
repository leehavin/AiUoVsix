using AiUoVsix.Command.SqlSugarGen.Common;
using AiUoVsix.Common;
using SqlSugar;
using System;
using System.Windows.Forms;

namespace AiUoVsix.Command.SqlSugarGen
{
    public partial class AddConnForm: Form
    {
        private bool _isAdd;
        public ConnectionElement ConnectionElement { get; private set; }
        public AddConnForm(ConnectionElement element)
        {
            InitializeComponent();

            this.cbxDbTypes.DataSource = (object)EnumUtil.GetInfo<SqlSugar.DbType>().GetList();
            this.cbxDbTypes.DisplayMember = "Name";
            this.cbxDbTypes.ValueMember = "Value";
            this.lblResult.Text = (string)null;
            this.ConnectionElement = element ?? new ConnectionElement();
            this._isAdd = element == null;
            if (!this._isAdd)
            {
                this.cbxDbTypes.SelectedValue = (object)(int)element.DatabaseType;
                this.txtConnStr.Text = element.ConnectionString;
                this.txtName.Text = element.Name;
            }
            else
                this.cbxDbTypes.SelectedValue = (object)0;
        }

        /// <summary>
        /// 链接字符串变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtConnStr_TextChanged(object sender, EventArgs e)
        {
            this.btnTest.Enabled = !string.IsNullOrEmpty(this.txtConnStr.Text);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txtConnStr.Text) || string.IsNullOrEmpty(this.txtName.Text))
            {
                int num = (int)MessageBox.Show("名称和连接字符串不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                this.ConnectionElement.Name = this.txtName.Text;
                this.ConnectionElement.DatabaseType = (SqlSugar.DbType)this.cbxDbTypes.SelectedValue;
                this.ConnectionElement.ConnectionString = this.txtConnStr.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
        
        /// <summary>
        /// 测试窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            ConnectionConfig config = new ConnectionConfig()
            {
                DbType = (SqlSugar.DbType)this.cbxDbTypes.SelectedValue,
                ConnectionString = this.txtConnStr.Text,
                IsAutoCloseConnection = true
            };
            try
            {
                using (SqlSugarClient sqlSugarClient = new SqlSugarClient(config))
                    sqlSugarClient.Ado.CheckConnection();
                this.lblResult.Text = "连接成功...";
            }
            catch
            {
                this.lblResult.Text = "连接失败...";
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        
    }
}
