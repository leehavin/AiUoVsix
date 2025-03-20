# AiUoVsix - AiUo框架开发助手

<div align="center">

[![Visual Studio Marketplace Version](https://img.shields.io/badge/VS%20Marketplace-v1.0.0.1-blue.svg)](https://marketplace.visualstudio.com/items?itemName=leehavin.AiUoVsix)
[![Visual Studio 2022](https://img.shields.io/badge/VS-2022-purple.svg)](https://visualstudio.microsoft.com/vs/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

<img src="src/AiUoVsix/aiuo-icon-256.ico" alt="AiUoVsix Logo" width="128" height="128">

</div>

## 📝 简介

AiUoVsix 是一款专为 AiUo 框架开发者设计的 Visual Studio 2022 扩展插件，提供了一系列实用工具，帮助开发者提高开发效率。该插件集成了数据库实体生成、NuGet 包发布和 Docker 部署等功能，为开发流程提供了全方位的支持。

## ✨ 主要功能

### 🔄 SQLSugar 数据库实体生成

- 支持多种数据库类型的连接配置
- 自动生成与数据库表结构匹配的实体类
- 支持 Partial 类模式，便于代码分离和维护
- 提供自定义模板，灵活适应不同项目需求

### 📦 NuGet 包发布工具

- 简化 NuGet 包的创建和发布流程
- 支持多个 NuGet 源的配置和管理
- 提供版本控制和包信息管理功能
- 自动处理项目依赖关系

### 🐳 Docker 部署助手

- 简化 Docker 镜像的构建和发布过程
- 提供 Docker 配置管理功能
- 支持多环境部署配置

## 🔧 安装要求

- Visual Studio 2022 (版本 17.0 或更高)
- .NET Framework 4.5 或更高版本
- Visual Studio Core Editor 组件

## 📥 安装方法

### 通过 Visual Studio Marketplace 安装

1. 在 Visual Studio 中，选择 **扩展** > **管理扩展**
2. 搜索 "AiUoVsix"
3. 点击 **下载** 按钮
4. 重启 Visual Studio 完成安装

### 手动安装

1. 下载最新的 VSIX 安装包
2. 关闭所有 Visual Studio 实例
3. 双击 VSIX 文件运行安装程序
4. 重启 Visual Studio

## 🚀 使用指南

### SQLSugar 数据库实体生成

1. 在 Visual Studio 中打开你的解决方案
2. 点击菜单 **工具** > **AiUo SQLSugar 实体生成**
3. 配置数据库连接信息
4. 选择要生成实体的表
5. 设置生成选项（命名空间、输出路径等）
6. 点击生成按钮完成实体类创建

### NuGet 包发布

1. 在 Visual Studio 中打开你的解决方案
2. 点击菜单 **工具** > **AiUo NuGet 发布**
3. 配置 NuGet 源信息
4. 设置包版本和发布选项
5. 点击发布按钮完成包发布

### Docker 部署

1. 在 Visual Studio 中打开你的解决方案
2. 点击菜单 **工具** > **AiUo Docker 发布**
3. 配置 Docker 相关设置
4. 执行构建和发布操作

## 🔄 更新日志

### v1.0.0.1
- 初始版本发布
- 实现 SQLSugar 数据库实体生成功能
- 实现 NuGet 包发布功能
- 实现 Docker 部署功能

## 🤝 贡献指南

欢迎为 AiUoVsix 项目做出贡献！如果你有任何建议或发现了问题，请提交 Issue 或 Pull Request。

## 📄 许可证

本项目采用 MIT 许可证，详情请参阅 [LICENSE](LICENSE) 文件。

## 👨‍💻 关于作者

- **开发者**: leehavin
- **项目主页**: [GitHub 仓库地址]

---

<div align="center">

**AiUoVsix** - 让 AiUo 框架开发更高效！

</div>