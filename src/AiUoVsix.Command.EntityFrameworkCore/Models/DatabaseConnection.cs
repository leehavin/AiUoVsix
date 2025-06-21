using System;
using System.ComponentModel.DataAnnotations;

namespace AiUoVsix.Command.EntityFrameworkCore.Models
{
    public class DatabaseConnection
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required(ErrorMessage = "连接名称不能为空")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "请选择数据库类型")]
        public DatabaseType DatabaseType { get; set; }
        
        [Required(ErrorMessage = "服务器地址不能为空")]
        public string Server { get; set; } = string.Empty;
        
        public int Port { get; set; }
        
        public string Database { get; set; } = string.Empty;
        
        public string Username { get; set; } = string.Empty;
        
        public string Password { get; set; } = string.Empty;
        
        public bool IntegratedSecurity { get; set; }
        
        public string ConnectionString
        {
            get
            {
                return DatabaseType switch
                {
                    DatabaseType.MySQL => $"Server={Server};Port={Port};Database={Database};Uid={Username};Pwd={Password};",
                    DatabaseType.SQLite => $"Data Source={Database};",
                    DatabaseType.SQLServer => IntegratedSecurity 
                        ? $"Server={Server};Database={Database};Integrated Security=true;"
                        : $"Server={Server};Database={Database};User Id={Username};Password={Password};",
                    _ => string.Empty
                };
            }
        }
        
        public int GetDefaultPort()
        {
            return DatabaseType switch
            {
                DatabaseType.MySQL => 3306,
                DatabaseType.SQLServer => 1433,
                DatabaseType.SQLite => 0,
                _ => 0
            };
        }
    }
}