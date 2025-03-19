using AiUoVsix.Command.SqlSugarGen.Common;
using AiUoVsix.Common;
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
    }
}
