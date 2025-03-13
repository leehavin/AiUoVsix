using System;
using System.IO;

namespace AiUoVsix.Common
{
    public class VsixConfigFile
    {
        public static string FILE_NAME = "aiuo.vsix.json";

        private EnvDTEWraper _dte;

        protected MultiConfigFile _configFile;

        public string BasePath { get; }

        public string FilePath { get; }

        public VsixConfigFile(EnvDTEWraper dte)
        {
            _dte = dte;
            string text = dte?.ActiveProject?.FileName;
            BasePath = ((text != null) ? Path.GetDirectoryName(text) : AppContext.BaseDirectory);
            FilePath = Path.Combine(BasePath, FILE_NAME);
            _configFile = new MultiConfigFile(FilePath);
        }

        public bool ExistsConfigFile()
        {
            return File.Exists(FilePath);
        }

        public bool TryGetSection<T>(string sectionName, out T ret) where T : class
        {
            ret = _configFile.GetSection<T>(sectionName);
            return ret != null;
        }

        public T GetSection<T>(string sectionName) where T : class
        {
            return _configFile.GetSection<T>(sectionName) ?? ReflectionUtil.CreateInstance<T>(Array.Empty<object>());
        }

        public void SetSection(string sectionName, object objSection)
        {
            _configFile.SetSection(sectionName, objSection);
            _configFile.Save();
        }

        public void Save()
        {
            _configFile.Save();
        }
    }
}
