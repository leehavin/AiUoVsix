using AiUoVsix.Command.NugetPublish.Enums;

namespace AiUoVsix.Command.NugetPublish.Models
{
    public class NugetSourceItem
    {
        public string NugetKey { get; set; }

        public string NugetSource { get; set; }

        public string SymbolKey { get; set; }

        public string SymbolSource { get; set; }

        public NugetMode Mode { get; set; } = NugetMode.None;
    }
}
