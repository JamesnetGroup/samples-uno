## 들어가며

오늘은 WPF 개발자들이 UnoPlatform을 활용하여 데스크톱 크로스플랫폼 애플리케이션을 개발할 때 유용한 아키텍처 전략에 대해 깊이 있게 살펴보겠습니다. 이 글은 지난 2024년 9월 26일 한국에서 진행된 닷넷 밋업에서 발표된 내용을 기반으로 재구성한 것입니다. 해당 밋업은 Blazor, MAUI, WPF를 주제로 매달 진행되는 정기 모임으로, 이번에는 WPF 개발자를 위한 UnoPlatform 데스크톱 프로젝트 아키텍처 설계 전략에 대해 2시간 동안 UnoPlatform을 직접 구현하는 시간을 가졌습니다. 약 70명의 개발자가 참가했으며, 대부분 닷넷 개발자이거나 크로스플랫폼에 관심이 있는 비 닷넷 개발자들로 구성되었습니다.

## 소개

안녕하세요. 개발자 이재웅입니다. 저는 Windows Development 분야의 Microsoft MVP로서 2008년부터 WPF를 시작하여 지금까지 WPF를 비롯한 다양한 XAML 기반 플랫폼을 다루고 있습니다. 오랜 기간 동안 WPF를 활용한 여러 프로젝트를 진행해왔으며, 그중 대표적인 오픈소스의 예로 WPF를 비롯한 UnoPlatform, WinUI3 플랫폼에서 각각 동일한 설계로 구현된 리그 오브 레전드 클라이언트 개발을 들 수 있습니다.

- 리그 오브 레전드 WPF: https://github.com/jamesnetgroup/leagueoflegends-wpf
- 리그 오브 레전드 UnoPlatform: https://github.com/jamesnetgroup/leagueoflegends-uno
- 리그 오브 레전드 WinUI3: https://github.com/jamesnetgroup/leagueoflegends-winui3

세 개의 다른 플랫폼이 어떻게 동일한 설계와 소스코드를 통해 리그 오브 레전드를 동일하게 구현했는지를 레포지토리를 통해 확인해볼 수 있습니다. 이번 글에서는 WPF 개발자를 대상으로 UnoPlatform을 활용한 데스크톱 크로스플랫폼 프로젝트의 아키텍처 전략을 소개하고자 합니다. 특히 의존성 주입(DI), 프로젝트 분산화 및 모듈화, MVVM 패턴의 적용 등 핵심 아키텍처 기술을 중심으로 설명할 것입니다. 또한 .NET Standard 2.0 기반의 Jamesnet.Core 프레임워크를 사용하여 WPF, UnoPlatform, WinUI3 등 다양한 플랫폼에서 동일하게 동작하고, 플랫폼에 상관없이 동일한 프로젝트 설계를 구현하는 방법을 상세히 다루고 있습니다.

만약 여러분이 WPF 또는 WinUI3, UWP 개발자라면 더욱 흥미롭게 UnoPlatform 프로젝트 아키텍처 전략에 대해 살펴볼 수 있을 것이며, 단 한 번의 데스크톱 개발을 통해 Windows, MacOS, Ubuntu에서 완벽하게 동일한 동작을 가능하게 하는 크로스플랫폼에 관심이 있다면 이 글이 큰 도움이 될 것입니다.



## 주제: UnoPlatform 자전거 대여소 관리자 애플리케이션 개발

부제: WPF 개발자를 위한 UnoPlatform 데스크톱 프로젝트 아키텍처 전략

이 글에서는 UnoPlatform을 활용하여 자전거 대여소 관리 애플리케이션을 개발하는 과정을 단계별로 상세히 설명합니다. 이를 통해 WPF 개발자들이 UnoPlatform의 아키텍처와 개발 방법을 깊이 있게 이해하고, 기존의 WPF 프로젝트를 크로스플랫폼으로 전환하는 데 실질적인 도움을 얻을 수 있도록 구성하였습니다.

#### 서버 구성

애플리케이션의 데이터 처리를 위해 서버 측에서는 Blazor로 구현된 API를 사용합니다. 이 API는 Docker로 구성되어 있으며, MySQL 데이터베이스와 Entity Framework Core를 사용하여 데이터 관리를 수행합니다. 이 서버는 이전 Blazor 세션에서 Gusam Park MVP가 발표한 내용으로, 해당 Docker 이미지를 그대로 사용할 수 있습니다.

서버의 주요 스펙은 다음과 같습니다.

