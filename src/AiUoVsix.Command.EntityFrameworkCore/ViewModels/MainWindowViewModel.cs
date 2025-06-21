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
        private bool _isEditing = false;
        
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
            IsEditing = true;
        }
        
        [RelayCommand]
        private void EditConnection()
        {
            if (SelectedConnection != null)
            {
                CurrentConnection = new DatabaseConnectionViewModel(SelectedConnection.ToModel());
                IsEditing = true;
            }
        }
        
        private async Task ConnectToDatabaseAsync()
        {
            if (SelectedConnection != null)
            {
                var connection = SelectedConnection.ToModel();
                await DatabaseBrowser.SetConnectionAsync(connection);
                IsEditing = false; // 确保显示数据库浏览器而不是编辑面板
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
                    Connections.Add(new DatabaseConnectionViewModel(CurrentConnection.ToModel()));
                }
                
                SaveConnections();
                IsEditing = false;
            }
        }
        
        [RelayCommand]
        private void CancelEdit()
        {
            IsEditing = false;
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
