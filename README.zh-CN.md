# UnoPlatform 示例项目集合

[![English](https://img.shields.io/badge/docs-English-blue.svg)](README.md) [![中文](https://img.shields.io/badge/docs-中文-red.svg)](README.zh-CN.md) [![한국어](https://img.shields.io/badge/docs-한국어-green.svg)](README.ko.md)

本仓库为 **WPF 开发者提供 UnoPlatform 桌面跨平台应用程序开发示例项目**。包含多个 UnoPlatform 示例项目，帮助您学习从 WPF 到 UnoPlatform 的转换及架构策略。

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download)
[![Stars](https://img.shields.io/github/stars/jamesnet214/samples-uno.svg)](https://github.com/jamesnet214/samples-uno/stargazers)
[![Forks](https://img.shields.io/github/forks/jamesnet214/samples-uno.svg)](https://github.com/jamesnet214/samples-uno/network/members)
[![Issues](https://img.shields.io/github/issues/jamesnet214/samples-uno.svg)](https://github.com/jamesnet214/samples-uno/issues)

## 项目介绍

本仓库为 WPF 开发者在**使用 UnoPlatform 开发桌面跨平台应用程序**时提供有用的架构策略和示例代码。包含多个 UnoPlatform 示例项目，帮助您学习从 WPF 转向 UnoPlatform 过程中所需的核心技术和模式。

我们自 **2008 年以来一直在研究和积累 WPF 技术**经验，在此期间，**Xamarin、MAUI、UnoPlatform、AvaloniaUI、OpenSilver** 等多种基于 XAML 的跨平台技术不断发展。这为将 WPF 积累的技术扩展到跨平台开辟了可能性。

本仓库重点介绍**依赖注入（DI）**、**项目分布式和模块化**、**MVVM 模式应用**等核心架构技术。同时详细说明如何使用基于 **.NET Standard 2.0 的 Jamesnet.Core 框架**在 WPF、UnoPlatform、WinUI 3 等各种平台上统一运行，实现与平台无关的相同项目设计。

## 示例项目列表

### 1. 英雄联盟客户端 (Uno-Platform)

这是一个使用 UnoPlatform **高质量重现英雄联盟客户端的项目**。该项目包含 UnoPlatform 各种技术实现的案例，展示了大型项目分布式设计的广泛技术方法。

[英雄联盟客户端 (Uno-Platform) GitHub 仓库](https://github.com/jamesnetgroup/leagueoflegends-uno)

### 2. UnoPlatform 自行车租赁管理应用程序

这是面向 WPF 开发者的 UnoPlatform 桌面项目架构策略，**于 2024 年 9 月 26 日在 .NET Meetup 研讨会上发表**。

本示例项目详细介绍了**使用 UnoPlatform 开发自行车租赁管理应用程序**的逐步过程。旨在帮助 WPF 开发者深入理解 UnoPlatform 架构和开发方法，并在将现有 WPF 项目转换为跨平台时提供实际帮助。

[详细技术文章](https://jamesnet.dev/article/174)

## 核心技术栈

> 仓库中包含所有框架源代码。

- [x] **Jamesnet.Core**：基于 .NET Standard 2.0 的跨平台核心库
- [x] **Jamesnet.Uno**：为 UnoPlatform 优化的 UI 框架

这两个库可以在 WPF 和 UnoPlatform 中同样使用，包含大型项目架构设计所需的所有核心功能。

#### 主要特点

- **依赖注入（DI）**：实现降低对象之间耦合度，提高可测试性和可扩展性的架构
- **项目分布式和模块化**：降低代码复杂度，提高团队协作，增加代码重用性
- **MVVM 模式应用**：通过分离视图和业务逻辑提高代码内聚性和可维护性
- **平台独立设计**：使用基于 .NET Standard 2.0 的 Jamesnet.Core 框架在各种平台上统一运行

#### 主要功能和实现

1. **大型项目架构**
   - [x] 模块化和分布式系统设计
   - [x] 通过依赖注入实现松耦合
   - [x] 基于插件的可扩展结构

2. **高级 UnoPlatform 技术**
   - [x] 各种自定义控件实现
   - [x] MVVM 模式的实际应用案例
   - [x] 平台独立设计

3. **性能优化**
   - [x] 高效的资源管理和内存使用
   - [x] 异步编程模式应用
   - [x] 渲染优化技术

4. **UI/UX 设计**
   - [x] 精细 UI 重现技术学习
   - [x] 自定义动画和过渡效果
   - [x] 动态主题系统

5. **框架设计**
   - [x] 基于事件的通信系统
   - [x] 状态管理模式实现
   - [x] 可扩展的导航系统

## 技术栈

- .NET 8.0
- UnoPlatform
- Jamesnet.Core (.NET Standard 2.0)
- Jamesnet.Uno

## 开始使用

### 前提条件

- Visual Studio 2022 或更高版本
- .NET 8.0 SDK
- 安装 UnoPlatform 项目模板

### 安装和运行

#### 1. 克隆仓库：

```bash
git clone https://github.com/jamesnet214/samples-uno.git
```

#### 2. 打开解决方案

- [x] Visual Studio
- [x] Visual Studio Code
- [x] JetBrains Rider

<div style="text-align: center;">
    <img src="https://github.com/user-attachments/assets/af70f422-7057-4e77-a54d-042ee8358d2a" width="32%"/>
    <img src="https://github.com/user-attachments/assets/e4feaa10-a107-4b58-8d13-1d8be620ec62" width="32%"/>
    <img src="https://github.com/user-attachments/assets/5ff487f6-55e4-43e1-9abf-f8d419ee6943" width="32%"/>
</div>

#### 3. 构建和运行项目
