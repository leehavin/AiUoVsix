using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AiUoVsix.Command.EntityFrameworkCore.Models;

namespace AiUoVsix.Command.EntityFrameworkCore.Services
{
    public class DatabaseService
    {
        public bool TestConnection(string connectionString, string databaseType)
        {
            try
            {
                switch (databaseType.ToLower())
                {
                    case "sql server":
                        using (var connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            return true;
                        }
                    // 其他数据库类型可以在这里添加
                    default:
                        throw new NotSupportedException($"不支持的数据库类型: {databaseType}");
                }
            }
            catch
            {
                return false;
            }
        }
        
        public List<TableInfo> GetTables(string connectionString, string databaseType)
        {
            var tables = new List<TableInfo>();
            
            try
            {
                switch (databaseType.ToLower())
                {
                    case "sql server":
                        tables = GetSqlServerTables(connectionString);
                        break;
                    default:
                        throw new NotSupportedException($"不支持的数据库类型: {databaseType}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"获取表信息失败: {ex.Message}");
            }
            
            return tables;
        }
        
        private List<TableInfo> GetSqlServerTables(string connectionString)
        {
            var tables = new List<TableInfo>();
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                var sql = @"
                    SELECT 
                        t.TABLE_SCHEMA as [Schema],
                        t.TABLE_NAME as TableName,
                        t.TABLE_TYPE as TableType,
                        ISNULL(ep.value, '') as Comment
                    FROM INFORMATION_SCHEMA.TABLES t
                    LEFT JOIN sys.tables st ON st.name = t.TABLE_NAME
                    LEFT JOIN sys.extended_properties ep ON ep.major_id = st.object_id AND ep.minor_id = 0 AND ep.name = 'MS_Description'
                    WHERE t.TABLE_TYPE = 'BASE TABLE'
                    ORDER BY t.TABLE_SCHEMA, t.TABLE_NAME";
                
                using (var command = new SqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(new TableInfo
                        {
                            Schema = reader["Schema"].ToString(),
                            TableName = reader["TableName"].ToString(),
                            TableType = reader["TableType"].ToString(),
                            Comment = reader["Comment"].ToString(),
                            IsSelected = false
                        });
                    }
                }
            }
            
            return tables;
        }
    }
}