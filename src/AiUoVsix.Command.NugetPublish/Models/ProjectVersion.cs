using System;

namespace AiUoVsix.Command.NugetPublish.Models
{

    public class ProjectVersion
    {
        public int Major { get; set; }

        public int Minor { get; set; }

        public int Build { get; set; }

        public string Suffix { get; set; }

        public ProjectVersion(string version)
        {
            string[] strArray = version.Split('-');
            Version version1 = new Version(strArray[0]);
            this.Major = version1.Major;
            this.Minor = version1.Minor;
            this.Build = version1.Build;
            if (strArray.Length != 2)
                return;
            this.Suffix = strArray[1];
        }

        public ProjectVersion(int major, int minor, int build, string suffix)
        {
            this.Major = major;
            this.Minor = minor;
            this.Build = build;
            this.Suffix = suffix;
        }

        public override string ToString()
        {
            string str = string.Format("{0}.{1}.{2}", (object)this.Major, (object)this.Minor, (object)this.Build);
            if (!string.IsNullOrEmpty(this.Suffix))
                str = str + "-" + this.Suffix;
            return str;
        }
    }
}
