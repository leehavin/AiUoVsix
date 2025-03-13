using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace AiUoVsix
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class GenerateEntityPackage : AsyncPackage
    {
        public const string PackageGuidString = "47c9875f-4d82-4357-ad2d-0c7d67a47b93";

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await GenerateEntityCommand.InitializeAsync(this);
            await DockerPublishCommand.InitializeAsync(this);
            await SqlSugarGenCommand.InitializeAsync(this);
            await NugetPublishCommand.InitializeAsync(this);
        }
    }
}
