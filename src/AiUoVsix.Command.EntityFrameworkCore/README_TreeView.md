# 数据库对象树形视图功能实现

## 功能概述

本次更新将原有的数据库对象浏览器从简单的展开面板（Expander）结构改造为带有复选框的树形结构（TreeView），提供更好的用户体验和交互功能。

## 主要改进

### 1. 新增TreeNodeViewModel类
- 位置：`ViewModels/TreeNodeViewModel.cs`
- 功能：
  - 支持树形节点的层次结构
  - 提供复选框状态管理
  - 支持父子节点的联动选择
  - 区分分类节点和数据库对象节点

### 2. 更新DatabaseBrowserViewModel
- 新增属性：
  - `DatabaseObjectTree`：树形结构的根节点集合
  - `SelectedTreeNode`：当前选中的树节点
- 新增方法：
  - `BuildDatabaseObjectTree()`：构建树形结构
  - `OnSelectedTreeNodeChanged()`：处理节点选择事件

### 3. 更新MainWindow.axaml界面
- 将原有的多个Expander控件替换为单个TreeView控件
- 每个节点包含：
  - 复选框：支持多选功能
  - 图标：区分不同类型的数据库对象
  - 名称：显示对象名称

## 功能特性

### 树形结构
- **表**：📋 图标，默认展开
- **视图**：👁️ 图标
- **函数**：⚙️ 图标
- **存储过程**：📦 图标

### 复选框功能
- 支持单个对象的选择/取消选择
- 父节点选中时，所有子节点自动选中
- 子节点全部选中时，父节点自动选中
- 子节点部分选中时，父节点保持未选中状态

### 详情显示
- 选中单个表或视图时，右侧显示字段详情
- 保持与原有功能的兼容性

## 技术实现

### 数据绑定
```xml
<TreeView ItemsSource="{Binding DatabaseBrowser.DatabaseObjectTree}"
          SelectedItem="{Binding DatabaseBrowser.SelectedTreeNode}">
    <TreeView.ItemTemplate>
        <TreeDataTemplate ItemsSource="{Binding Children}">
            <StackPanel Orientation="Horizontal">
                <CheckBox IsChecked="{Binding IsChecked}"/>
                <TextBlock Text="{Binding Icon}"/>
                <TextBlock Text="{Binding Name}"/>
            </StackPanel>
        </TreeDataTemplate>
    </TreeView.ItemTemplate>
</TreeView>
```

### 复选框联动逻辑
```csharp
partial void OnIsCheckedChanged(bool value)
{
    // 更新子节点
    foreach (var child in Children)
    {
        child.IsChecked = value;
    }
    
    // 更新父节点
    UpdateParentCheckState();
}
```

## 使用说明

1. **连接数据库**：在左侧选择数据库连接
2. **浏览对象**：中间面板显示树形结构的数据库对象
3. **选择对象**：使用复选框选择需要的对象
4. **查看详情**：点击单个表或视图查看字段详情
5. **批量操作**：通过复选框实现批量选择功能

## 后续扩展

- 可以基于选中的对象生成Entity Framework代码
- 支持导出选中对象的结构
- 添加搜索和过滤功能
- 支持拖拽操作