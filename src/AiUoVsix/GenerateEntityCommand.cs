using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using Task = System.Threading.Tasks.Task;

namespace AiUoVsix
{
    internal sealed class GenerateEntityCommand
    {
        public const int CommandId = 0x010011;
        public static readonly Guid CommandSet = new Guid("c35419c1-8b14-4889-9e58-71c9c6a7c143");
        private readonly AsyncPackage package;

        private GenerateEntityCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static GenerateEntityCommand Instance
        {
            get;
            private set;
        }

        private IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return package;
            }
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new GenerateEntityCommand(package, commandService);
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var form = new GenerateEntityForm();
            form.ShowDialog();
        }
    }
}
