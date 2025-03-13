using System;
using System.Diagnostics;
using System.IO;

namespace AiUoVsix.Command.NugetPublish
{

    public class CommandRunner
    {
        public string ExecutablePath { get; }

        public string WorkingDirectory { get; }

        public CommandRunner(string executablePath, string workingDirectory = null)
        {
            this.ExecutablePath = executablePath ?? throw new ArgumentNullException(nameof(executablePath));
            this.WorkingDirectory = workingDirectory ?? Path.GetDirectoryName(executablePath);
        }

        public string Run(string arguments)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(this.ExecutablePath, arguments)
            {
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WorkingDirectory = this.WorkingDirectory
            };
            Process process = new Process()
            {
                StartInfo = processStartInfo
            };
            process.Start();
            return process.StandardOutput.ReadToEnd();
        }
    }
}
