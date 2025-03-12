using AiUoVsix.Common;
using System;
using System.IO;

namespace AiUoVsix.Command.SqlSugarGen.Common
{
    internal static class GenUtil
    {
        private const string SectionName = "SqlSugar";
        public static GenOptions Options;

        public static EnvDTEWraper CurrentDTE { get; private set; }

        public static VsixConfigFile ConfigFile { get; private set; }

        public static string BasePath => GenUtil.ConfigFile.BasePath;

        public static void Init(EnvDTEWraper dte = null)
        {
            GenUtil.CurrentDTE = dte;
            GenUtil.ConfigFile = new VsixConfigFile(dte);
            GenUtil.Options = GenUtil.ConfigFile.GetSection<GenOptions>("SqlSugar");
        }

        public static void SaveConfig()
        {
            GenUtil.ConfigFile.SetSection("SqlSugar", (object)GenUtil.Options);
            GenUtil.ConfigFile.Save();
        }

        public static string GetProjectName() => GenUtil.CurrentDTE?.ActiveProject.Name;

        public static void AddDteProjectFile(string file)
        {
            GenUtil.CurrentDTE?.ActiveProject.AddFromFile(file);
        }

        public static string GetProjectDir()
        {
            return Path.GetDirectoryName(GenUtil.CurrentDTE?.ActiveProject.FullName);
        }

        public static ConnectionElement GetDefaultElement()
        {
            return GenUtil.Options.Elements.Find((Predicate<ConnectionElement>)(x => x.Name == GenUtil.Options.DefaultElement));
        }
    }
}
