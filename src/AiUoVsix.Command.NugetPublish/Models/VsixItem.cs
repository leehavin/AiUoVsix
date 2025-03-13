namespace AiUoVsix.Command.NugetPublish.Models
{
    public class VsixItem
    {
        public string ProjectPath { get; set; }

        public string VsixPath { get; set; }

        public string ConfigPath { get; set; }

        public string Token { get; set; }

        public string PublisherExePath { get; set; }

        public string MSBuildExePath { get; set; }
    }
}
