using AiUoVsix.Command.SqlSugarGen.Common;
using AiUoVsix.Common;
using System.Windows.Forms;

namespace AiUoVsix.Command.SqlSugarGen
{
    public partial class MainForm: Form
    {
        public MainForm(EnvDTEWraper dte = null)
        {
            InitializeComponent();
            GenUtil.Init(dte);
        }
    }
}
