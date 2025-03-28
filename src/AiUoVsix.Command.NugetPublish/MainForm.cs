﻿using AiUoVsix.Command.NugetPublish.Enums;
using AiUoVsix.Command.NugetPublish.Models;
using AiUoVsix.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AiUoVsix.Command.NugetPublish
{
    public partial class MainForm : Form
    {
        private SynchronizationContext _synchronizationContext;
        private string _basePath;
        private string _configFile;
        private string _nupkgsPath;
        private List<NugetSourceItem> _nugetItems = new List<NugetSourceItem>();
        private Dictionary<string, ProjectInfo> _projectList;
        private NugetSourceItem _selectedNugetSource;
        private ProjectInfoParser _projectParser = new ProjectInfoParser();
        private VsixItem _vsixItem;
        private ProjectInfo _vsixProjectInfo;

        public static EnvDTEWraper CurrentDTE { get; set; }

        public MainForm(EnvDTEWraper dte = null)
        {
            InitializeComponent();
            MainForm.CurrentDTE = dte;
            this._synchronizationContext = SynchronizationContext.Current;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _basePath = ((CurrentDTE != null) ? Path.GetDirectoryName(CurrentDTE.SolutionFile) : Application.StartupPath);
            _configFile = Path.Combine(_basePath, "nuget.json");
            txtConfigPath.Text = _configFile;
            _nupkgsPath = Path.Combine(_basePath, "nupkgs");
            txtNupkgs.Text = _nupkgsPath;
            cbxVersion.SelectedIndex = 1;
            PublishConfig publishConfig;
            if (File.Exists(_configFile))
            {
                publishConfig = JsonConvert.DeserializeObject<PublishConfig>(File.ReadAllText(_configFile));
            }
            else
            {
                publishConfig = new PublishConfig();
                publishConfig.NugetItems.Add(new NugetSourceItem
                {
                    NugetSource = "https://api.nuget.org/v3/index.json",
                    NugetKey = "oy2azc4vyfqzcrqqk6raqd2ltz6yqiuuzlozz366ws3mci",
                    Mode = NugetMode.None
                });
                publishConfig.VsixItem = new VsixItem
                {
                    Token = "3twuo3jaffjjlqjzbn66ijxcma2qffswyt7ajekwnnsg62jbewgq"
                };
            }
            _nugetItems = publishConfig.NugetItems;
            chkGit.Checked = publishConfig.IsGitCommit;
            BindCbxNuget(publishConfig.NugetIndex);
            _projectList = new Dictionary<string, ProjectInfo>();
            foreach (string project in publishConfig.Projects)
            {
                string absolutePath = IOUtil.GetAbsolutePath(_basePath, project);
                _projectList.Add(absolutePath, _projectParser.ParseInfo(absolutePath, _basePath));
            }
            dlgAdd.InitialDirectory = _basePath;
            BindGrid();
            _vsixItem = publishConfig.VsixItem ?? new VsixItem();
            BindVsix();
        }

        private void BindCbxNuget(int index)
        {
            if (_nugetItems.Count > 0)
            {
                cbxNuget.DataSource = _nugetItems.Select((NugetSourceItem item) => item.NugetSource).ToList();
                cbxNuget.SelectedIndex = index;
                NugetSourceItem nugetSourceItem = _nugetItems[index];
                cbxMode.SelectedIndex = (int)nugetSourceItem.Mode;
            }
            else
            {
                cbxNuget.DataSource = null;
            }
        }

        private void BindGrid()
        {
            var list = _projectList.Values.ToList();
            list.Sort();
            dgwMain.DataSource = list;
        }

        private void BindVsix()
        {
            if (!string.IsNullOrEmpty(_vsixItem.ProjectPath))
            {
                txtVsixProject.Text = IOUtil.GetAbsolutePath(_basePath, _vsixItem.ProjectPath);
            }
            if (!string.IsNullOrEmpty(_vsixItem.VsixPath))
            {
                txtVsixPath.Text = IOUtil.GetAbsolutePath(_basePath, _vsixItem.VsixPath);
            }
            if (!string.IsNullOrEmpty(_vsixItem.ConfigPath))
            {
                txtVsixConfig.Text = IOUtil.GetAbsolutePath(_basePath, _vsixItem.ConfigPath);
            }
            if (!string.IsNullOrEmpty(_vsixItem.Token))
            {
                txtVsixToken.Text = _vsixItem.Token;
            }
            if (!string.IsNullOrEmpty(_vsixItem.PublisherExePath))
            {
                txtVsixExePath.Text = _vsixItem.PublisherExePath;
            }
            if (!string.IsNullOrEmpty(_vsixItem.MSBuildExePath))
            {
                txtMSBuild.Text = _vsixItem.MSBuildExePath;
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this._vsixItem.Token = this.txtVsixToken.Text;
            var publishConfig = new PublishConfig()
            {
                NugetIndex = this.cbxNuget.SelectedIndex,
                NugetItems = this._nugetItems,
                IsGitCommit = this.chkGit.Checked,
                VsixItem = this._vsixItem
            };

            var list = this._projectList.Values.ToList();
            list.Sort();

            foreach (var projectInfo in list)
            {
                string relativePath = IOUtil.GetRelativePath(this._basePath, projectInfo.ProjectPath);
                publishConfig.Projects.Add(relativePath);
            }
            File.WriteAllText(this._configFile, JsonConvert.SerializeObject((object)publishConfig, Formatting.Indented));
            MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        /// <summary>
        /// NuGet发布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPublish_Click(object sender, EventArgs e)
        {
            if (this.cbxNuget.SelectedIndex == -1)
            {
                MessageBox.Show("请选择Nuget Source");
            }
            else
            {
                this.BuildAndPublish(this.GetPublishProjects());
            }
        }

        private void BuildAndPublish(List<ProjectInfo> projs)
        {
            if (projs.Count == 0)
                return;

            progressBar1.Maximum = projs.Count;
            EnableControls(executing: true);
            tabMain.SelectTab(2);
            txtOutput.Clear();
            lblResult.Text = "正在生成...";
            bgwMain.RunWorkerAsync((projs, cbxVersion.SelectedIndex, (NugetMode)cbxMode.SelectedIndex, chkGit.Checked, chkRelease.Checked, txtBeta.Text));
        }

        private void EnableControls(bool executing)
        {
            this.chkRelease.Enabled = this.cbxNuget.Enabled = this.btnAddSource.Enabled = this.btnEditSource.Enabled = this.btnDelSource.Enabled = this.btnPublish.Enabled = this.btnVsixPublish.Enabled = this.btnSave.Enabled = this.btnAddNuget.Enabled = this.btnNupkg.Enabled = this.dgwMain.Enabled = !executing;
            this.btnCancel.Enabled = executing;
        }

        /// <summary>
        /// 获取发布项目
        /// </summary>
        /// <returns></returns>
        private List<ProjectInfo> GetPublishProjects()
        {
            var publishProjects = new List<ProjectInfo>();
            for (int index = 0; index < this.dgwMain.Rows.Count; ++index)
            {
                DataGridViewRow row = this.dgwMain.Rows[index];
                if (Convert.ToBoolean(row.Cells[0].Value))
                    publishProjects.Add((ProjectInfo)row.DataBoundItem);
            }
            return publishProjects;
        }

        /// <summary>
        /// 添加Nuget源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSource_Click(object sender, EventArgs e)
        {
            AddSourceForm addSourceForm = new AddSourceForm();
            if (addSourceForm.ShowDialog() != DialogResult.OK)
                return;

            this._nugetItems.Add(new NugetSourceItem()
            {
                NugetSource = addSourceForm.NugetSource,
                NugetKey = addSourceForm.NugetKey,
                SymbolKey = addSourceForm.SymbolKey,
                SymbolSource = addSourceForm.SymbolSource,
                Mode = addSourceForm.Mode
            });
            this.BindCbxNuget(this._nugetItems.Count - 1);
        }

        /// <summary>
        /// 修改Nuget
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditSource_Click(object sender, EventArgs e)
        {
            AddSourceForm addSourceForm = new AddSourceForm(this._nugetItems[this.cbxNuget.SelectedIndex]);
            if (addSourceForm.ShowDialog() != DialogResult.OK)
                return;
            NugetSourceItem nugetItem = this._nugetItems[this.cbxNuget.SelectedIndex];
            nugetItem.NugetKey = addSourceForm.NugetKey;
            nugetItem.NugetSource = addSourceForm.NugetSource;
            nugetItem.Mode = addSourceForm.Mode;
            this.BindCbxNuget(this.cbxNuget.SelectedIndex);
        }

        /// <summary>
        /// 修改Nuget
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelSource_Click(object sender, EventArgs e)
        {
            this._nugetItems.RemoveAt(this.cbxNuget.SelectedIndex);
            this.BindCbxNuget(0);
        }

        /// <summary>
        /// 发布Vsix插件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVsixPublish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtVsixProject.Text) || string.IsNullOrEmpty(this.txtVsixExePath.Text) || string.IsNullOrEmpty(this.txtMSBuild.Text) || string.IsNullOrEmpty(this.txtVsixPath.Text) || string.IsNullOrEmpty(this.txtVsixPath.Text) || string.IsNullOrEmpty(this.txtVsixToken.Text))
            {
                MessageBox.Show("VSIX信息不能为空");
            }
            this.BuildAndPublish(new List<ProjectInfo>() { this._vsixProjectInfo });
        }

        /// <summary>
        /// 添加NuGet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNuget_Click(object sender, EventArgs e)
        {
            if (this.dlgAdd.ShowDialog() != DialogResult.OK)
                return;
            string fileName = this.dlgAdd.FileName;
            if (this._projectList.ContainsKey(fileName))
            {
                MessageBox.Show("项目已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this._projectList.Add(fileName, this._projectParser.ParseInfo(fileName, this._basePath));
                this.BindGrid();
            }
        }

        /// <summary>
        /// 点击Nupkg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNupkg_Click(object sender, EventArgs e)
        {
            new PublishNupkgForm(this._nugetItems == null || this._nugetItems.Count <= 0 ? null : this._nugetItems[this.cbxNuget.SelectedIndex]).ShowDialog();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var projectInfo = (ProjectInfo)dgwMain.SelectedRows[0].DataBoundItem;
            _projectList.Remove(projectInfo.ProjectPath);
            BindGrid();
        }

        /// <summary>
        /// 编译并发布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuildDeployToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = (ProjectInfo)dgwMain.SelectedRows[0].DataBoundItem;
            List<ProjectInfo> projs = new List<ProjectInfo> { item };
            BuildAndPublish(projs);
        }
          
        /// <summary>
        /// NuGet选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxNuget_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._selectedNugetSource = this._nugetItems[this.cbxNuget.SelectedIndex];
            this.cbxMode.SelectedIndex = (int)this._selectedNugetSource.Mode;
        }

        /// <summary>
        /// 进程显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void bgwMain_DoWork(object sender, DoWorkEventArgs e)
        {
            var tuple = ((List<ProjectInfo>, int, NugetMode, bool, bool, string))e.Argument;
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            foreach (ProjectInfo item2 in tuple.Item1)
            {
                if (item2.Status)
                {
                    _projectParser.UpdateVersion(item2, tuple.Item2, tuple.Item6);
                    string text = item2.Version.ToString();
                    string item = item2.ProjectName + " v" + text;
                    if (dictionary.ContainsKey(text))
                    {
                        dictionary[text].Add(item);
                        continue;
                    }
                    dictionary.Add(text, new List<string> { item });
                }
            }

            var stringBuilder = new StringBuilder();
            if (dictionary.Count == 1)
            {
                stringBuilder.Append("发布版本: v" + dictionary.First().Key);
            }
            else
            {
                stringBuilder.Append("发布版本:");
                foreach (List<string> value in dictionary.Values)
                {
                    stringBuilder.Append(string.Join(";", value));
                }
            }

            if (tuple.Item4)
            {
                CommandRunner commandRunner = new CommandRunner("git", _basePath);
                string gitOut = commandRunner.Run("add .") + Environment.NewLine;
                gitOut += commandRunner.Run("commit -m \"" + stringBuilder.ToString() + "\"");
                _synchronizationContext.Send(delegate
                {
                    txtDetail.AppendText(gitOut + Environment.NewLine);
                }, gitOut);
            }

            for (int i = 0; i < tuple.Item1.Count; i++)
            {
                if (bgwMain.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                ProjectInfo projectInfo = tuple.Item1[i];
                if (!projectInfo.Status)
                {
                    continue;
                }
                string cmd = null;
                string text2 = (tuple.Item5 ? "Release" : "Debug");
                switch (projectInfo.PublishType)
                {
                    case PublishType.Nuget:
                        switch (projectInfo.ProjectType)
                        {
                            case ProjectType.Standard:
                            case ProjectType.Core:
                            case ProjectType.Net:
                                {
                                    string text3 = string.Empty;
                                    switch (tuple.Item3)
                                    {
                                        case NugetMode.Symbols:
                                            text3 = "--include-symbols -p:SymbolPackageFormat=snupkg";
                                            break;
                                        case NugetMode.Embedded:
                                            text3 = "-p:DebugSymbols=true -p:DebugType=embedded";
                                            break;
                                    }
                                    cmd = "dotnet build " + projectInfo.ProjectPath + " -c " + text2;
                                    ExecAndOutput(cmd, i, projectInfo.ProjectName, "编译");
                                    cmd = "dotnet pack " + projectInfo.ProjectPath + " -c " + text2 + " -o " + _nupkgsPath + " --nologo -v m  " + text3;
                                    ExecAndOutput(cmd, i, projectInfo.ProjectName, "打包");
                                    break;
                                }
                            case ProjectType.Framework:
                                cmd = "nuget pack " + projectInfo.ProjectPath + " -OutputDirectory " + _nupkgsPath + " -Symbols -SymbolPackageFormat snupkg";
                                ExecAndOutput(cmd, i, projectInfo.ProjectName, "编译");
                                break;
                        }
                        break;
                    case PublishType.Vsix:
                        cmd = "\"" + txtMSBuild.Text + "\" " + projectInfo.ProjectPath + " -t:rebuild -p:Configuration=Debug";
                        ExecAndOutput(cmd, i, projectInfo.ProjectName, "编译");
                        break;
                }

                switch (projectInfo.PublishType)
                {
                    case PublishType.Nuget:
                        {
                            string text4 = Path.Combine(_nupkgsPath, projectInfo.ProjectName + "." + projectInfo.Version.ToString());
                            string text5 = text4 + ".nupkg";
                            if (!File.Exists(text5))
                            {
                                throw new Exception("文件不存在：" + text5);
                            }
                            NugetSourceItem selectedNugetSource = _selectedNugetSource;
                            if (projectInfo.ProjectType == ProjectType.Framework)
                            {
                                cmd = "nuget push " + text5 + " -ApiKey " + selectedNugetSource.NugetKey + " -Source " + selectedNugetSource.NugetSource;
                            }
                            else
                            {
                                cmd = "dotnet nuget push " + text5 + " -k " + selectedNugetSource.NugetKey + " -s " + selectedNugetSource.NugetSource;
                            }
                            ExecAndOutput(cmd, i, projectInfo.ProjectName, "发布.nupkg");
                            string text6 = text4 + ".snupkg";
                            if (File.Exists(text6))
                            {
                                cmd = "dotnet nuget push " + text6 + " -sk " + selectedNugetSource.SymbolKey + " -ss " + selectedNugetSource.SymbolSource;
                                ExecAndOutput(cmd, i, projectInfo.ProjectName, "发布.snupkg");
                            }
                            break;
                        }
                    case PublishType.Vsix:
                        cmd = "\"" + txtVsixExePath.Text + "\" publish -payload \"" + txtVsixPath.Text + "\" -publishManifest \"" + txtVsixConfig.Text + "\" -personalAccessToken \"" + txtVsixToken.Text + "\"";
                        _synchronizationContext.Send(delegate
                        {
                            txtDetail.AppendText(cmd + Environment.NewLine);
                        }, cmd);
                        ExecAndOutput(cmd, i, projectInfo.ProjectName, "发布.vsix");
                        break;
                }
                bgwMain.ReportProgress(i, i);
            }
        }

        /// <summary>
        /// 发布进程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMain_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = (int)e.UserState + 1;
        }

        /// <summary>
        /// 发布完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "出现异常: ";
                lblResult.Text += e.Error.Message;
            }
            else
            {
                lblResult.Text = "操作完成";
            }
            progressBar1.Value = 0;
            progressBar1.Maximum = 0;
            txtOutput.AppendText("============ 结 束 ==========");
            EnableControls(executing: false);
        }


        private void ExecAndOutput(string cmd, int index, string project, string type)
        {
            string strOut = null;
            if (WindowsUtil.RunCommand(cmd, out var output))
            {
                strOut += $"项目{index + 1}: {project} =>{type}成功。{Environment.NewLine}";
            }
            else
            {
                strOut += $"项目{index + 1}: {project} =>{type}失败！{Environment.NewLine}";
                strOut = strOut + "cmd: " + cmd + Environment.NewLine;
                strOut = strOut + output + Environment.NewLine;
            }
            _synchronizationContext.Send(delegate
            {
                txtOutput.AppendText(strOut);
            }, strOut);
            _synchronizationContext.Send(delegate
            {
                txtDetail.AppendText(output + Environment.NewLine);
            }, output);
        }
    }
}
