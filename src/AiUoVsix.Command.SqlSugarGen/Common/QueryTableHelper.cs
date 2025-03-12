using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO; 

namespace AiUoVsix.Command.SqlSugarGen.Common
{
  internal class QueryTableHelper
  {
    public static Dictionary<string, ListViewObjectItem> GetDbObjectDict(
      ConnectionElement conn,
      bool useCache)
    {
      Dictionary<string, ListViewObjectItem> ret = new Dictionary<string, ListViewObjectItem>();
            GetDatabase(conn).DbMaintenance.GetTableInfoList(useCache).ForEach((Action<DbTableInfo>) (x => ret.Add(x.Name, new ListViewObjectItem(x.Name, DbObjectType.Table, false))));
            GetDatabase(conn).DbMaintenance.GetViewInfoList(useCache).ForEach((Action<DbTableInfo>) (x => ret.Add(x.Name, new ListViewObjectItem(x.Name, DbObjectType.View, false))));
      return ret;
    }

    public static HashSet<string> GetDbObjects(List<ConnectionElement> conns)
    {
      HashSet<string> ret = new HashSet<string>();
      foreach (ConnectionElement conn in conns)
      {
        SqlSugarClient database = GetDatabase(conn);
        database.DbMaintenance.GetTableInfoList().ForEach( t =>
        {
          if (ret.Contains(t.Name))
            return;
          ret.Add(t.Name);
        });
        database.DbMaintenance.GetViewInfoList().ForEach( v =>
        {
          if (ret.Contains(v.Name))
            return;
          ret.Add(v.Name);
        });
      }
      return ret;
    }

    public static HashSet<string> GetFileTables(string outputPath)
    {
      HashSet<string> fileTables = new HashSet<string>();
      foreach (string file in Directory.GetFiles(outputPath, "*.cs", SearchOption.AllDirectories))
      {
        string withoutExtension = Path.GetFileNameWithoutExtension(file);
        if (!withoutExtension.EndsWith(".partial") && !fileTables.Contains(withoutExtension))
          fileTables.Add(withoutExtension);
      }
      return fileTables;
    }

    private static SqlSugarClient GetDatabase(ConnectionElement conn)
    {
      return new SqlSugarClient(new ConnectionConfig()
      {
        DbType = conn.DatabaseType,
        ConnectionString = conn.ConnectionString,
        IsAutoCloseConnection = true
      });
    }
  }
}
