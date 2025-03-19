using AiUoVsix.Command.NugetPublish.Enums;
using AiUoVsix.Command.NugetPublish.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AiUoVsix.Command.NugetPublish
{
    public partial class AddSourceForm : Form
    {
        public string NugetSource { get; set; }
        public string NugetKey { get; set; }
        public string SymbolSource { get; set; }
        public string SymbolKey { get; set; }
        public NugetMode Mode { get; set; }



        public AddSourceForm(NugetSourceItem item = null)
        {
            this.InitializeComponent();
            //if (item != null)
            //{
            //    this.txtNugetKey.Text = item.NugetKey;
            //    this.txtNugetSource.Text = item.NugetSource;
            //    this.txtSymbolSource.Text = item.SymbolSource;
            //    this.txtSymbolKey.Text = item.SymbolKey;
            //    this.cbxMode.SelectedIndex = (int)item.Mode;
            //}
            //else
            //    this.cbxMode.SelectedIndex = 2;
        }
    }
}
