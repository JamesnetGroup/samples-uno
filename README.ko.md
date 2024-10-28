# UnoPlatform 샘플 프로젝트 모음

[![English](https://img.shields.io/badge/docs-English-blue.svg)](README.md) [![中文](https://img.shields.io/badge/docs-中文-red.svg)](README.zh-CN.md) [![한국어](https://img.shields.io/badge/docs-한국어-green.svg)](README.ko.md)

이 레포지터리는 **WPF 개발자를 위한 UnoPlatform 데스크톱 크로스플랫폼 애플리케이션 개발 샘플 프로젝트**를 제공합니다. 다양한 UnoPlatform 샘플 프로젝트를 포함하고 있으며, WPF에서 UnoPlatform으로의 전환 및 아키텍처 전략을 학습할 수 있습니다.

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download)
[![Stars](https://img.shields.io/github/stars/jamesnet214/samples-uno.svg)](https://github.com/jamesnet214/samples-uno/stargazers)
[![Forks](https://img.shields.io/github/forks/jamesnet214/samples-uno.svg)](https://github.com/jamesnet214/samples-uno/network/members)
[![Issues](https://img.shields.io/github/issues/jamesnet214/samples-uno.svg)](https://github.com/jamesnet214/samples-uno/issues)

## 프로젝트 소개

이 레포지터리는 WPF 개발자들이 **UnoPlatform을 활용하여 데스크톱 크로스플랫폼 애플리케이션을 개발**할 때 유용한 아키텍처 전략과 샘플 코드를 제공합니다. 다양한 UnoPlatform 샘플 프로젝트를 포함하고 있으며, WPF에서 UnoPlatform으로 전환하는 과정에서 필요한 핵심 기술과 패턴을 학습할 수 있습니다.

**2008년부터 WPF 기술**을 연구하고 경험을 쌓아왔으며, 그동안 **Xamarin, MAUI, UnoPlatform, AvaloniaUI, OpenSilver** 등 다양한 XAML 기반의 크로스플랫폼 기술이 발전해 왔습니다. 이를 통해 WPF에서 축적한 기술을 크로스플랫폼으로 확장할 수 있는 가능성이 열렸습니다.

이 레포지터리에서는 **의존성 주입(DI)**, **프로젝트 분산화 및 모듈화**, **MVVM 패턴의 적용** 등 핵심 아키텍처 기술을 중심으로 설명합니다. 또한 **.NET Standard 2.0 기반의 Jamesnet.Core 프레임워크**를 사용하여 WPF, UnoPlatform, WinUI 3 등 다양한 플랫폼에서 동일하게 동작하고, 플랫폼에 상관없이 동일한 프로젝트 설계를 구현하는 방법을 상세히 다룹니다.

## 샘플 프로젝트 목록

### 1. 리그 오브 레전드 클라이언트 (Uno-Platform)

UnoPlatform을 활용한 **리그 오브 레전드 클라이언트의 고품질 재현 프로젝트**입니다. 이 프로젝트는 UnoPlatform의 다양한 기술 구현에 대한 사례들을 포함하고 있으며, 대규모 프로젝트의 분산화 설계에 관한 폭넓은 기술적 접근을 보여줍니다.

[리그 오브 레전드 클라이언트 (Uno-Platform) GitHub 레포지터리](https://github.com/jamesnetgroup/leagueoflegends-uno)


### 2. UnoPlatform 자전거 대여소 관리자 애플리케이션

WPF 개발자를 위한 UnoPlatform 데스크톱 프로젝트 아키텍처 전략으로, **2024년 9월 26일 닷넷 밋업 세미나에서 발표된 내용**입니다.

이 샘플 프로젝트에서는 **UnoPlatform을 활용하여 자전거 대여소 관리 애플리케이션**을 개발하는 과정을 단계별로 상세히 설명합니다. 이를 통해 WPF 개발자들이 UnoPlatform의 아키텍처와 개발 방법을 깊이 있게 이해하고, 기존의 WPF 프로젝트를 크로스플랫폼으로 전환하는 데 실질적인 도움을 얻을 수 있도록 구성하였습니다.

 [상세 기술 아티클](https://jamesnet.dev/article/174)


## 핵심 기술 스택

> 레포지터리 안에 프레임워크 소스코드가 모두 포함되어 있습니다.

- [x] **Jamesnet.Core**: .NET Standard 2.0 기반의 크로스플랫폼 코어 라이브러리
- [x] **Jamesnet.Uno**: UnoPlatform에 최적화된 UI 프레임워크

이 두 라이브러리는 WPF와 UnoPlatform 모두에서 동일하게 사용 가능하며, 대규모 프로젝트의 아키텍처 설계에 필요한 모든 핵심 기능을 포함하고 있습니다.

#### 주요 특징

- **의존성 주입(DI)**: 객체 간의 결합도를 낮추고, 테스트 가능성과 확장성을 높이는 아키텍처 구현
- **프로젝트 분산화 및 모듈화**: 코드 복잡성 감소, 팀 협업 향상, 코드 재사용성 증가
- **MVVM 패턴 적용**: 뷰와 비즈니스 로직을 분리하여 코드의 응집도를 높이고 유지보수성 향상
- **플랫폼 독립적 설계**: .NET Standard 2.0 기반의 Jamesnet.Core 프레임워크를 사용하여 다양한 플랫폼에서 동일하게 동작

#### 주요 기능 및 구현 사항

1. **대규모 프로젝트 아키텍처**
   - [x] 모듈화 및 분산 시스템 설계
   - [x] 의존성 주입을 통한 느슨한 결합
   - [x] 플러그인 기반 확장 가능한 구조

2. **고급 UnoPlatform 기술**
   - [x] 다양한 CustomControl 구현
   - [x] MVVM 패턴의 실제 적용 사례
   - [x] 플랫폼 독립적인 설계

3. **성능 최적화**
   - [x] 효율적인 리소스 관리 및 메모리 사용
   - [x] 비동기 프로그래밍 패턴 적용
   - [x] 렌더링 최적화 기법

4. **UI/UX 디자인**
   - [x] 정교한 UI 재현을 위한 기술 학습
   - [x] 사용자 정의 애니메이션 및 전환 효과
   - [x] 동적 테마 시스템

5. **프레임워크 설계**
   - [x] 이벤트 기반 통신 시스템
   - [x] 상태 관리 패턴 구현
   - [x] 확장 가능한 네비게이션 시스템

## 기술 스택

- .NET 8.0
- UnoPlatform
- Jamesnet.Core (.NET Standard 2.0)
- Jamesnet.Uno

## 시작하기

### 필요 조건

- Visual Studio 2022 이상
- .NET 8.0 SDK
- UnoPlatform 프로젝트 템플릿 설치

### 설치 및 실행

#### 1. 리포지터리 클론:

```bash
git clone https://github.com/jamesnet214/samples-uno.git
```

#### 2. 솔루션 열기

- [x] Visual Studio
- [x] Visual Studio Code
- [x] JetBrains Rider

<div style="text-align: center;">
    <img src="https://github.com/user-attachments/assets/af70f422-7057-4e77-a54d-042ee8358d2a" width="32%"/>
    <img src="https://github.com/user-attachments/assets/e4feaa10-a107-4b58-8d13-1d8be620ec62" width="32%"/>
    <img src="https://github.com/user-attachments/assets/5ff487f6-55e4-43e1-9abf-f8d419ee6943" width="32%"/>
</div>

#### 3. 프로젝트 빌드 및 실행

- [x] 시작 프로젝트 설정
- [x] F5를 누르거나 실행 버튼 클릭

## 학습 기회

이 레포지터리는 WPF 개발자들에게 귀중한 통찰력을 제공합니다:

1. **복잡한 UI 재현**: 정교한 사용자 인터페이스를 재현하는 기술 학습
2. **커스텀 컨트롤 개발**: UnoPlatform 커스텀 컨트롤 구축 과정 이해
3. **실전 MVVM**: 복잡한 애플리케이션에서 MVVM 패턴의 실제 구현 사례 확인
4. **크로스플랫폼 아키텍처 설계**: 플랫폼에 독립적인 프로젝트 설계 방법 학습
5. **성능 최적화**: 대규모 애플리케이션 최적화 전략 학습

## 기여하기

UnoPlatform 샘플 프로젝트에 대한 기여를 환영합니다! 이슈를 제출하거나, 풀 리퀘스트를 생성하거나, 개선 사항을 제안해 주세요.

## 라이선스

이 프로젝트는 MIT 라이선스 하에 배포됩니다. 자세한 내용은 [LICENSE](LICENSE) 파일을 참조하세요.

## 연락처

- 웹사이트: [https://jamesnet.dev](https://jamesnet.dev)
- 이메일: james@jamesnet.dev, vickyqu115@hotmail.com

이 레포지터리를 통해 WPF 개발자들이 UnoPlatform을 활용하여 크로스플랫폼 애플리케이션 개발의 새로운 지평을 열 수 있기를 바랍니다!
