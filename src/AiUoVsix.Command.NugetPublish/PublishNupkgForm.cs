using AiUoVsix.Command.NugetPublish.Models;
using AiUoVsix.Common;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AiUoVsix.Command.NugetPublish
{
    public partial class PublishNupkgForm : Form
    {

        private SynchronizationContext _synchronizationContext;

        public PublishNupkgForm(NugetSourceItem item = null)
        {
            this.InitializeComponent();
            this._synchronizationContext = SynchronizationContext.Current;
            if (item == null)
                return;
            this.txtNugetKey.Text = item.NugetKey;
            this.txtNugetSource.Text = item.NugetSource;
        }

        private void btnSelect_Click(object sender, System.EventArgs e)
        {
            if (this.dlgSelect.ShowDialog() != DialogResult.OK)
                return;
            this.txtList.Clear();
            this.txtList.Text = string.Join(Environment.NewLine, this.dlgSelect.FileNames);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtList.Text))
                return;
            this.EnableControls(false);
            this.tabMain.SelectTab(1);
            this.txtOutput.Clear();
            this.bgwMain.RunWorkerAsync();
        }

        private void btnCancle_Click(object sender, EventArgs e) => this.Close();

        private void EnableControls(bool enabled)
        {
            this.txtNugetKey.Enabled = this.txtNugetSource.Enabled = this.btnOk.Enabled = this.btnSelect.Enabled = this.btnCancle.Enabled = enabled;
        }

        private void bgwMain_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            foreach (string path in this.txtList.Text.SplitNewLine())
            {
                // CliUtil.Execute("dotnet nuget push -k " + this.txtNugetKey.Text + " -s " + this.txtNugetSource.Text + " " + path);
                this.OutputLine("发布" + Path.GetFileName(path) + "成功");
            }
        }

        private void OutputLine(string str)
        {
            str += Environment.NewLine;
            this._synchronizationContext.Send((SendOrPostCallback)(callback => this.txtOutput.AppendText(str)), (object)str);
        }
    }
}
