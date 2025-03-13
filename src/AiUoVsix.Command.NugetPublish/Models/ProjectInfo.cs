using AiUoVsix.Command.NugetPublish.Enums;
using System;

namespace AiUoVsix.Command.NugetPublish.Models
{ 
    public class ProjectInfo : IComparable<ProjectInfo>
    {
        public string ProjectName { get; set; }

        public ProjectVersion Version { get; set; }

        public ProjectType ProjectType { get; set; }

        public string ProjectTypeString { get; set; }

        public PublishType PublishType { get; set; }

        public bool Status { get; set; }

        public string ProjectPath { get; set; }

        public int CompareTo(ProjectInfo other)
        {
            int num = this.ProjectType.CompareTo((object)other.ProjectType);
            if (num == 0)
                num = this.ProjectName.CompareTo(other.ProjectName);
            return num;
        }
    }
}
