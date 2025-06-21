using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using AiUoVsix.Command.EntityFrameworkCore.Models;

namespace AiUoVsix.Command.EntityFrameworkCore.Services
{
    public class DatabaseService
    {
        public async Task<bool> TestConnectionAsync(DatabaseConnection connection)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using var dbConnection = CreateConnection(connection);
                    dbConnection.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<List<DatabaseObject>> GetTablesAsync(DatabaseConnection connection)
        {
            return await Task.Run(() =>
            {
                var tables = new List<DatabaseObject>();
                
                try
                {
                    using var dbConnection = CreateConnection(connection);
                    dbConnection.Open();
                    
                    string query = connection.DatabaseType switch
                    {
                        DatabaseType.MySQL => "SELECT TABLE_NAME, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = @database AND TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME",
                        DatabaseType.SQLServer => "SELECT TABLE_NAME, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = @database AND TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME",
                        DatabaseType.SQLite => "SELECT name as TABLE_NAME, 'BASE TABLE' as TABLE_TYPE FROM sqlite_master WHERE type='table' ORDER BY name",
                        _ => throw new NotSupportedException($"Database type {connection.DatabaseType} is not supported")
                    };
                    
                    using var command = dbConnection.CreateCommand();
                    command.CommandText = query;
                    
                    if (connection.DatabaseType != DatabaseType.SQLite)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = "@database";
                        parameter.Value = connection.Database;
                        command.Parameters.Add(parameter);
                    }
                    
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        tables.Add(new DatabaseObject
                        {
                            Name = reader["TABLE_NAME"]?.ToString() ?? string.Empty,
                            Type = DatabaseObjectType.Table
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get tables: {ex.Message}", ex);
                }
                
                return tables;
            });
        }

        public async Task<List<DatabaseObject>> GetViewsAsync(DatabaseConnection connection)
        {
            return await Task.Run(() =>
            {
                var views = new List<DatabaseObject>();
                
                try
                {
                    using var dbConnection = CreateConnection(connection);
                    dbConnection.Open();
                    
                    string query = connection.DatabaseType switch
                    {
                        DatabaseType.MySQL => "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_SCHEMA = @database ORDER BY TABLE_NAME",
                        DatabaseType.SQLServer => "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_CATALOG = @database ORDER BY TABLE_NAME",
                        DatabaseType.SQLite => "SELECT name as TABLE_NAME FROM sqlite_master WHERE type='view' ORDER BY name",
                        _ => throw new NotSupportedException($"Database type {connection.DatabaseType} is not supported")
                    };
                    
                    using var command = dbConnection.CreateCommand();
                    command.CommandText = query;
                    
                    if (connection.DatabaseType != DatabaseType.SQLite)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = "@database";
                        parameter.Value = connection.Database;
                        command.Parameters.Add(parameter);
                    }
                    
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        views.Add(new DatabaseObject
                        {
                            Name = reader["TABLE_NAME"]?.ToString() ?? string.Empty,
                            Type = DatabaseObjectType.View
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get views: {ex.Message}", ex);
                }
                
                return views;
            });
        }

        public async Task<List<DatabaseObject>> GetFunctionsAsync(DatabaseConnection connection)
        {
            return await Task.Run(() =>
            {
                var functions = new List<DatabaseObject>();
                
                try
                {
                    using var dbConnection = CreateConnection(connection);
                    dbConnection.Open();
                    
                    string query = connection.DatabaseType switch
                    {
                        DatabaseType.MySQL => "SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA = @database AND ROUTINE_TYPE = 'FUNCTION' ORDER BY ROUTINE_NAME",
                        DatabaseType.SQLServer => "SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_CATALOG = @database AND ROUTINE_TYPE = 'FUNCTION' ORDER BY ROUTINE_NAME",
                        DatabaseType.SQLite => "SELECT '' as ROUTINE_NAME WHERE 1=0", // SQLite doesn't have functions in the same way
                        _ => throw new NotSupportedException($"Database type {connection.DatabaseType} is not supported")
                    };
                    
                    using var command = dbConnection.CreateCommand();
                    command.CommandText = query;
                    
                    if (connection.DatabaseType != DatabaseType.SQLite)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = "@database";
                        parameter.Value = connection.Database;
                        command.Parameters.Add(parameter);
                    }
                    
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        functions.Add(new DatabaseObject
                        {
                            Name = reader["ROUTINE_NAME"]?.ToString() ?? string.Empty,
                            Type = DatabaseObjectType.Function
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get functions: {ex.Message}", ex);
                }
                
                return functions;
            });
        }

        public async Task<List<DatabaseObject>> GetStoredProceduresAsync(DatabaseConnection connection)
        {
            return await Task.Run(() =>
            {
                var procedures = new List<DatabaseObject>();
                
                try
                {
                    using var dbConnection = CreateConnection(connection);
                    dbConnection.Open();
                    
                    string query = connection.DatabaseType switch
                    {
                        DatabaseType.MySQL => "SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA = @database AND ROUTINE_TYPE = 'PROCEDURE' ORDER BY ROUTINE_NAME",
                        DatabaseType.SQLServer => "SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_CATALOG = @database AND ROUTINE_TYPE = 'PROCEDURE' ORDER BY ROUTINE_NAME",
                        DatabaseType.SQLite => "SELECT '' as ROUTINE_NAME WHERE 1=0", // SQLite doesn't have stored procedures
                        _ => throw new NotSupportedException($"Database type {connection.DatabaseType} is not supported")
                    };
                    
                    using var command = dbConnection.CreateCommand();
                    command.CommandText = query;
                    
                    if (connection.DatabaseType != DatabaseType.SQLite)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = "@database";
                        parameter.Value = connection.Database;
                        command.Parameters.Add(parameter);
                    }
                    
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        procedures.Add(new DatabaseObject
                        {
                            Name = reader["ROUTINE_NAME"]?.ToString() ?? string.Empty,
                            Type = DatabaseObjectType.StoredProcedure
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get stored procedures: {ex.Message}", ex);
                }
                
                return procedures;
            });
        }

        public async Task<List<TableColumn>> GetTableColumnsAsync(DatabaseConnection connection, string tableName)
        {
            return await Task.Run(() =>
            {
                var columns = new List<TableColumn>();
                
                try
                {
                    using var dbConnection = CreateConnection(connection);
                    dbConnection.Open();
                    
                    string query = connection.DatabaseType switch
                    {
                        DatabaseType.MySQL => @"
                            SELECT 
                                COLUMN_NAME,
                                DATA_TYPE,
                                IS_NULLABLE,
                                COLUMN_DEFAULT,
                                COLUMN_KEY,
                                COLUMN_COMMENT
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_SCHEMA = @database AND TABLE_NAME = @tableName 
                            ORDER BY ORDINAL_POSITION",
                        DatabaseType.SQLServer => @"
                            SELECT 
                                COLUMN_NAME,
                                DATA_TYPE,
                                IS_NULLABLE,
                                COLUMN_DEFAULT,
                                CASE WHEN pk.COLUMN_NAME IS NOT NULL THEN 'PRI' ELSE '' END as COLUMN_KEY,
                                '' as COLUMN_COMMENT
                            FROM INFORMATION_SCHEMA.COLUMNS c
                            LEFT JOIN (
                                SELECT ku.TABLE_CATALOG, ku.TABLE_SCHEMA, ku.TABLE_NAME, ku.COLUMN_NAME
                                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
                                INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ku
                                    ON tc.CONSTRAINT_TYPE = 'PRIMARY KEY' 
                                    AND tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME
                            ) pk ON c.TABLE_CATALOG = pk.TABLE_CATALOG 
                                AND c.TABLE_SCHEMA = pk.TABLE_SCHEMA 
                                AND c.TABLE_NAME = pk.TABLE_NAME 
                                AND c.COLUMN_NAME = pk.COLUMN_NAME
                            WHERE c.TABLE_CATALOG = @database AND c.TABLE_NAME = @tableName 
                            ORDER BY c.ORDINAL_POSITION",
                        DatabaseType.SQLite => @"
                            SELECT 
                                name as COLUMN_NAME,
                                type as DATA_TYPE,
                                CASE WHEN [notnull] = 0 THEN 'YES' ELSE 'NO' END as IS_NULLABLE,
                                dflt_value as COLUMN_DEFAULT,
                                CASE WHEN pk = 1 THEN 'PRI' ELSE '' END as COLUMN_KEY,
                                '' as COLUMN_COMMENT
                            FROM pragma_table_info(@tableName)
                            ORDER BY cid",
                        _ => throw new NotSupportedException($"Database type {connection.DatabaseType} is not supported")
                    };
                    
                    using var command = dbConnection.CreateCommand();
                    command.CommandText = query;
                    
                    if (connection.DatabaseType != DatabaseType.SQLite)
                    {
                        var dbParameter = command.CreateParameter();
                        dbParameter.ParameterName = "@database";
                        dbParameter.Value = connection.Database;
                        command.Parameters.Add(dbParameter);
                    }
                    
                    var tableParameter = command.CreateParameter();
                    tableParameter.ParameterName = "@tableName";
                    tableParameter.Value = tableName;
                    command.Parameters.Add(tableParameter);
                    
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        columns.Add(new TableColumn
                        {
                            Name = reader["COLUMN_NAME"]?.ToString() ?? string.Empty,
                            DataType = reader["DATA_TYPE"]?.ToString() ?? string.Empty,
                            IsNullable = reader["IS_NULLABLE"]?.ToString() == "YES",
                            DefaultValue = reader["COLUMN_DEFAULT"] == DBNull.Value ? null : reader["COLUMN_DEFAULT"]?.ToString(),
                            IsPrimaryKey = reader["COLUMN_KEY"]?.ToString() == "PRI",
                            Comment = reader["COLUMN_COMMENT"] == DBNull.Value ? null : reader["COLUMN_COMMENT"]?.ToString()
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get table columns: {ex.Message}", ex);
                }
                
                return columns;
            });
        }

        private IDbConnection CreateConnection(DatabaseConnection connection)
        {
            return connection.DatabaseType switch
            {
                DatabaseType.MySQL => new MySqlConnection(connection.ConnectionString),
                DatabaseType.SQLServer => new SqlConnection(connection.ConnectionString),
                DatabaseType.SQLite => new SqliteConnection(connection.ConnectionString),
                _ => throw new NotSupportedException($"Database type {connection.DatabaseType} is not supported")
            };
        }
    }
}