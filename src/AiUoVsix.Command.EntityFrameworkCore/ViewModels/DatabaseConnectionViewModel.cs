using AiUoVsix.Command.EntityFrameworkCore.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AiUoVsix.Command.EntityFrameworkCore.Services;

namespace AiUoVsix.Command.EntityFrameworkCore.ViewModels
{
    public partial class DatabaseConnectionViewModel : ObservableValidator
    {
        [ObservableProperty]
        private string _id = Guid.NewGuid().ToString();
        
        [ObservableProperty]
        [Required(ErrorMessage = "连接名称不能为空")]
        private string _name = string.Empty;
        
        [ObservableProperty]
        private DatabaseType _databaseType = DatabaseType.MySQL;
        
        [ObservableProperty]
        [Required(ErrorMessage = "服务器地址不能为空")]
        private string _server = string.Empty;
        
        [ObservableProperty]
        private int _port;
        
        [ObservableProperty]
        private string _database = string.Empty;
        
        [ObservableProperty]
        private string _username = string.Empty;
        
        [ObservableProperty]
        private string _password = string.Empty;
        
        [ObservableProperty]
        private bool _integratedSecurity;
        
        [ObservableProperty]
        private string _connectionString = string.Empty;
        
        public ObservableCollection<DatabaseType> DatabaseTypes { get; } = new()
        {
            DatabaseType.MySQL,
            DatabaseType.SQLite,
            DatabaseType.SQLServer
        };
        
        public DatabaseConnectionViewModel()
        {
            UpdatePort();
            UpdateConnectionString();
        }
        
        public DatabaseConnectionViewModel(DatabaseConnection connection) : this()
        {
            LoadFromModel(connection);
        }
        
        partial void OnDatabaseTypeChanged(DatabaseType value)
        {
            UpdatePort();
            UpdateConnectionString();
        }
        
        partial void OnServerChanged(string value)
        {
            UpdateConnectionString();
        }
        
        partial void OnPortChanged(int value)
        {
            UpdateConnectionString();
        }
        
        partial void OnDatabaseChanged(string value)
        {
            UpdateConnectionString();
        }
        
        partial void OnUsernameChanged(string value)
        {
            UpdateConnectionString();
        }
        
        partial void OnPasswordChanged(string value)
        {
            UpdateConnectionString();
        }
        
        partial void OnIntegratedSecurityChanged(bool value)
        {
            UpdateConnectionString();
        }
        
        private void UpdatePort()
        {
            if (Port == 0)
            {
                Port = DatabaseType switch
                {
                    DatabaseType.MySQL => 3306,
                    DatabaseType.SQLServer => 1433,
                    DatabaseType.SQLite => 0,
                    _ => 0
                };
            }
        }
        
        private void UpdateConnectionString()
        {
            ConnectionString = DatabaseType switch
            {
                DatabaseType.MySQL => $"Server={Server};Port={Port};Database={Database};Uid={Username};Pwd={Password};",
                DatabaseType.SQLite => $"Data Source={Database};",
                DatabaseType.SQLServer => IntegratedSecurity 
                    ? $"Server={Server};Database={Database};Integrated Security=true;"
                    : $"Server={Server};Database={Database};User Id={Username};Password={Password};",
                _ => string.Empty
            };
        }
        
        public DatabaseConnection ToModel()
        {
            return new DatabaseConnection
            {
                Id = Id,
                Name = Name,
                DatabaseType = DatabaseType,
                Server = Server,
                Port = Port,
                Database = Database,
                Username = Username,
                Password = Password,
                IntegratedSecurity = IntegratedSecurity
            };
        }
        
        public void LoadFromModel(DatabaseConnection connection)
        {
            Id = connection.Id;
            Name = connection.Name;
            DatabaseType = connection.DatabaseType;
            Server = connection.Server;
            Port = connection.Port;
            Database = connection.Database;
            Username = connection.Username;
            Password = connection.Password;
            IntegratedSecurity = connection.IntegratedSecurity;
        }
        
        [ObservableProperty]
        private bool _isTestingConnection;
        
        [ObservableProperty]
        private string _testResult = string.Empty;
        
        private readonly DatabaseService _databaseService = new();
        
        [RelayCommand]
        private async Task TestConnectionAsync()
        {
            IsTestingConnection = true;
            TestResult = "正在测试连接...";
            
            try
            {
                var connection = ToModel();
                var result = await _databaseService.TestConnectionAsync(connection);
                
                if (result)
                {
                    TestResult = "✓ 连接成功";
                }
                else
                {
                    TestResult = "✗ 连接失败";
                }
            }
            catch (Exception ex)
            {
                TestResult = $"✗ 连接失败: {ex.Message}";
            }
            finally
            {
                IsTestingConnection = false;
            }
        }
    }
}