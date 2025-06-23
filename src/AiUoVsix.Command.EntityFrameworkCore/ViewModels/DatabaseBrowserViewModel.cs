using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AiUoVsix.Command.EntityFrameworkCore.Models;
using AiUoVsix.Command.EntityFrameworkCore.Services;

namespace AiUoVsix.Command.EntityFrameworkCore.ViewModels
{
    public partial class DatabaseBrowserViewModel : ViewModelBase
    {
        private readonly DatabaseService _databaseService;
        
        [ObservableProperty]
        private DatabaseConnection? _currentConnection;
        
        [ObservableProperty]
        private ObservableCollection<DatabaseObject> _tables = new();
        
        [ObservableProperty]
        private ObservableCollection<DatabaseObject> _views = new();
        
        [ObservableProperty]
        private ObservableCollection<DatabaseObject> _functions = new();
        
        [ObservableProperty]
        private ObservableCollection<DatabaseObject> _storedProcedures = new();
        
        [ObservableProperty]
        private DatabaseObject? _selectedObject;
        
        [ObservableProperty]
        private ObservableCollection<TableColumn> _columns = new();
        
        [ObservableProperty]
        private bool _isLoading;
        
        [ObservableProperty]
        private string _statusMessage = "请选择数据库连接";
        
        [ObservableProperty]
        private bool _isConnected;
        
        [ObservableProperty]
        private string _selectedTableName = string.Empty;
        
        [ObservableProperty]
        private ObservableCollection<TableColumn> _tableColumns = new();
        
        [ObservableProperty]
        private string _connectionStatus = "未连接";
        
        [ObservableProperty]
        private DatabaseObject? _selectedTable;
        
        [ObservableProperty]
        private ObservableCollection<TreeNodeViewModel> _databaseObjectTree = new();
        
        [ObservableProperty]
        private TreeNodeViewModel? _selectedTreeNode;
        
        public DatabaseBrowserViewModel()
        {
            _databaseService = new DatabaseService();
        }
        
        public async Task SetConnectionAsync(DatabaseConnection connection)
        {
            CurrentConnection = connection;
            await RefreshDatabaseObjectsAsync();
        }
        
