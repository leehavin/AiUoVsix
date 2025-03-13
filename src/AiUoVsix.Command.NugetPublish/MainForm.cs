using AiUoVsix.Command.NugetPublish.Enums;
using AiUoVsix.Command.NugetPublish.Models;
using AiUoVsix.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AiUoVsix.Command.NugetPublish
{
    public partial class MainForm: Form
    {

        public static EnvDTEWraper CurrentDTE { get; set; }

        private string _basePath;
        private string _configFile;
        private string _nupkgsPath;
        private List<NugetSourceItem> _nugetItems = new List<NugetSourceItem>();
        private Dictionary<string, ProjectInfo> _projectList;
        private NugetSourceItem _selectedNugetSource;
        private ProjectInfoParser _projectParser = new ProjectInfoParser();
        private VsixItem _vsixItem;
        private ProjectInfo _vsixProjectInfo;

        public MainForm(EnvDTEWraper dte = null)
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this._basePath = MainForm.CurrentDTE != null ? Path.GetDirectoryName(MainForm.CurrentDTE.SolutionFile) : Application.StartupPath;
            this._configFile = Path.Combine(this._basePath, "nuget.json");
            this.txtConfigPath.Text = this._configFile;
            this._nupkgsPath = Path.Combine(this._basePath, "nupkgs");
            this.txtNupkgs.Text = this._nupkgsPath;
            this.cbxVersion.SelectedIndex = 1;
            PublishConfig publishConfig;
            if (File.Exists(this._configFile))
            {
               //  publishConfig = JsonConvert.DeserializeObject<PublishConfig>(File.ReadAllText(this._configFile));
            }
            else
            {
                publishConfig = new PublishConfig();
                publishConfig.NugetItems.Add(new NugetSourceItem()
                {
                    NugetSource = "https://api.nuget.org/v3/index.json",
                    NugetKey = "oy2azc4vyfqzcrqqk6raqd2ltz6yqiuuzlozz366ws3mci",
                    Mode = NugetMode.None
                });
                publishConfig.VsixItem = new VsixItem()
                {
                    Token = "3twuo3jaffjjlqjzbn66ijxcma2qffswyt7ajekwnnsg62jbewgq"
                };
                if (MainForm.CurrentDTE == null)
                    ;
            }
           // this._nugetItems = publishConfig.NugetItems;
           // this.chkGit.Checked = publishConfig.IsGitCommit;
           // this.BindCbxNuget(publishConfig.NugetIndex);
            this._projectList = new Dictionary<string, ProjectInfo>();
           //  foreach (string project in publishConfig.Projects)
            {
                // string absolutePath = IOUtil.GetAbsolutePath(this._basePath, project);
                // this._projectList.Add(absolutePath, this._projectParser.ParseInfo(absolutePath, this._basePath));
            }
            this.dlgAdd.InitialDirectory = this._basePath;
            this.BindGrid();
            // this._vsixItem = publishConfig.VsixItem ?? new VsixItem();
            this.BindVsix();
        }

        private void BindGrid()
        {
            List<ProjectInfo> list = this._projectList.Values.ToList<ProjectInfo>();
            list.Sort();
            this.dgwMain.DataSource = (object)list;
        }

        private void BindVsix()
        {
            //if (!string.IsNullOrEmpty(this._vsixItem.ProjectPath))
            //    this.txtVsixProject.Text = IOUtil.GetAbsolutePath(this._basePath, this._vsixItem.ProjectPath);
            //if (!string.IsNullOrEmpty(this._vsixItem.VsixPath))
            //    this.txtVsixPath.Text = IOUtil.GetAbsolutePath(this._basePath, this._vsixItem.VsixPath);
            //if (!string.IsNullOrEmpty(this._vsixItem.ConfigPath))
            //    this.txtVsixConfig.Text = IOUtil.GetAbsolutePath(this._basePath, this._vsixItem.ConfigPath);
            //if (!string.IsNullOrEmpty(this._vsixItem.Token))
            //    this.txtVsixToken.Text = this._vsixItem.Token;
            //if (!string.IsNullOrEmpty(this._vsixItem.PublisherExePath))
            //    this.txtVsixExePath.Text = this._vsixItem.PublisherExePath;
            //if (string.IsNullOrEmpty(this._vsixItem.MSBuildExePath))
            //    return;
            //this.txtMSBuild.Text = this._vsixItem.MSBuildExePath;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.dlgAdd.ShowDialog() != DialogResult.OK)
                return;
            string fileName = this.dlgAdd.FileName;
            if (this._projectList.ContainsKey(fileName))
            {
                int num = (int)MessageBox.Show("项目已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this._projectList.Add(fileName, this._projectParser.ParseInfo(fileName, this._basePath));
                this.BindGrid();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this._vsixItem.Token = this.txtVsixToken.Text;
            PublishConfig publishConfig = new PublishConfig()
            {
                NugetIndex = this.cbxNuget.SelectedIndex,
                NugetItems = this._nugetItems,
                IsGitCommit = this.chkGit.Checked,
                VsixItem = this._vsixItem
            };
            List<ProjectInfo> list = this._projectList.Values.ToList<ProjectInfo>();
            list.Sort();
            foreach (ProjectInfo projectInfo in list)
            {
               // string relativePath = IOUtil.GetRelativePath(this._basePath, projectInfo.ProjectPath);
               // publishConfig.Projects.Add(relativePath);
            }
           // File.WriteAllText(this._configFile, JsonConvert.SerializeObject((object)publishConfig, (Formatting)1));
            int num = (int)MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            if (this.cbxNuget.SelectedIndex == -1)
            {
                int num = (int)MessageBox.Show("请选择Nuget Source");
            }
            else
                this.BuildAndPublish(this.GetPublishProjects());
        }

        private void btnVsixPublish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtVsixProject.Text) || string.IsNullOrEmpty(this.txtVsixExePath.Text) || string.IsNullOrEmpty(this.txtMSBuild.Text) || string.IsNullOrEmpty(this.txtVsixPath.Text) || string.IsNullOrEmpty(this.txtVsixConfig.Text) || string.IsNullOrEmpty(this.txtVsixToken.Text))
            {
                int num = (int)MessageBox.Show("VSIX信息不能为空");
            }
            this.BuildAndPublish(new List<ProjectInfo>()
      {
        this._vsixProjectInfo
      });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.btnCancel.Enabled = false;
            this.bgwMain.CancelAsync();
        }

        private void btnNupkg_Click(object sender, EventArgs e)
        {
           // int num = (int)new PublishNupkgForm(this._nugetItems == null || this._nugetItems.Count <= 0 ? (NugetSourceItem)null : this._nugetItems[this.cbxNuget.SelectedIndex]).ShowDialog();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._projectList.Remove(((ProjectInfo)this.dgwMain.SelectedRows[0].DataBoundItem).ProjectPath);
            this.BindGrid();
        }

        private void cmenuGrid_Opening(object sender, CancelEventArgs e)
        {
        }

        private void 编译并发布ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BuildAndPublish(new List<ProjectInfo>()
      {
        (ProjectInfo) this.dgwMain.SelectedRows[0].DataBoundItem
      });
        }

        private void BuildAndPublish(List<ProjectInfo> projs)
        {
            if (projs.Count == 0)
                return;
            this.progressBar1.Maximum = projs.Count;
            this.EnableControls(true);
            this.tabMain.SelectTab(2);
            this.txtOutput.Clear();
            this.lblResult.Text = "正在生成...";
            this.bgwMain.RunWorkerAsync((object)(projs, this.cbxVersion.SelectedIndex, (NugetMode)this.cbxMode.SelectedIndex, this.chkGit.Checked, this.chkRelease.Checked, this.txtBeta.Text));
        }

        private void bgwMain_DoWork(object sender, DoWorkEventArgs e)
        {
            (List<ProjectInfo>, int, NugetMode, bool, bool, string) valueTuple = ((List<ProjectInfo>, int, NugetMode, bool, bool, string))e.Argument;
            Dictionary<string, List<string>> source = new Dictionary<string, List<string>>();
            foreach (ProjectInfo project in valueTuple.Item1)
            {
                if (project.Status)
                {
                    this._projectParser.UpdateVersion(project, valueTuple.Item2, valueTuple.Item6);
                    string key = project.Version.ToString();
                    string str = project.ProjectName + " v" + key;
                    if (source.ContainsKey(key))
                        source[key].Add(str);
                    else
                        source.Add(key, new List<string>() { str });
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            if (source.Count == 1)
            {
                stringBuilder.Append("发布版本: v" + source.First<KeyValuePair<string, List<string>>>().Key);
            }
            else
            {
                stringBuilder.Append("发布版本:");
                foreach (List<string> values in source.Values)
                    stringBuilder.Append(string.Join(";", (IEnumerable<string>)values));
            }
            if (valueTuple.Item4)
            {
                CommandRunner commandRunner = new CommandRunner("git", this._basePath);
                string gitOut = commandRunner.Run("add .") + Environment.NewLine;
                gitOut += commandRunner.Run("commit -m \"" + stringBuilder.ToString() + "\"");
               //  this._synchronizationContext.Send((SendOrPostCallback)(callback => this.txtDetail.AppendText(gitOut + Environment.NewLine)), (object)gitOut);
            }
            for (int index = 0; index < valueTuple.Item1.Count; ++index)
            {
                if (this.bgwMain.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                ProjectInfo projectInfo = valueTuple.Item1[index];
                if (projectInfo.Status)
                {
                    string cmd = (string)null;
                    string str1 = valueTuple.Item5 ? "Release" : "Debug";
                    switch (projectInfo.PublishType)
                    {
                        case PublishType.Nuget:
                            switch (projectInfo.ProjectType)
                            {
                                case ProjectType.Standard:
                                case ProjectType.Core:
                                case ProjectType.Net:
                                    string str2 = string.Empty;
                                    switch (valueTuple.Item3)
                                    {
                                        case NugetMode.Symbols:
                                            str2 = "--include-symbols -p:SymbolPackageFormat=snupkg";
                                            break;
                                        case NugetMode.Embedded:
                                            str2 = "-p:DebugSymbols=true -p:DebugType=embedded";
                                            break;
                                    }
                                    cmd = "dotnet build " + projectInfo.ProjectPath + " -c " + str1;
                                    this.ExecAndOutput(cmd, index, projectInfo.ProjectName, "编译");
                                    cmd = "dotnet pack " + projectInfo.ProjectPath + " -c " + str1 + " -o " + this._nupkgsPath + " --nologo -v m  " + str2;
                                    this.ExecAndOutput(cmd, index, projectInfo.ProjectName, "打包");
                                    break;
                                case ProjectType.Framework:
                                    cmd = "nuget pack " + projectInfo.ProjectPath + " -OutputDirectory " + this._nupkgsPath + " -Symbols -SymbolPackageFormat snupkg";
                                    this.ExecAndOutput(cmd, index, projectInfo.ProjectName, "编译");
                                    break;
                            }
                            break;
                        case PublishType.Vsix:
                            cmd = "\"" + this.txtMSBuild.Text + "\" " + projectInfo.ProjectPath + " -t:rebuild -p:Configuration=Debug";
                            this.ExecAndOutput(cmd, index, projectInfo.ProjectName, "编译");
                            break;
                    }
                    switch (projectInfo.PublishType)
                    {
                        case PublishType.Nuget:
                            string str3 = Path.Combine(this._nupkgsPath, projectInfo.ProjectName + "." + projectInfo.Version.ToString());
                            string path1 = str3 + ".nupkg";
                            if (!File.Exists(path1))
                                throw new Exception("文件不存在：" + path1);
                            NugetSourceItem selectedNugetSource = this._selectedNugetSource;
                            if (projectInfo.ProjectType == ProjectType.Framework)
                                cmd = "nuget push " + path1 + " -ApiKey " + selectedNugetSource.NugetKey + " -Source " + selectedNugetSource.NugetSource;
                            else
                                cmd = "dotnet nuget push " + path1 + " -k " + selectedNugetSource.NugetKey + " -s " + selectedNugetSource.NugetSource;
                            this.ExecAndOutput(cmd, index, projectInfo.ProjectName, "发布.nupkg");
                            string path2 = str3 + ".snupkg";
                            if (File.Exists(path2))
                            {
                                cmd = "dotnet nuget push " + path2 + " -sk " + selectedNugetSource.SymbolKey + " -ss " + selectedNugetSource.SymbolSource;
                                this.ExecAndOutput(cmd, index, projectInfo.ProjectName, "发布.snupkg");
                                break;
                            }
                            break;
                        case PublishType.Vsix:
                            cmd = "\"" + this.txtVsixExePath.Text + "\" publish -payload \"" + this.txtVsixPath.Text + "\" -publishManifest \"" + this.txtVsixConfig.Text + "\" -personalAccessToken \"" + this.txtVsixToken.Text + "\"";
                           // this._synchronizationContext.Send((SendOrPostCallback)(callback => this.txtDetail.AppendText(cmd + Environment.NewLine)), (object)cmd);
                            this.ExecAndOutput(cmd, index, projectInfo.ProjectName, "发布.vsix");
                            break;
                    }
                    this.bgwMain.ReportProgress(index, (object)index);
                }
            }
        }

        private void bgwMain_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = (int)e.UserState + 1;
        }

        private void bgwMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.lblResult.ForeColor = Color.Red;
                this.lblResult.Text = "出现异常: ";
                this.lblResult.Text += e.Error.Message;
            }
            else
                this.lblResult.Text = "操作完成";
            this.progressBar1.Value = 0;
            this.progressBar1.Maximum = 0;
            this.txtOutput.AppendText("============ 结 束 ==========");
            this.EnableControls(false);
        }

        private List<ProjectInfo> GetPublishProjects()
        {
            List<ProjectInfo> publishProjects = new List<ProjectInfo>();
            for (int index = 0; index < this.dgwMain.Rows.Count; ++index)
            {
                DataGridViewRow row = this.dgwMain.Rows[index];
                if (Convert.ToBoolean(row.Cells[0].Value))
                    publishProjects.Add((ProjectInfo)row.DataBoundItem);
            }
            return publishProjects;
        }

        private void EnableControls(bool executing)
        {
            this.cbxVersion.Enabled = this.cbxNuget.Enabled = this.btnAddSource.Enabled = this.btnEditSource.Enabled = this.btnDelSource.Enabled = this.btnPublish.Enabled = this.btnVsixPublish.Enabled = this.btnSave.Enabled = this.btnAdd.Enabled = this.btnNupkg.Enabled = this.dgwMain.Enabled = !executing;
            this.btnCancel.Enabled = executing;
        }

        private void ExecAndOutput(string cmd, int index, string project, string type)
        {
            //string strOut = (string)null;
            //string output;
            //if (WindowsUtil.RunCommand(cmd, out output))
            //{
            //    strOut += string.Format("项目{0}: {1} =>{2}成功。{3}", (object)(index + 1), (object)project, (object)type, (object)Environment.NewLine);
            //}
            //else
            //{
            //    strOut += string.Format("项目{0}: {1} =>{2}失败！{3}", (object)(index + 1), (object)project, (object)type, (object)Environment.NewLine);
            //    strOut = strOut + "cmd: " + cmd + Environment.NewLine;
            //    strOut = strOut + output + Environment.NewLine;
            //}
            //this._synchronizationContext.Send((SendOrPostCallback)(callback => this.txtOutput.AppendText(strOut)), (object)strOut);
            //this._synchronizationContext.Send((SendOrPostCallback)(callback => this.txtDetail.AppendText(output + Environment.NewLine)), (object)output);
        }

        private void btnAddSource_Click(object sender, EventArgs e)
        {
            //AddSourceForm addSourceForm = new AddSourceForm();
            //if (addSourceForm.ShowDialog() != DialogResult.OK)
            //    return;
            //this._nugetItems.Add(new NugetSourceItem()
            //{
            //    NugetSource = addSourceForm.NugetSource,
            //    NugetKey = addSourceForm.NugetKey,
            //    SymbolKey = addSourceForm.SymbolKey,
            //    SymbolSource = addSourceForm.SymbolSource,
            //    Mode = addSourceForm.Mode
            //});
            //this.BindCbxNuget(this._nugetItems.Count - 1);
        }

        private void btnDelSource_Click(object sender, EventArgs e)
        {
            this._nugetItems.RemoveAt(this.cbxNuget.SelectedIndex);
            this.BindCbxNuget(0);
        }

        private void btnEditSource_Click(object sender, EventArgs e)
        {
            //AddSourceForm addSourceForm = new AddSourceForm(this._nugetItems[this.cbxNuget.SelectedIndex]);
            //if (addSourceForm.ShowDialog() != DialogResult.OK)
            //    return;
            //NugetSourceItem nugetItem = this._nugetItems[this.cbxNuget.SelectedIndex];
            //nugetItem.NugetKey = addSourceForm.NugetKey;
            //nugetItem.NugetSource = addSourceForm.NugetSource;
            //nugetItem.Mode = addSourceForm.Mode;
            //this.BindCbxNuget(this.cbxNuget.SelectedIndex);
        }

        private void BindCbxNuget(int index)
        {
            if (this._nugetItems.Count > 0)
            {
                this.cbxNuget.DataSource = (object)this._nugetItems.Select<NugetSourceItem, string>((Func<NugetSourceItem, string>)(item => item.NugetSource)).ToList<string>();
                this.cbxNuget.SelectedIndex = index;
                this.cbxMode.SelectedIndex = (int)this._nugetItems[index].Mode;
            }
            else
                this.cbxNuget.DataSource = (object)null;
        }

        private void cbxNuget_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._selectedNugetSource = this._nugetItems[this.cbxNuget.SelectedIndex];
            this.cbxMode.SelectedIndex = (int)this._selectedNugetSource.Mode;
        }

        private void btnVsixProject_Click(object sender, EventArgs e)
        {
            //if (this.dlgAdd.ShowDialog() != DialogResult.OK)
            //    return;
            //this.txtVsixProject.Text = this.dlgAdd.FileName;
            //this._vsixItem.ProjectPath = IOUtil.GetRelativePath(this._basePath, this.dlgAdd.FileName);
        }

        private void txtVsixProject_TextChanged(object sender, EventArgs e)
        {
            //string withoutExtension = Path.GetFileNameWithoutExtension(this.txtVsixProject.Text);
            //this._vsixProjectInfo = this._projectParser.ParseInfo(this.txtVsixProject.Text, this._basePath);
            //this.lblVsixVersion.Text = this._vsixProjectInfo.Version.ToString();
            //string directoryName = Path.GetDirectoryName(this.txtVsixProject.Text);
            //this.txtVsixPath.Text = Path.Combine(directoryName, "bin\\debug\\" + withoutExtension + ".vsix");
            //this._vsixItem.VsixPath = IOUtil.GetRelativePath(this._basePath, this.txtVsixPath.Text);
            //this.txtVsixConfig.Text = Path.Combine(directoryName, "bin\\debug\\publishManifest.json");
            //this._vsixItem.ConfigPath = IOUtil.GetRelativePath(this._basePath, this.txtVsixConfig.Text);
        }

        private void btnVsixPath_Click(object sender, EventArgs e)
        {
            //if (this.dlgAdd.ShowDialog() != DialogResult.OK)
            //    return;
            //this.txtVsixPath.Text = this.dlgAdd.FileName;
            //this._vsixItem.VsixPath = IOUtil.GetRelativePath(this._basePath, this.dlgAdd.FileName);
        }

        private void btnVsixConfig_Click(object sender, EventArgs e)
        {
            //if (this.dlgAdd.ShowDialog() != DialogResult.OK)
            //    return;
            //this.txtVsixConfig.Text = this.dlgAdd.FileName;
            //this._vsixItem.ConfigPath = IOUtil.GetRelativePath(this._basePath, this.dlgAdd.FileName);
        }

        private void btnVsixExePath_Click(object sender, EventArgs e)
        {
            if (this.dlgAdd.ShowDialog() != DialogResult.OK)
                return;
            this._vsixItem.PublisherExePath = this.txtVsixExePath.Text = this.dlgAdd.FileName;
        }

        private void btnMSBuild_Click(object sender, EventArgs e)
        {
            if (this.dlgAdd.ShowDialog() != DialogResult.OK)
                return;
            this._vsixItem.MSBuildExePath = this.txtMSBuild.Text = this.dlgAdd.FileName;
        }

        private void dgwMain_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode != Keys.Space)
            //    return;
            //DataGridViewCell cell = this.dgwMain.SelectedRows[0].Cells[0];
            //cell.Value = (object)!Convert.ToString(cell.Value).ToBoolean(false);
        }

        private void cbxVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblBeta.Visible = this.txtBeta.Visible = this.cbxVersion.SelectedIndex == 4;
        }
    }
}
