using AiUoVsix.Command.SqlSugarGen.Common;
using AiUoVsix.Common;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace AiUoVsix.Command.SqlSugarGen.Templates
{
    [GeneratedCode("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public class SqlSugarPartialEO : SqlSugarPartialEOBase
    {
        private ConnectionElement _conn;
        private string _tableName;
        // private List<ColumnSchema> _columns;

        public virtual string TransformText()
        {
            Write("using System;\r\nusing System.Linq;\r\nusing System.Text;\r\nusing SqlSugar;\r\nusing TinyFx.Data.SqlSugar;\r\n\r\nnamespace ");
            Write(ToStringHelper.ToStringWithCulture(EONamespace));
            Write("\r\n{\r\n");
            if (UseConfigId)
            {
                Write("    \r\n    [SugarConfigId(\"");
                Write(ToStringHelper.ToStringWithCulture(ConfigId));
                Write("\")]\r\n");
            }
            Write("    public partial class ");
            Write(ToStringHelper.ToStringWithCulture(EOClassName));
            Write("\r\n    {\r\n");
            if (UseTinyFx)
            {
                Write("        #region tinyfx\r\n        public static implicit operator ");
                Write(ToStringHelper.ToStringWithCulture(EOTinyFx));
                Write("(");
                Write(ToStringHelper.ToStringWithCulture(EOClassName));
                Write(" value)\r\n        {\r\n            if (value==null) return null;\r\n            return new ");
                Write(ToStringHelper.ToStringWithCulture(EOTinyFx));
                Write("\r\n            {\r\n");
                // foreach (ColumnSchema column in Columns)
                //{
                //    Write("\t\t        ");
                //    Write(ToStringHelper.ToStringWithCulture((object)column.OrmPropertyName));
                //    Write(" = value.");
                //    Write(ToStringHelper.ToStringWithCulture((object)column.OrmPropertyName));
                //    Write(",\r\n");
                //}
                Write("            };\r\n        }\r\n        public static implicit operator ");
                Write(ToStringHelper.ToStringWithCulture(EOClassName));
                Write("(");
                Write(ToStringHelper.ToStringWithCulture(EOTinyFx));
                Write(" value)\r\n        {\r\n            if (value==null) return null;\r\n            return new ");
                Write(ToStringHelper.ToStringWithCulture(EOClassName));
                Write("\r\n            {\r\n");
                //foreach (ColumnSchema column in Columns)
                //{
                //    Write("\t\t        ");
                //    Write(ToStringHelper.ToStringWithCulture((object)column.OrmPropertyName));
                //    Write(" = value.");
                //    Write(ToStringHelper.ToStringWithCulture((object)column.OrmPropertyName));
                //    Write(",\r\n");
                //}
                Write("            };\r\n        }\r\n        #endregion\r\n");
            }
            Write("    }\r\n}");
            return GenerationEnvironment.ToString();
        }

        public string EOClassName { get; set; }

        public string EOTinyFx { get; set; }

        public string EONamespace { get; set; }

        public bool UseTinyFx => _conn.Partial == PartialMode.TinyOrm;

        public bool UseConfigId => _conn.UseSugarConfigId;

        public string ConfigId => _conn.Name;

        public SqlSugarPartialEO(ConnectionElement conn, string tableName, string nameSpace)
        {
            _conn = conn;
            _tableName = tableName;
            EOClassName = tableName.PascalCase() + "PO";
            EOTinyFx = tableName.PascalCase() + "EO";
            EONamespace = nameSpace;
        }

        //public List<ColumnSchema> Columns
        //{
        //    get
        //    {
        //        if (_columns == null)
        //            _columns = new MySqlSchemaProvider(new MySqlDatabase(_conn.ConnectionString)).GetTableColumns(_tableName).ToList<ColumnSchema>();
        //        return _columns.ToList();
        //    }
        //}
    }
}