        [RelayCommand]
        private async Task TestConnectionAsync()
        {
            if (CurrentConnection == null) return;
            
            IsLoading = true;
            StatusMessage = "正在测试连接...";
            
            try
            {
                var result = await _databaseService.TestConnectionAsync(CurrentConnection);
                if (result)
                {
                    StatusMessage = "连接成功";
                    ConnectionStatus = "已连接";
                    IsConnected = true;
                    await RefreshDatabaseObjectsAsync();
                }
                else
                {
                    StatusMessage = "连接失败";
                    ConnectionStatus = "连接失败";
                    IsConnected = false;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"连接失败: {ex.Message}";
                ConnectionStatus = "连接失败";
                IsConnected = false;
            }
            finally
            {
                IsLoading = false;
            }
        }
        
        [RelayCommand]
        private async Task RefreshDatabaseObjectsAsync()
        {
            if (CurrentConnection == null) return;
            
            IsLoading = true;
            StatusMessage = "正在加载数据库对象...";
            
            try
            {
                // 并行加载所有数据库对象
                var tablesTask = _databaseService.GetTablesAsync(CurrentConnection);
                var viewsTask = _databaseService.GetViewsAsync(CurrentConnection);
                var functionsTask = _databaseService.GetFunctionsAsync(CurrentConnection);
                var proceduresTask = _databaseService.GetStoredProceduresAsync(CurrentConnection);
                
                await Task.WhenAll(tablesTask, viewsTask, functionsTask, proceduresTask);
                
                Tables.Clear();
                Views.Clear();
                Functions.Clear();
                StoredProcedures.Clear();
                
                foreach (var table in await tablesTask)
                {
                    Tables.Add(table);
                }
                
                foreach (var view in await viewsTask)
                {
                    Views.Add(view);
                }
                
                foreach (var function in await functionsTask)
                {
                    Functions.Add(function);
                }
                
                foreach (var procedure in await proceduresTask)
                {
                    StoredProcedures.Add(procedure);
                }
                
                StatusMessage = $"已加载 {Tables.Count} 个表, {Views.Count} 个视图, {Functions.Count} 个函数, {StoredProcedures.Count} 个存储过程";
                IsConnected = true;
                
                // 构建树形结构
                BuildDatabaseObjectTree();
            }
            catch (Exception ex)
            {
                StatusMessage = $"加载失败: {ex.Message}";
                IsConnected = false;
            }
            finally
            {
                IsLoading = false;
            }
        }
        
        [RelayCommand]
        private async Task LoadTableColumnsAsync(DatabaseObject? databaseObject)
        {
            if (databaseObject == null || CurrentConnection == null) return;
            
            SelectedObject = databaseObject;
            SelectedTable = databaseObject;
            SelectedTableName = databaseObject.Name;
            
            if (databaseObject.Type != DatabaseObjectType.Table && databaseObject.Type != DatabaseObjectType.View)
            {
                Columns.Clear();
                TableColumns.Clear();
                return;
            }
            
            IsLoading = true;
            StatusMessage = $"正在加载 {databaseObject.Name} 的列信息...";
            
            try
            {
                var columns = await _databaseService.GetTableColumnsAsync(CurrentConnection, databaseObject.Name);
                
                await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Columns.Clear();
                    TableColumns.Clear();
                    foreach (var column in columns)
                    {
                        Columns.Add(column);
                        TableColumns.Add(column);
                    }
                    
                    StatusMessage = $"已加载 {databaseObject.Name} 的 {columns.Count} 个列";
                });
            }
            catch (Exception ex)
            {
                await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                {
                    StatusMessage = $"加载列信息失败: {ex.Message}";
                    Columns.Clear();
                    TableColumns.Clear();
                });
            }
            finally
            {
                await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                {
                    IsLoading = false;
                });
            }
        }
        
        private void BuildDatabaseObjectTree()
        {
            DatabaseObjectTree.Clear();
            
            // 创建表节点
            if (Tables.Any())
            {
                var tablesNode = new TreeNodeViewModel("表", "🗂️", TreeNodeType.Category)
                {
                    IsExpanded = true
                };
                
                foreach (var table in Tables)
                {
                    var tableNode = new TreeNodeViewModel(table, "🗂️");
                    tablesNode.AddChild(tableNode);
                }
                
                DatabaseObjectTree.Add(tablesNode);
            }
            
            // 创建视图节点
            if (Views.Any())
            {
                var viewsNode = new TreeNodeViewModel("视图", "👀", TreeNodeType.Category);
                
                foreach (var view in Views)
                {
                    var viewNode = new TreeNodeViewModel(view, "👀");
                    viewsNode.AddChild(viewNode);
                }
                
                DatabaseObjectTree.Add(viewsNode);
            }
            
            // 创建函数节点
            if (Functions.Any())
            {
                var functionsNode = new TreeNodeViewModel("函数", "🔧", TreeNodeType.Category);
                
                foreach (var function in Functions)
                {
                    var functionNode = new TreeNodeViewModel(function, "🔧");
                    functionsNode.AddChild(functionNode);
                }
                
                DatabaseObjectTree.Add(functionsNode);
            }
            
            // 创建存储过程节点
            if (StoredProcedures.Any())
            {
                var proceduresNode = new TreeNodeViewModel("存储过程", "⚡", TreeNodeType.Category);
                
                foreach (var procedure in StoredProcedures)
                {
                    var procedureNode = new TreeNodeViewModel(procedure, "⚡");
                    proceduresNode.AddChild(procedureNode);
                }
                
                DatabaseObjectTree.Add(proceduresNode);
            }
        }
        
        partial void OnSelectedTreeNodeChanged(TreeNodeViewModel? value)
        {
            if (value?.DatabaseObject != null)
            {
                // 选中了数据库对象，加载其详细信息
                _ = LoadTableColumnsAsync(value.DatabaseObject);
            }
        }
        
        [RelayCommand]
        private void SelectAll()
        {
            foreach (var node in DatabaseObjectTree)
            {
                node.IsChecked = true;
                // 确保所有子节点也被选中
                foreach (var child in node.Children)
                {
                    child.IsChecked = true;
                }
            }
            StatusMessage = "已全选所有表";
        }
        
        [RelayCommand]
        private void UnselectAll()
        {
            foreach (var node in DatabaseObjectTree)
            {
                node.IsChecked = false;
                // 确保所有子节点也被取消选中
                foreach (var child in node.Children)
                {
                    child.IsChecked = false;
                }
            }
            StatusMessage = "已取消全选";
        }
        
        [RelayCommand]
        private void GenerateEntity()
        {
            // 获取所有选中的表
            var selectedTables = GetAllSelectedTables();
            if (selectedTables.Count == 0)
            {
                StatusMessage = "请至少选择一个表或视图";
                return;
            }
            
            // 显示选中的表和视图名称
            var tableNames = string.Join(", ", selectedTables.Select(t => t.Name));
            
            // TODO: 实现实体生成逻辑
            StatusMessage = $"准备生成 {selectedTables.Count} 个实体类: {tableNames}";
            
            // 这里应该添加实体生成的实际逻辑
            // 例如：生成C#类文件，添加属性，生成DbContext等
        }
        
        private System.Collections.Generic.List<DatabaseObject> GetAllSelectedTables()
        {
            var result = new System.Collections.Generic.List<DatabaseObject>();
            
            // 遍历所有表节点
            var tablesNode = DatabaseObjectTree.FirstOrDefault(n => n.Name.Contains("表"));
            if (tablesNode != null)
            {
                foreach (var tableNode in tablesNode.Children)
                {
                    if (tableNode.IsChecked && tableNode.DatabaseObject != null)
                    {
                        result.Add(tableNode.DatabaseObject);
                    }
                }
            }
            
            // 遍历所有视图节点
            var viewsNode = DatabaseObjectTree.FirstOrDefault(n => n.Name.Contains("视图"));
            if (viewsNode != null)
            {
                foreach (var viewNode in viewsNode.Children)
                {
                    if (viewNode.IsChecked && viewNode.DatabaseObject != null)
                    {
                        result.Add(viewNode.DatabaseObject);
                    }
                }
            }
            
            return result;
        }
        
        public void ClearData()
        {
            CurrentConnection = null;
            Tables.Clear();
            Views.Clear();
            Functions.Clear();
            StoredProcedures.Clear();
            Columns.Clear();
            TableColumns.Clear();
            DatabaseObjectTree.Clear();
            SelectedObject = null;
            SelectedTable = null;
            SelectedTreeNode = null;
            SelectedTableName = string.Empty;
            ConnectionStatus = "未连接";
            IsConnected = false;
            StatusMessage = "请选择数据库连接";
        }
    }
}