- 프레임워크: Blazor
- 데이터베이스: MySQL
- ORM: Entity Framework Core
- 배포 환경: Docker

GitHub 주소는 다음과 같습니다.

- 서버 소스 코드: [GitHub Repository](https://github.com/blazorstudy/bicycle-sharing-system-workshop/tree/main/sessions/1.%20blazor)

서버 환경은 이미 Docker로 구성되어 있으므로, 별도의 서버 구축 없이 기존의 Docker 이미지를 사용하여 애플리케이션과 연동할 수 있습니다. 따라서 UnoPlatform 애플리케이션에서는 이 API를 활용하여 자전거 대여소 정보 등을 처리할 수 있습니다.

이러한 접근 방식은 클라이언트와 서버 간의 개발 효율성을 높이고, 재사용성을 극대화하는 데 도움이 됩니다. 또한, Docker를 활용함으로써 개발 환경의 일관성을 유지하고, 배포 및 스케일링을 용이하게 할 수 있습니다.

## 목차

1. UnoPlatform 설치
2. 프레임워크 프로젝트 복사
3. 작업 순서
   - 메인 애플리케이션 프로젝트 생성
   - XAML 기반 프로젝트 아키텍처를 위한 기술 소개
   - AppBootstrapper 구현
   - App.xaml.cs 설정
   - 라이브러리 생성 (BicycleSharingSystem.Main)
   - CustomControl 구현 규칙
   - 애플리케이션에서 뷰의 참조 추가
   - MainContent 뷰모델 구성
   - 라이브러리 생성 (.Navigate)
   - MenuContent 모듈 주입
   - 라이브러리 생성 (.Rental)
   - RentalContent 뷰 등록 및 메뉴 연결
   - RentalContent 구현
   - 라이브러리 생성 (.Support)
   - MenuManager 설계
4. 핵심 아키텍처 기술 심화
   - 의존성 주입(DI)
   - 프로젝트 분산화 및 모듈화
   - MVVM 패턴의 적용
   - 플랫폼에 독립적인 설계



## 1. UnoPlatform 설치

UnoPlatform을 사용하여 크로스플랫폼 애플리케이션을 개발하기 위해서는 먼저 프로젝트 템플릿을 설치해야 합니다. 다음 명령어를 통해 최신 UnoPlatform 프로젝트 템플릿을 설치할 수 있습니다.

```
dotnet new install Uno.ProjectTemplates.Dotnet
```

이 명령어를 실행하면 .NET CLI를 통해 UnoPlatform의 프로젝트 템플릿이 설치되며, 이후 `dotnet new` 명령어를 사용하여 다양한 UnoPlatform 프로젝트를 생성할 수 있습니다. 이를 통해 Windows, macOS, Linux 등 다양한 운영체제에서 동작하는 애플리케이션을 개발할 수 있습니다.



## 2. 프레임워크 프로젝트 복사

프로젝트의 기반이 되는 프레임워크를 복사하여 사용합니다. 이는 프로젝트의 일관성을 유지하고, 핵심 기능을 재사용하기 위한 중요한 단계입니다.

```
\sessions\3. uno-platform\start\Jamesnet.Core
\sessions\3. uno-platform\start\Jamesnet.Uno
```

Jamesnet.Core는 .NET Standard 2.0 기반의 프레임워크로, 의존성 주입(DI), MVVM 패턴, 뷰모델 초기화 등 다양한 핵심 기능을 제공합니다. Jamesnet.Uno는 UnoPlatform을 위한 확장 기능을 포함하고 있으며, WPF, UnoPlatform, WinUI3 등 다양한 플랫폼에서 동일하게 동작합니다. 이 프레임워크를 사용함으로써 플랫폼에 종속되지 않고 동일한 프로젝트 설계를 구현할 수 있으며, 코드의 재사용성과 유지보수성을 크게 향상시킬 수 있습니다.



## 3. 작업 순서

### 메인 애플리케이션 프로젝트 생성

먼저 메인 애플리케이션 프로젝트를 생성합니다. 프로젝트 이름은 BicycleSharingSystem으로 합니다. 타겟 프레임워크로는 `net8.0-desktop`을 사용하며, UnoPlatform의 Desktop은 Windows, macOS, Linux 등 크로스플랫폼을 지원합니다. 프로젝트 생성 시에는 빈(blank) 모드로 생성하여 불필요한 기본 코드를 최소화합니다. 이렇게 하면 프로젝트의 구조를 더 명확하게 파악할 수 있고, 필요에 따라 필요한 구성 요소를 직접 추가하여 관리할 수 있습니다.

```
<TargetFrameworks>net8.0-desktop</TargetFrameworks>
```

프로젝트를 생성한 후에는 TargetFramework 또는 TargetFrameworks 지정이 제대로 되어 있고, 프로젝트가 제대로 빌드되는지 먼저 확인하는 것이 좋습니다. 또한 비주얼 스튜디오 또는 Rider 관련 버그가 있을 수도 있으니 빌드가 제대로 안 되는 경우 개발 프로그램을 재시작하는 것도 좋은 팁입니다.



### XAML 기반 프로젝트 아키텍처를 위한 기술 소개

WPF, UnoPlatform, WinUI3 등 다양한 XAML 기반 프레임워크를 동일하게 사용하기 위한 전략을 수립합니다. 이를 위해 .NET Standard 2.0을 사용하여 플랫폼에 독립적인 코어 로직을 구현합니다. 적용된 프로젝트들은 모두 다른 플랫폼에서 동일하게 사용되는 .NET Standard 2.0 기반의 Jamesnet.Core 프레임워크입니다.

- WPF 기반: WPF를 사용한 리그 오브 레전드 클라이언트 구현.
- UnoPlatform 기반: UnoPlatform을 사용한 MacOS, Ubuntu, Windows 리그 오브 레전드 클라이언트 구현.
- WinUI3 기반: UnoPlatform을 그대로 변환한 WinUI3 리그 오브 레전드 클라이언트 구현.

이러한 프로젝트들은 동일한 아키텍처와 코어 로직을 공유하며, 플랫폼에 따라 UI 레이어만 변경하여 다양한 환경에서 동작합니다. 이는 의존성 주입(DI), 프로젝트 분산화, 모듈화 등의 아키텍처 기술을 통해 가능합니다.

Jamesnet.Core 프레임워크는 MVVM 패턴 구현, RelayCommand 사용, 의존성 주입(DI), 뷰모델 초기화, IView 인터페이스 활용, UnoContent 구현 등의 기능을 제공합니다. 이를 통해 WPF, UnoPlatform, WinUI3 등 다양한 플랫폼에서 동일한 코어 로직과 아키텍처를 공유할 수 있으며, 개발 생산성을 높이고 유지보수 비용을 절감하는 데 큰 도움이 됩니다.

프로젝트에 필요한 모든 아키텍처 기술을 자체 프레임워크로 구현하여 관리하는 방식은 심층적인 문제 해결, 효율적인 디버깅, 지속적인 개선, 깊이 있는 학습, 유연한 확장성 등의 이점을 제공합니다. 이러한 접근 방식은 단순한 코드 재사용을 넘어서, 보다 견고하고 발전적인 개발 생태계를 구축하는 데 기여합니다.



### AppBootstrapper 구현

AppBootstrapper는 애플리케이션의 시작 지점에서 필요한 설정과 초기화를 담당하는 핵심 클래스입니다. 이를 통해 의존성 주입, 모듈 초기화, 뷰모델 등록 등을 효율적으로 관리할 수 있습니다.

```csharp
internal class BicycleSharingSystemBootstrapper : AppBootstrapper
{
    protected override void LayerInitializer(IContainer container, ILayerManager layer)
    {
        // 레이어 초기화 및 모듈 주입
    }

    protected override void RegisterDependencies(IContainer container)
    {
        // 의존성 주입을 위한 싱글턴 및 인스턴스 등록
    }

    protected override void RegisterViewModels(IViewModelMapper viewModelMapper)
    {
        // 뷰와 뷰모델 간의 매핑 등록
    }
}
```

LayerInitializer에서는 레이어 매니저를 초기화하고, 뷰를 특정 레이어에 매핑합니다. RegisterDependencies에서는 서비스나 뷰 등을 의존성 주입 컨테이너에 등록하며, RegisterViewModels에서는 뷰와 뷰모델 간의 관계를 등록하여 MVVM 패턴을 지원합니다. 이러한 구조를 통해 애플리케이션의 초기화 로직을 깔끔하게 분리하고, 확장성과 유지보수성을 높일 수 있습니다.



### App.xaml.cs 설정

App.xaml.cs는 애플리케이션의 진입점이며, 여기서 AppBootstrapper를 초기화합니다.

```csharp
protected override void OnLaunched(LaunchActivatedEventArgs args)
{
    var bootstrapper = new BicycleSharingSystemBootstrapper();
    bootstrapper.Run();
}
```

OnLaunched 메서드에서 AppBootstrapper의 인스턴스를 생성하고, Run 메서드를 호출하여 초기화를 진행합니다. 이 시점에서 의존성 주입 컨테이너가 구성되고, 뷰와 뷰모델의 매핑이 설정되며, 초기 화면이 표시될 준비를 합니다. UnoPlatform은 WPF의 전통적인 프로젝트 구조를 계승하고 있으므로, WPF 개발자들이 익숙한 방식으로 애플리케이션의 흐름을 이해하고 관리할 수 있습니다.



### 라이브러리 생성 (BicycleSharingSystem.Main)

메인 화면과 관련된 코드를 별도의 라이브러리 프로젝트로 분리하여 관리합니다. 프로젝트 이름은 BicycleSharingSystem.Main으로 합니다. 이는 메인 애플리케이션의 핵심 로직과 UI를 분리하여 코드의 복잡성을 낮추고, 모듈화된 구조를 갖추기 위함입니다. 각 모듈이 독립적으로 개발 및 테스트될 수 있어 개발 효율성이 높아지며, 코드의 재사용성이 향상되어 유지보수 비용이 절감됩니다. 또한 팀 간의 협업이 수월해집니다.



### CustomControl 구현 규칙

CustomControl을 구현할 때는 클래스 파일과 리소스 파일을 분리하여 관리합니다. 클래스 파일은 UI/Views/MainContent.cs로, ResourceDictionary는 Themes/Views/MainContent.xaml로 구성합니다. 이러한 구조를 통해 코드와 리소스를 명확하게 분리하여 관리할 수 있으며, 복잡한 컨트롤을 구현할 때 유용합니다.

Themes/Generic.xaml 파일은 CustomControl의 기본 스타일과 템플릿을 정의하는 곳입니다.

```xml
<ResourceDictionary Source="ms-appx:///BicycleSharingSystem.Main/Themes/Views/MainContent.xaml"/>
```

UnoPlatform에서는 ms-appx:/// 형식을 사용하여 리소스의 경로를 지정하며, WPF에서는 ";component/" 형식을 사용하므로, 플랫폼 간의 차이점을 유의해야 합니다. 이 파일을 통해 CustomControl의 기본 스타일이 애플리케이션 전체에서 적용됩니다.

MainContent 클래스는 다음과 같이 구현됩니다.

```csharp
using Jamesnet.Uno;

namespace BicycleSharingSystem.Main.UI.Views;

public class MainContent : UnoView
{
    public MainContent()
    {
        DefaultStyleKey = typeof(MainContent);
    }
}
```

UnoView를 상속받아 UnoPlatform의 뷰로 동작하며, DefaultStyleKey를 설정하여 해당 컨트롤의 기본 스타일을 지정합니다. WPF에서는 전통적으로 DefaultStyleKeyProperty를 static 생성자에서 등록하는 것이 일반적이지만, UnoPlatform, WinUI3, UWP, AvaloniaUI, OpenSilver와 같은 플랫폼에서는 인스턴스 생성자에서 타입을 지정하는 것이 일반적입니다. 이러한 접근 방식으로 다양한 플랫폼에서 일관된 방식으로 컨트롤의 스타일을 지정할 수 있으며, 크로스플랫폼 개발 시 코드의 호환성과 유지보수성을 향상시킬 수 있습니다.

MainContent 구현을 위한 테스트용 Slider 컨트롤은 다음과 같이 추가됩니다.

```xml
<ResourceDictionary
  xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
  xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
  xmlns:views="using:BicycleSharingSystem.Main.UI.Views">

  <Style TargetType="views:MainContent">
    <Setter Property="Template">
      <Setter.Value>
        <ControlTemplate TargetType="views:MainContent">
          <Grid>
            <Slider/>
          </Grid>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
</ResourceDictionary>
```

xmlns:views 네임스페이스를 추가하여 MainContent 클래스를 참조하고, Slider 컨트롤을 추가하여 컨트롤이 제대로 동작하는지 테스트합니다. 이렇게 기본적인 컨트롤을 추가하여 레이아웃과 스타일이 올바르게 적용되는지 확인할 수 있습니다.



### 애플리케이션에서 뷰의 참조 추가

MainContent를 애플리케이션에서 사용할 수 있도록 설정합니다. BicycleSharingSystem.Main 프로젝트를 메인 애플리케이션에 참조로 추가하고, AppBootstrapper의 RegisterDependencies 메서드에서 MainContent를 싱글턴으로 등록합니다.

```csharp
protected override void RegisterDependencies(IContainer container)
{
    container.RegisterSingleton<MainContent>();
}
```

LayerInitializer 메서드에서 MainContent를 레이어에 매핑합니다.

```csharp
protected override void LayerInitializer(IContainer container, ILayerManager layer)
{
    IView mainContent = container.Resolve<MainContent>();

    layer.Mapping("MainLayer", mainContent);
}
```

MainPage.xaml에 UnoLayer 컨트롤을 추가하고 Name 속성을 "MainLayer"로 설정합니다.

```xml
<UnoLayer Name="MainLayer"/>
```

이를 통해 MainContent가 MainLayer에 주입되어 화면에 표시됩니다.



### MainContent 뷰모델 구성

MVVM 패턴을 적용하여 MainContent의 뷰모델을 구성합니다. Local/ViewModels/MainContentViewModel.cs 파일을 생성하여 다음과 같이 뷰모델을 구현합니다.

```csharp
public class MainContentViewModel : ViewModelBase
{
    // 프로퍼티와 명령 정의
}
```

AppBootstrapper의 RegisterViewModels 메서드에서 뷰와 뷰모델 간의 매핑을 등록합니다.

```csharp
protected override void RegisterViewModels(IViewModelMapper viewModelMapper)
{
    viewModelMapper.Register<MainContent, MainContentViewModel>();
}
```

이를 통해 뷰모델의 프로퍼티 변경이나 명령이 제대로 동작하는지 테스트할 수 있으며, MVVM 패턴을 통해 뷰와 뷰모델을 분리함으로써 코드의 응집도를 높이고, 테스트 가능성을 향상시킬 수 있습니다.



### 라이브러리 생성 (.Navigate)

메뉴 네비게이션 기능을 담당하는 모듈을 별도의 라이브러리로 분리합니다. 프로젝트 이름은 BicycleSharingSystem.Navigate로 합니다. UI/Views 디렉토리에 MenuContent.cs 클래스를 생성하고, Themes/Views 디렉토리에 MenuContent.xaml 리소스 파일을 생성합니다. Themes/Generic.xaml 파일에는 다음과 같이 MenuContent.xaml을 포함하도록 설정합니다.

```xml
<ResourceDictionary Source="ms-appx:///BicycleSharingSystem.Navigate/Themes/Views/MenuContent.xaml"/>
```

Local/ViewModels 디렉토리에 MenuContentViewModel.cs 뷰모델을 생성하고, XAML에서는 RadioButton을 사용하여 메뉴를 구성하며 Command를 바인딩합니다. Command 구현은 메뉴 선택 시 동작할 명령을 CommandParameter를 사용하여 다음과 같이 구현합니다.

```csharp
public ICommand NavigateCommand { get; }

public MenuContentViewModel()
{
    NavigateCommand = new RelayCommand<string>(NavigateTo);
}

private void NavigateTo(string menu)
{
    // 메뉴에 따른 네비게이션 로직 구현
}
```



### MenuContent 모듈 주입

MenuContent를 애플리케이션에 모듈로 주입하여 사용합니다. 애플리케이션에서 참조 추가: BicycleSharingSystem.Navigate 프로젝트를 메인 애플리케이션에 참조로 추가합니다. 싱글턴 등록: AppBootstrapper의 RegisterDependencies 메서드에서 MenuContent를 싱글턴으로 등록합니다.

```csharp
container.RegisterSingleton<MenuContent>();
```

뷰 초기 주입: LayerInitializer 메서드에서 MenuContent를 레이어에 매핑합니다.

```csharp
IView menuContent = container.Resolve<MenuContent>();

layer.Mapping("MenuLayer", menuContent);
```

MainPage.xaml에 UnoLayer 컨트롤을 추가하고 Name 속성을 "MenuLayer"로 설정합니다.

```xml
<UnoLayer Name="MenuLayer"/>
```

이를 통해 MenuContent가 MenuLayer에 주입되어 화면에 표시됩니다.



### 라이브러리 생성 (.Rental)

자전거 대여소 정보 관리를 위한 화면을 별도의 모듈로 구현합니다. 프로젝트 이름은 BicycleSharingSystem.Rental로 합니다. UI/Views 디렉토리에 RentalContent.cs 클래스를 생성하고, Themes/Views 디렉토리에 RentalContent.xaml 리소스 파일을 생성합니다. Themes/Generic.xaml 파일에는 다음과 같이 설정합니다.

```xml
<ResourceDictionary Source="ms-appx:///BicycleSharingSystem.Rental/Themes/Views/RentalContent.xaml"/>
```



### RentalContent 뷰 등록 및 메뉴 연결

RentalContent를 애플리케이션에 등록하고, 메뉴에서 선택 시 해당 뷰로 네비게이션되도록 설정합니다. 이름으로 뷰 등록: AppBootstrapper의 RegisterDependencies 메서드에서 RentalContent를 이름으로 등록합니다.

```csharp
container.Register<RentalContent>("RentalContent");
```

MenuContentViewModel에서 RentalContent 뷰를 주입합니다.

```csharp
private void NavigateTo(string menu)
{
    var layerManager = _container.Resolve<ILayerManager>();
    var contentView = _container.Resolve<IView>($"{menu}Content");
    layerManager.Show("ContentLayer", contentView);
}
```

이를 통해 메뉴 이름과 일치하는 뷰를 동적으로 로드하여 표시할 수 있습니다.



### RentalContent 구현

RentalContent의 실제 기능을 구현하기 위해 먼저 RentalViewModel을 생성합니다.

```csharp
public class RentalViewModel : ViewModelBase
{
    private ObservableCollection<RentalStation> _stations;
    private RentalStation _selectedStation;

    public ObservableCollection<RentalStation> Stations
    {
        get => _stations;
        set => SetProperty(ref _stations, value);
    }

    public RentalStation SelectedStation
    {
        get => _selectedStation;
        set => SetProperty(ref _selectedStation, value);
    }
}
```

자전거 대여소 정보를 담는 데이터 모델을 다음과 같이 정의합니다.

```csharp
public class RentalStation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int TotalBikes { get; set; }
    public int AvailableBikes { get; set; }
    public DateTime LastUpdate { get; set; }
    public string Status { get; set; }
}
```



### 라이브러리 생성 (.Support)

공용 컨트롤, 서비스, API 연결 등을 관리하기 위한 지원 모듈을 생성합니다. 프로젝트 이름은 BicycleSharingSystem.Support로 합니다. UI/Units 디렉토리에 커스텀 컨트롤 BicRadioButton.cs를 생성하고, Themes/Units 디렉토리에 BicRadioButton.xaml 리소스 파일을 생성하여 컨트롤의 스타일과 템플릿을 정의합니다. 외부 API와의 통신을 처리하는 서비스는 다음과 같이 구현합니다.

```csharp
public class RentalService
{
    private readonly HttpClient _httpClient;

    public RentalService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api.bicycle-rental.com/");
    }

    public async Task<IEnumerable<RentalStation>> GetRentalStationsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("stations");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<RentalStation>>(content);
            }
            return Array.Empty<RentalStation>();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"API 호출 중 오류 발생: {ex.Message}");
            return Array.Empty<RentalStation>();
        }
    }

    public async Task<bool> UpdateStationAsync(RentalStation station)
    {
        try
        {
            var content = JsonSerializer.Serialize(station);
            var response = await _httpClient.PutAsync(
                $"stations/{station.Id}",
                new StringContent(content, Encoding.UTF8, "application/json")
            );
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"업데이트 중 오류 발생: {ex.Message}");
            return false;
        }
    }
}
```



### MenuManager 설계

메뉴 관리를 더욱 유연하고 확장성 있게 하기 위해 MenuManager를 설계합니다. 인터페이스를 다음과 같이 정의합니다.

```csharp
public interface IMenuManager
{
    void NavigateTo(string menu);
    bool CanNavigate(string menu);
    event EventHandler<string> NavigationChanged;
}
```

이 인터페이스를 구현하는 MenuManager 클래스는 다음과 같이 작성됩니다.

```csharp
public class MenuManager : IMenuManager
{
    private readonly IContainer _container;
    private readonly Dictionary<string, Type> _registeredViews;
    private string _currentView;

    public event EventHandler<string> NavigationChanged;

    public MenuManager(IContainer container)
    {
        _container = container;
        _registeredViews = new Dictionary<string, Type>();
    }

    public void RegisterView(string key, Type viewType)
    {
        if (!_registeredViews.ContainsKey(key))
        {
            _registeredViews.Add(key, viewType);
        }
    }

    public void NavigateTo(string menu)
    {
        if (!CanNavigate(menu))
            return;

        var layerManager = _container.Resolve<ILayerManager>();
        var contentView = _container.Resolve<IView>($"{menu}Content");

        layerManager.Show("ContentLayer", contentView);

        _currentView = menu;
        NavigationChanged?.Invoke(this, menu);
    }

    public bool CanNavigate(string menu)
    {
        return _registeredViews.ContainsKey(menu) && _currentView != menu;
    }
}
```



## 4. 핵심 아키텍처 기술 심화

### 의존성 주입(DI)

의존성 주입은 객체 지향 프로그래밍에서 객체 간의 의존성을 줄이고 결합도를 낮추어 코드의 유연성과 확장성을 높이는 중요한 패턴입니다. Jamesnet.Core 프레임워크에서는 이러한 의존성 주입을 지원하기 위해 컨테이너를 제공합니다. 다음은 의존성 주입 컨테이너를 사용하여 필요한 서비스나 뷰모델 등을 등록하는 방법의 예시입니다.

```csharp
using BicycleSharingSystem.Bicycle.Local.ViewModels;
using BicycleSharingSystem.Bicycle.UI.Views;
using BicycleSharingSystem.Main.Local.ViewModels;
using BicycleSharingSystem.Main.UI.Views;
using BicycleSharingSystem.Navigate.Local.Services;
using BicycleSharingSystem.Navigate.Local.ViewModels;
using BicycleSharingSystem.Navigate.UI.Views;
using BicycleSharingSystem.Rental.Local.ViewModels;
using BicycleSharingSystem.Rental.UI.Views;
using BicycleSharingSystem.Support.Local.Services;
using Jamesnet.Core;

namespace BicycleSharingSystem;
internal class BicycleSharingSystemBootstrapper : AppBootstrapper
{
    protected override void RegisterViewModels(IViewModelMapper vmMapper)
    {
        vmMapper.Register<MainContent, MainContentViewModel>();
        vmMapper.Register<MenuContent, MenuContentViewModel>();
        vmMapper.Register<BicycleContent, BicycleContentViewModel>();
        vmMapper.Register<RentalContent, RentalContentViewModel>();
    }

    protected override void RegisterDependencies(IContainer container)
    {
        container.RegisterSingleton<IBicycleSharingService, BicycleSharingService>();
        container.RegisterSingleton<IRentalOfficeService, RentalOfficeService>();
        container.RegisterSingleton<IMenuManager, MenuManager>();

        container.RegisterSingleton<IView, MainContent>();
        container.RegisterSingleton<IView, MenuContent>();
        container.RegisterSingleton<IView, BicycleContent>("BicycleContent");
        container.RegisterSingleton<IView, RentalContent>("RentalContent");
    }

    protected override void LayerInitializer(IContainer container, ILayerManager layer)
    {
        IView mainContent = container.Resolve<MainContent>();
        IView menuContent = container.Resolve<MenuContent>();

        layer.Mapping("MainLayer", mainContent);
        layer.Mapping("MenuLayer", menuContent);
    }

    protected override void OnStartup()
    {
    }
}
```

이 코드를 통해 각종 서비스나 뷰모델, 뷰 등을 컨테이너에 등록하고, 필요한 곳에서 주입받아 사용할 수 있습니다. 특히 RegisterDependencies 메서드에서 IBicycleSharingService, IRentalOfficeService, IMenuManager 등의 서비스 인터페이스와 구현체를 등록하여 의존성 주입을 설정합니다.

MenuManager의 경우, BicycleSharingSystem.Navigate 프로젝트에 분산화되어 구현되었지만, 이를 모든 분산된 프로젝트 영역에서 주입받아 사용할 수 있도록 IMenuManager 인터페이스를 만들어 공용 프로젝트인 BicycleSharingSystem.Support에서 전략적으로 정의하였습니다. 이렇게 함으로써 각 모듈이 IMenuManager 인터페이스를 통해 MenuManager의 기능을 활용할 수 있으며, 의존성 역전 원칙을 준수하여 모듈 간의 결합도를 낮추고 확장성을 높였습니다.

IMenuManager 인터페이스를 공용 프로젝트에 배치함으로써 각 모듈은 MenuManager의 구현 세부 사항에 의존하지 않고, 인터페이스를 통해 필요한 기능만을 사용할 수 있습니다. 이는 SOLID 원칙 중 하나인 의존성 역전 원칙(DIP)을 적용한 사례로, 고수준 모듈이 저수준 모듈에 의존하지 않고 추상화에 의존하도록 설계되었습니다.

이러한 구조는 프로젝트의 유지보수성과 확장성을 크게 향상시킵니다. 새로운 메뉴 기능을 추가하거나 MenuManager의 내부 구현을 변경하더라도 인터페이스 계약이 유지되는 한 다른 모듈에 영향을 주지 않기 때문입니다. 따라서 각 모듈은 독립적으로 개발되고 테스트될 수 있으며, 팀 간 협업도 수월해집니다.

또한, 의존성 주입 컨테이너를 통해 IMenuManager를 싱글턴으로 등록함으로써 애플리케이션 전역에서 하나의 인스턴스만 사용하게 됩니다. 이는 메뉴 관리 기능의 일관성을 유지하고, 리소스 사용을 최적화하는 데 도움이 됩니다.

이처럼 의존성 주입과 인터페이스 기반의 설계를 활용하여 모듈 간의 결합도를 낮추고, 확장성과 유지보수성을 높이는 전략은 대규모 애플리케이션 개발에서 매우 중요합니다. 특히 크로스플랫폼 환경에서 다양한 모듈이 공존하는 경우, 이러한 설계 패턴은 코드의 품질과 개발 효율성을 크게 향상시킬 수 있습니다.



### 프로젝트 분산화 및 모듈화

프로젝트를 기능별로 분리하고 모듈화하면 코드 복잡성 감소, 팀 협업 향상, 코드 재사용성 증가, 유지보수성 향상 등의 장점이 있습니다. 예를 들어, BicycleSharingSystem.Main, BicycleSharingSystem.Navigate, BicycleSharingSystem.Rental 등의 모듈로 프로젝트를 분리하여 관리하였습니다.



### MVVM 패턴의 적용

MVVM 패턴은 WPF와 같은 XAML 기반 프레임워크에서 많이 사용되는 아키텍처 패턴으로, 뷰와 비즈니스 로직을 분리하여 코드의 응집도를 높이고 유지보수성을 향상시킵니다. 이를 통해 뷰와 뷰모델을 분리하여 코드의 응집도를 높이고, 테스트 가능성을 향상시킬 수 있습니다.



### 플랫폼에 독립적인 설계

.NET Standard 2.0 기반의 Jamesnet.Core 프레임워크를 사용함으로써 다양한 플랫폼에서 동일하게 동작하는 코드를 작성할 수 있습니다. 이를 통해 코드 재사용성 극대화, 개발 효율성 향상, 일관된 아키텍처 유지 등의 이점을 얻을 수 있습니다. WPF, UnoPlatform, WinUI3 등 다양한 플랫폼에서 동일한 프로젝트 설계를 구현할 수 있으며, 플랫폼별로 UI 레이어만 변경하여 애플리케이션을 크로스플랫폼으로 확장할 수 있습니다.



## 마무리

이번 글에서는 WPF 개발자가 UnoPlatform을 활용하여 데스크톱 크로스플랫폼 애플리케이션을 개발하기 위한 아키텍처 전략과 구현 방법을 단계별로 자세히 살펴보았습니다. 핵심 포인트는 다음과 같습니다.

의존성 주입(DI)을 통해 객체 간의 결합도를 낮추고, 테스트 가능성과 확장성을 높였습니다. 프로젝트 분산화 및 모듈화를 통해 코드의 복잡성을 낮추고, 팀 간의 협업과 코드 재사용성을 향상시켰습니다. MVVM 패턴의 적용으로 뷰와 뷰모델을 분리하여 코드의 응집도를 높이고, 유지보수성을 향상시켰습니다. 플랫폼에 독립적인 설계를 통해 다양한 플랫폼에서 동일한 프로젝트 설계를 구현하였습니다.

이를 통해 기존의 WPF 개발자들이 익숙한 기술과 경험을 활용하여 크로스플랫폼 애플리케이션을 효율적으로 개발할 수 있습니다. 또한 프로젝트의 확장성과 유지보수성을 높여 장기적인 관점에서의 개발 생산성을 향상시킬 수 있습니다.



**참고 자료 및 추가 정보**

- UnoPlatform 공식 사이트: https://platform.uno/
- Jamesnet.Core GitHub 리포지토리: https://github.com/jamesnetgroup/leagueoflegends-uno
- MVVM 패턴에 대한 이해: https://docs.microsoft.com/ko-kr/dotnet/desktop/wpf/get-started/introduction-to-mvvm

문의 사항이나 추가적인 도움이 필요하시면 댓글로 남겨주시기 바랍니다. 읽어주셔서 감사합니다.

