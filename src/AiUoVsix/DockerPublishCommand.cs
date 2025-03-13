﻿using AiUoVsix.Command.DockerPublish;
using AiUoVsix.Common;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace AiUoVsix
{
    public class DockerPublishCommand
    {
        public const int CommandId = 4150;
        public static readonly Guid CommandSet = new Guid("f8cee976-c0f5-46c8-a8e6-da9c954e5f58");
        private readonly AsyncPackage package;

        private DockerPublishCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            MenuCommand command = new MenuCommand(new EventHandler(this.Execute), new CommandID(DockerPublishCommand.CommandSet, 4150));
            ((MenuCommandService)commandService).AddCommand(command);
        }

        public static DockerPublishCommand Instance { get; private set; }

        private IAsyncServiceProvider ServiceProvider => (IAsyncServiceProvider)this.package;

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            object obj = await package.GetServiceAsync(typeof(IMenuCommandService));
            OleMenuCommandService commandService = obj as OleMenuCommandService;
            obj = (object)null;
            DockerPublishCommand.Instance = new DockerPublishCommand(package, commandService);
            commandService = (OleMenuCommandService)null;
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread(nameof(Execute));
            int num = (int)new MainForm(new EnvDTEWraper(this.ServiceProvider?.GetServiceAsync(typeof(DTE))?.Result as DTE)).ShowDialog();
        }
    }
}
