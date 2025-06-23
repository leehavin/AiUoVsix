using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using AiUoVsix.Command.EntityFrameworkCore.Models;

namespace AiUoVsix.Command.EntityFrameworkCore.ViewModels
{
    public partial class TreeNodeViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _name = string.Empty;
        
        [ObservableProperty]
        private string _icon = string.Empty;
        
        [ObservableProperty]
        private bool _isExpanded;
        
        [ObservableProperty]
        private bool _isChecked;
        
        [ObservableProperty]
        private bool _isSelected;
        
        [ObservableProperty]
        private TreeNodeType _nodeType;
        
        [ObservableProperty]
        private DatabaseObject? _databaseObject;
        
        [ObservableProperty]
        private ObservableCollection<TreeNodeViewModel> _children = new();
        
        [ObservableProperty]
        private TreeNodeViewModel? _parent;
        
        public TreeNodeViewModel(string name, string icon, TreeNodeType nodeType)
        {
            Name = name;
            Icon = icon;
            NodeType = nodeType;
        }
        
        public TreeNodeViewModel(DatabaseObject databaseObject, string icon)
        {
            DatabaseObject = databaseObject;
            Name = databaseObject.Name;
            Icon = icon;
            NodeType = TreeNodeType.DatabaseObject;
        }
        
        partial void OnIsCheckedChanged(bool value)
        {
            // 更新子节点的选中状态
            foreach (var child in Children)
            {
                child.IsChecked = value;
            }
            
            // 更新父节点的选中状态
            UpdateParentCheckState();
        }
        
        private void UpdateParentCheckState()
        {
            if (Parent == null) return;
            
            var checkedCount = Parent.Children.Where(c => c.IsChecked).Count();
            var totalCount = Parent.Children.Count;
            
            if (checkedCount == 0)
            {
                Parent.IsChecked = false;
            }
            else if (checkedCount == totalCount)
            {
                Parent.IsChecked = true;
            }
            else
            {
                // 部分选中状态，在Avalonia中没有三态复选框，所以我们设置为false
                // 在实际UI中，可以考虑使用特殊样式来表示部分选中状态
                Parent.IsChecked = false;
            }
            
            // 递归更新父节点的状态
            Parent.UpdateParentCheckState();
        }
        
        public void AddChild(TreeNodeViewModel child)
        {
            child.Parent = this;
            Children.Add(child);
        }
    }
    
    public enum TreeNodeType
    {
        Category,
        DatabaseObject
    }
}