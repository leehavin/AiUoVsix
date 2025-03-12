using AiUoVsix.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;

namespace AiUoVsix.Command.SqlSugarGen.Common
{
    internal class GenService
    {
        private ConnectionElement _conn;
        private string _baseOutput;
        private string _ns;

        public GenService(ConnectionElement conn)
        {
            this._conn = conn;
            this._baseOutput = Path.Combine(GenUtil.BasePath, conn.OutputPath);
            this._ns = conn.Namespace;
        }

        public void Execute(List<string> names)
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            using (SqlSugarClient database = this.GetDatabase())
            {
                foreach (string name in names)
                {
                    string eoClassName = this.GetEoClassName(name);
                    database.MappingTables.Add(eoClassName, name);
                    string[] strArray = name.Split('_');
                    string key = !(strArray[0] == "v") || strArray.Length <= 2 || database.DbMaintenance.IsAnyTable(name) ? strArray[0] : strArray[1];
                    if (dictionary.ContainsKey(key))
                        dictionary[key].Add(name);
                    else
                        dictionary.Add(key, new List<string>() { name });
                }
                foreach (KeyValuePair<string, List<string>> keyValuePair in dictionary)
                {
                    string str = this._conn.UseSubPath ? Path.Combine(this._baseOutput, keyValuePair.Key) : this._baseOutput;
                    database.DbFirst.IsCreateAttribute().IsCreateDefaultValue().Where(keyValuePair.Value.ToArray()).FormatFileName((Func<string, string>)(x => x.TrimEnd("PO", false).ToLower())).SettingPropertyTemplate((Func<DbColumnInfo, string, string, string>)((column, tmpl, type) =>
                    {
                        string format = "\r\n           [SugarColumn({0})]";
                        List<string> values = new List<string>();
                        if (column.IsPrimarykey)
                            values.Add("IsPrimaryKey=true");
                        if (column.IsIdentity)
                            values.Add("IsIdentity=true");
                        if (values.Count == 0)
                            format = "";
                        return tmpl.Replace("{PropertyType}", type).Replace("{PropertyName}", column.DbColumnName.PascalCase()).Replace("{SugarColumn}", string.Format(format, (object)string.Join(",", (IEnumerable<string>)values)));
                    })).CreateClassFile(str, this._ns);
                    foreach (string tableName in keyValuePair.Value)
                    {
                        string path = Path.Combine(str, tableName + ".partial.cs");
                        switch (this._conn.Partial)
                        {
                            case PartialMode.Empty:
                            case PartialMode.TinyOrm:
                                //  string contents = new SqlSugarPartialEO(this._conn, tableName, this._ns).TransformText();
                                // File.WriteAllText(path, contents);
                                break;
                            case PartialMode.Delete:
                                if (File.Exists(path))
                                {
                                    File.Delete(path);
                                    break;
                                }
                                break;
                        }
                    }
                }
            }
        }

        private string GetEoClassName(string name) => name.PascalCase() + "PO";

        private SqlSugarClient GetDatabase()
        {
            return new SqlSugarClient(new ConnectionConfig()
            {
                DbType = this._conn.DatabaseType,
                ConnectionString = this._conn.ConnectionString,
                IsAutoCloseConnection = true
            });
        }
    }
}
