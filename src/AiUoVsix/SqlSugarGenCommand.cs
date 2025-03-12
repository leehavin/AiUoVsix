using AiUoVsix.Command.SqlSugarGen;
using AiUoVsix.Common;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace AiUoVsix
{
    public class SqlSugarGenCommand
    {
        public const int CommandId = 4132;
        public static readonly Guid CommandSet = new Guid("f8cee976-c0f5-46c8-a8e6-da9c954e5f58");
        private readonly AsyncPackage package;

        private SqlSugarGenCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            MenuCommand command = new MenuCommand(new EventHandler(this.Execute), new CommandID(SqlSugarGenCommand.CommandSet, 4132));
            ((MenuCommandService)commandService).AddCommand(command);
        }

        public static SqlSugarGenCommand Instance { get; private set; }

        private IAsyncServiceProvider ServiceProvider => (IAsyncServiceProvider)this.package;

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            object obj = await package.GetServiceAsync(typeof(IMenuCommandService));
            OleMenuCommandService commandService = obj as OleMenuCommandService;
            obj = (object)null;
            SqlSugarGenCommand.Instance = new SqlSugarGenCommand(package, commandService);
            commandService = (OleMenuCommandService)null;
        }

        private void Execute(object sender, EventArgs e)
        {
            try
            {
                ThreadHelper.ThrowIfNotOnUIThread(nameof(Execute));
                int num = (int)new MainForm(new EnvDTEWraper(this.ServiceProvider?.GetServiceAsync(typeof(DTE))?.Result as DTE)).ShowDialog();
            }
            catch (Exception ex)
            {
                VsShellUtilities.ShowMessageBox((IServiceProvider)this.package, ex.Message, "出现错误", (OLEMSGICON)1, (OLEMSGBUTTON)0, (OLEMSGDEFBUTTON)0);
            }
        }
    }
}
