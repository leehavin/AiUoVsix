using AiUoVsix.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

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
            var dictionary = new Dictionary<string, List<string>>();

            using (SqlSugarClient database = this.GetDatabase())
            {
                foreach (string name in names)
                {
                    string eoClassName = this.GetEoClassName(name);
                    database.MappingTables.Add(eoClassName, name);
                    string[] strArray = name.Split('_');
                    string key = strArray[0] != "v" || strArray.Length <= 2 || database.DbMaintenance.IsAnyTable(name) ? strArray[0] : strArray[1];

                    if (dictionary.ContainsKey(key))
                    {
                        dictionary[key].Add(name);
                    }
                    else
                    {
                        dictionary.Add(key, new List<string>() { name });
                    }
                }

                foreach (var keyValuePair in dictionary)
                {
                    string str = this._conn.UseSubPath ? Path.Combine(this._baseOutput, keyValuePair.Key) : this._baseOutput;
                    database.DbFirst.IsCreateAttribute()
                        .IsCreateDefaultValue()
                        .Where(keyValuePair.Value.ToArray())
                        .FormatFileName(x => ToPascalCase(x) + "Entity")
                        .FormatClassName(x=> ToPascalCase(x))
                     .SettingPropertyTemplate(((column, tmpl, type) =>
                    {
                        string format = "\r\n           [SugarColumn({0})]";
                        List<string> values = new List<string>();
                        if (column.IsPrimarykey)
                            values.Add("IsPrimaryKey=true");
                        if (column.IsIdentity)
                            values.Add("IsIdentity=true");
                        if (values.Count == 0)
                            format = "";

                        return tmpl.Replace("{PropertyType}", type)
                        .Replace("{PropertyName}", column.DbColumnName.PascalCase())
                        .Replace("{SugarColumn}", string.Format(format, string.Join(",", values)));

                    })).CreateClassFile(str, this._ns);

                    foreach (string tableName in keyValuePair.Value)
                    {
                        string path = Path.Combine(str, tableName + ".partial.cs");
                        switch (this._conn.Partial)
                        {
                            case PartialMode.Empty:
                            case PartialMode.TinyOrm:
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

        /// <summary>
        /// 转换首字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string ToPascalCase(string input)
        {
            // 使用正则表达式匹配下划线后的字母，并将其转换为大写
            string result = Regex.Replace(input, "_([a-z])", match => match.Groups[1].Value.ToUpper());
            // 将整个字符串的首字母也转换为大写
            result = char.ToUpper(result[0]) + result.Substring(1);
            return result;
        }

        private string GetEoClassName(string name) => ToPascalCase(name) + "Entity";

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
