using AiUoVsix.Command.EntityFrameworkCore.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AiUoVsix.Command.EntityFrameworkCore.Services;

namespace AiUoVsix.Command.EntityFrameworkCore.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<DatabaseConnectionViewModel> _connections = new();
        
        [ObservableProperty]
        private DatabaseConnectionViewModel? _selectedConnection;
        
        [ObservableProperty]
        private DatabaseConnectionViewModel _currentConnection = new();
        
        [ObservableProperty]
        private bool _isEditingConnection = false;
        
        [ObservableProperty]
        private bool _isNewConnection = false;
        
        [ObservableProperty]
        private DatabaseBrowserViewModel _databaseBrowser = new();
        
        private AppConfiguration _configuration = null!;
        
        public MainWindowViewModel()
        {
            LoadConnections();
        }
        
        private void LoadConnections()
        {
            _configuration = ConfigurationManager.LoadConfiguration();
            Connections.Clear();
            
            foreach (var connection in _configuration.DatabaseConnections)
            {
                Connections.Add(new DatabaseConnectionViewModel(connection));
            }
        }
        
        [RelayCommand]
        private void AddConnection()
        {
            CurrentConnection = new DatabaseConnectionViewModel();
            IsNewConnection = true;
            IsEditingConnection = true;
        }
        
        [RelayCommand]
        private void EditConnection()
        {
            if (SelectedConnection != null)
            {
                CurrentConnection = new DatabaseConnectionViewModel(SelectedConnection.ToModel());
                IsNewConnection = false;
                IsEditingConnection = true;
            }
        }
        
        private async Task ConnectToDatabaseAsync()
        {
            if (SelectedConnection != null)
            {
                var connection = SelectedConnection.ToModel();
                await DatabaseBrowser.SetConnectionAsync(connection);
            }
        }
        
        [RelayCommand]
        private void DeleteConnection()
        {
            if (SelectedConnection != null)
            {
                Connections.Remove(SelectedConnection);
                SaveConnections();
                SelectedConnection = null;
            }
        }
        
        [RelayCommand]
        private void SaveConnection()
        {
            if (CurrentConnection != null)
            {
                var existingConnection = Connections.FirstOrDefault(c => c.Id == CurrentConnection.Id);
                if (existingConnection != null)
                {
                    // 更新现有连接
                    existingConnection.LoadFromModel(CurrentConnection.ToModel());
                }
                else
                {
                    // 添加新连接
                    var newConnection = new DatabaseConnectionViewModel(CurrentConnection.ToModel());
                    Connections.Add(newConnection);
                    SelectedConnection = newConnection; // 自动选择新添加的连接
                }
                
                SaveConnections();
                IsEditingConnection = false;
                IsNewConnection = false;
            }
        }
        
        [RelayCommand]
        private void CancelEdit()
        {
            IsEditingConnection = false;
            IsNewConnection = false;
            CurrentConnection = new DatabaseConnectionViewModel();
        }
        
        [RelayCommand]
        private void CancelEditConnection()
        {
            IsEditingConnection = false;
            IsNewConnection = false;
            CurrentConnection = new DatabaseConnectionViewModel();
        }
        
        private void SaveConnections()
        {
            _configuration.DatabaseConnections.Clear();
            foreach (var connectionVm in Connections)
            {
                _configuration.DatabaseConnections.Add(connectionVm.ToModel());
            }
            
            ConfigurationManager.SaveConfiguration(_configuration);
        }
        
        partial void OnSelectedConnectionChanged(DatabaseConnectionViewModel? value)
        {
            if (value != null)
            {
                // 自动连接到选中的数据库
                _ = Task.Run(async () => await ConnectToDatabaseAsync());
            }
            else
            {
                DatabaseBrowser.ClearData();
            }
        }
    }
}
