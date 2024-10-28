# UnoPlatform Sample Projects Collection

[![English](https://img.shields.io/badge/docs-English-blue.svg)](README.md) [![中文](https://img.shields.io/badge/docs-中文-red.svg)](README.zh-CN.md) [![한국어](https://img.shields.io/badge/docs-한국어-green.svg)](README.ko.md)

This repository provides **UnoPlatform Desktop Cross-Platform Application Development Sample Projects for WPF Developers**. It includes various UnoPlatform sample projects and helps you learn about transitioning from WPF to UnoPlatform and architectural strategies.

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download)
[![Stars](https://img.shields.io/github/stars/jamesnetgroup/samples-uno.svg)](https://github.com/jamesnetgroup/samples-uno/stargazers)
[![Forks](https://img.shields.io/github/forks/jamesnetgroup/samples-uno.svg)](https://github.com/jamesnetgroup/samples-uno/network/members)
[![Issues](https://img.shields.io/github/issues/jamesnetgroup/samples-uno.svg)](https://github.com/jamesnetgroup/samples-uno/issues)

## Project Introduction

This repository provides architectural strategies and sample code useful for WPF developers when **developing desktop cross-platform applications using UnoPlatform**. It includes various UnoPlatform sample projects and helps you learn core technologies and patterns needed when transitioning from WPF to UnoPlatform.

We have been researching and gaining experience with **WPF technology since 2008**, and during that time, various XAML-based cross-platform technologies such as **Xamarin, MAUI, UnoPlatform, AvaloniaUI, and OpenSilver** have evolved. This has opened up possibilities to extend WPF-accumulated technology to cross-platform.

This repository focuses on core architectural technologies such as **Dependency Injection (DI)**, **project distribution and modularization**, and **MVVM pattern application**. It also details how to implement the same project design regardless of platform using the **.NET Standard 2.0-based Jamesnet.Core framework**, which works uniformly across various platforms including WPF, UnoPlatform, and WinUI 3.

## Sample Project List

### 1. League of Legends Client (Uno-Platform)

This is a **high-quality reproduction project of the League of Legends client using UnoPlatform**. This project includes examples of various technical implementations in UnoPlatform and demonstrates a broad technical approach to distributed design in large-scale projects.

[League of Legends Client (Uno-Platform) GitHub Repository](https://github.com/jamesnetgroup/leagueoflegends-uno)

### 2. UnoPlatform Bicycle Rental Management Application

This is a UnoPlatform desktop project architecture strategy for WPF developers, **presented at the .NET Meetup seminar on September 26, 2024**.

This sample project provides step-by-step details on developing a **bicycle rental management application using UnoPlatform**. It is designed to help WPF developers gain a deep understanding of UnoPlatform architecture and development methods, and provide practical help in converting existing WPF projects to cross-platform.

[Detailed Technical Article](https://jamesnet.dev/article/174)

## Core Technology Stack

> All framework source code is included in the repository.

- [x] **Jamesnet.Core**: Cross-platform core library based on .NET Standard 2.0
- [x] **Jamesnet.Uno**: UI framework optimized for UnoPlatform

These two libraries can be used identically in both WPF and UnoPlatform, and include all core functions needed for large-scale project architecture design.

#### Key Features

- **Dependency Injection (DI)**: Implementing architecture that reduces coupling between objects and increases testability and scalability
- **Project Distribution and Modularization**: Reducing code complexity, improving team collaboration, increasing code reusability
- **MVVM Pattern Application**: Improving code cohesion and maintainability by separating views and business logic
- **Platform-Independent Design**: Operating identically on various platforms using the .NET Standard 2.0-based Jamesnet.Core framework

#### Main Features and Implementations

1. **Large-Scale Project Architecture**
   - [x] Modularization and distributed system design
   - [x] Loose coupling through dependency injection
   - [x] Plugin-based extensible structure

2. **Advanced UnoPlatform Technologies**
   - [x] Various CustomControl implementations
   - [x] Real-world MVVM pattern application cases
   - [x] Platform-independent design

3. **Performance Optimization**
   - [x] Efficient resource management and memory usage
   - [x] Asynchronous programming pattern application
   - [x] Rendering optimization techniques

4. **UI/UX Design**
   - [x] Technology learning for sophisticated UI reproduction
   - [x] Custom animations and transition effects
   - [x] Dynamic theme system

5. **Framework Design**
   - [x] Event-based communication system
   - [x] State management pattern implementation
   - [x] Extensible navigation system

## Technology Stack

- .NET 8.0
- UnoPlatform
- Jamesnet.Core (.NET Standard 2.0)
- Jamesnet.Uno

## Getting Started

### Prerequisites

- Visual Studio 2022 or higher
- .NET 8.0 SDK
- UnoPlatform project templates installed

### Installation and Running

#### 1. Clone the Repository:

```bash
git clone https://github.com/jamesnet214/samples-uno.git
```

#### 2. Open Solution

- [x] Visual Studio
- [x] Visual Studio Code
- [x] JetBrains Rider

<div style="text-align: center;">
    <img src="https://github.com/user-attachments/assets/af70f422-7057-4e77-a54d-042ee8358d2a" width="32%"/>
    <img src="https://github.com/user-attachments/assets/e4feaa10-a107-4b58-8d13-1d8be620ec62" width="32%"/>
    <img src="https://github.com/user-attachments/assets/5ff487f6-55e4-43e1-9abf-f8d419ee6943" width="32%"/>
</div>

#### 3. Build and Run Project

- [x] Set startup project
- [x] Press F5 or click run button

## Learning Opportunities

This repository provides valuable insights for WPF developers:

1. **Complex UI Reproduction**: Learning techniques to reproduce sophisticated user interfaces
2. **Custom Control Development**: Understanding the process of building UnoPlatform custom controls
3. **Practical MVVM**: Examining real implementation cases of MVVM pattern in complex applications
4. **Cross-Platform Architecture Design**: Learning platform-independent project design methods
5. **Performance Optimization**: Learning optimization strategies for large-scale applications

## Contributing

We welcome contributions to the UnoPlatform sample projects! Please submit issues, create pull requests, or suggest improvements.

## License

This project is distributed under the MIT license. See the [LICENSE](LICENSE) file for details.

## Contact

- Website: [https://jamesnet.dev](https://jamesnet.dev)
- Email: james@jamesnet.dev, vickyqu115@hotmail.com

We hope this repository helps WPF developers open new horizons in cross-platform application development using UnoPlatform!
