using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace AiUoVsix
{
    public class EntityFrameworkCoreCommand
    {
        public const int CommandId = 0x0104;
        public static readonly Guid CommandSet = new Guid("c35419c1-8b14-4889-9e58-71c9c6a7c143");
        private readonly AsyncPackage package;

        public EntityFrameworkCoreCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }


        public static EntityFrameworkCoreCommand Instance { get; private set; }

        private IAsyncServiceProvider ServiceProvider => this.package;

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new EntityFrameworkCoreCommand(package, commandService);
        }

        private void Execute(object sender, EventArgs e)
        {
            try
            {
                ThreadHelper.ThrowIfNotOnUIThread(nameof(Execute));
                var dte = ServiceProvider.GetServiceAsync(typeof(DTE)).Result as DTE;
                
                // 启动EntityFrameworkCore应用程序
                var exePath = System.IO.Path.Combine(
                    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    "AiUoVsix.Command.EntityFrameworkCore.exe");
                
                if (System.IO.File.Exists(exePath))
                {
                    System.Diagnostics.Process.Start(exePath);
                }
                else
                {
                    VsShellUtilities.ShowMessageBox(this.package, 
                        $"找不到EntityFrameworkCore应用程序: {exePath}", 
                        "错误", 
                        OLEMSGICON.OLEMSGICON_WARNING,
                        OLEMSGBUTTON.OLEMSGBUTTON_OK,
                        OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                }
            }
            catch (Exception ex)
            {
                VsShellUtilities.ShowMessageBox(this.package, ex.Message, "出现错误", OLEMSGICON.OLEMSGICON_CRITICAL,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }
        }
    }
}
