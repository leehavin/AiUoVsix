using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace AiUoVsix.Common
{
    public class MultiConfigFile
    {
        public string ConfigFile { get; private set; }

        public JObject ConfigObject { get; private set; }

        public bool ExistsConfigFile => File.Exists(ConfigFile);

        public MultiConfigFile(string configFile)
        {
            //IL_0057: Unknown result type (might be due to invalid IL or missing references)
            //IL_0061: Expected O, but got Unknown
            //IL_002b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0031: Expected O, but got Unknown
            //IL_0039: Unknown result type (might be due to invalid IL or missing references)
            //IL_0043: Expected O, but got Unknown
            ConfigFile = configFile;
            if (File.Exists(ConfigFile))
            {
                JsonTextReader val = new JsonTextReader((TextReader)new StreamReader(ConfigFile));
                try
                {
                    ConfigObject = (JObject)JToken.ReadFrom((JsonReader)(object)val);
                    return;
                }
                finally
                {
                    ((IDisposable)val)?.Dispose();
                }
            }

            ConfigObject = new JObject();
        }

        public bool ExistsSection(string sectionName)
        {
            return ConfigObject[sectionName] != null;
        }

        public bool TryGetSection<T>(string sectionName, out T ret) where T : class
        {
            ret = GetSection<T>(sectionName);
            return ret != null;
        }

        public T GetSection<T>(string name) where T : class
        {
            JToken val = ConfigObject[name];
            return (val == null) ? null : val.ToObject<T>();
        }

        public MultiConfigFile SetSection(string name, object obj, bool notWriteNull = false)
        {
            JToken val = JToken.FromObject(obj);
            if (((IEnumerable<JToken>)val).Count() == 0 && notWriteNull)
            {
                return this;
            }

            ConfigObject[name] = val;
            return this;
        }

        public void Save()
        {
            //IL_000c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0012: Expected O, but got Unknown
            JsonTextWriter val = new JsonTextWriter((TextWriter)new StreamWriter(ConfigFile));
            try
            {
                ((JsonWriter)val).Formatting = (Formatting)1;
                ((JToken)ConfigObject).WriteTo((JsonWriter)(object)val, Array.Empty<JsonConverter>());
            }
            finally
            {
                ((IDisposable)val)?.Dispose();
            }
        }

        public override string ToString()
        {
            return ExistsConfigFile ? File.ReadAllText(ConfigFile) : string.Empty;
        }
    }
}
