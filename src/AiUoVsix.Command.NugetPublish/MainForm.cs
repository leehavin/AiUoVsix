using AiUoVsix.Command.NugetPublish.Enums;
using AiUoVsix.Command.NugetPublish.Models;
using AiUoVsix.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            this._basePath = MainForm.CurrentDTE != null ? Path.GetDirectoryName(MainForm.CurrentDTE.SolutionFile) : Application.StartupPath;
            this._configFile = Path.Combine(this._basePath, "nuget.json");
            this.txtConfigPath.Text = this._configFile;
            this._nupkgsPath = Path.Combine(this._basePath, "nupkgs");
            this.txtNupkgs.Text = this._nupkgsPath;
            this.cbxVersion.SelectedIndex = 1;
            PublishConfig publishConfig;
            if (File.Exists(this._configFile))
            {
                publishConfig = JsonConvert.DeserializeObject<PublishConfig>(File.ReadAllText(this._configFile));
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
                {

                }
            }
            this._nugetItems = publishConfig.NugetItems;
            this.chkGit.Checked = publishConfig.IsGitCommit;
            this.BindCbxNuget(publishConfig.NugetIndex);
            this._projectList = new Dictionary<string, ProjectInfo>();
            foreach (string project in publishConfig.Projects)
            {
                string absolutePath = IOUtil.GetAbsolutePath(this._basePath, project);
                this._projectList.Add(absolutePath, this._projectParser.ParseInfo(absolutePath, this._basePath));
            }
            this.dlgAdd.InitialDirectory = this._basePath;
            this.BindGrid();
            this._vsixItem = publishConfig.VsixItem ?? new VsixItem();
            this.BindVsix();
        }

        private void BindCbxNuget(int index)
        {
            if (this._nugetItems.Count > 0)
            {
                this.cbxNuget.DataSource = this._nugetItems.Select((item => item.NugetSource)).ToList();
                this.cbxNuget.SelectedIndex = index;
                this.cbxVersion.SelectedIndex = (int)this._nugetItems[index].Mode;
            }
            else
                this.cbxNuget.DataSource = null;
        }

        private void BindGrid()
        {
            var list = this._projectList.Values.ToList();
            list.Sort();
            this.dgwMain.DataSource = list;
        }

        private void BindVsix()
        {
            if (!string.IsNullOrEmpty(this._vsixItem.ProjectPath))
                this.txtVsixProject.Text = IOUtil.GetAbsolutePath(this._basePath, this._vsixItem.ProjectPath);
            if (!string.IsNullOrEmpty(this._vsixItem.VsixPath))
                this.txtVsixExePath.Text = IOUtil.GetAbsolutePath(this._basePath, this._vsixItem.VsixPath);
            if (!string.IsNullOrEmpty(this._vsixItem.ConfigPath))
                this.txtVsixPath.Text = IOUtil.GetAbsolutePath(this._basePath, this._vsixItem.ConfigPath);
            if (!string.IsNullOrEmpty(this._vsixItem.Token))
                this.txtVsixToken.Text = this._vsixItem.Token;
            if (!string.IsNullOrEmpty(this._vsixItem.PublisherExePath))
                this.txtVsixExePath.Text = this._vsixItem.PublisherExePath;
            if (string.IsNullOrEmpty(this._vsixItem.MSBuildExePath))
                return;
            this.txtMSBuild.Text = this._vsixItem.MSBuildExePath;
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

            this.progressBar1.Maximum = projs.Count;
            this.EnableControls(true);
            this.tabMain.SelectTab(2);
            this.txtOutput.Clear();
            this.lblResult.Text = "正在生成...";
            this.bgwMain.RunWorkerAsync((object)(projs, this.cbxVersion.SelectedIndex, (NugetMode)this.cbxVersion.SelectedIndex, this.chkGit.Checked, this.chkRelease.Checked, this.txtBeta.Text));
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
    }
}
