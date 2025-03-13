using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiUoVsix.Command.NugetPublish.Models
{
    public class PublishConfig
    {
        public List<NugetSourceItem> NugetItems = new List<NugetSourceItem>();

        public int NugetIndex { get; set; }

        public bool IsGitCommit { get; set; }

        public List<string> Projects { get; set; } = new List<string>();

        public VsixItem VsixItem { get; set; }
    }
}
