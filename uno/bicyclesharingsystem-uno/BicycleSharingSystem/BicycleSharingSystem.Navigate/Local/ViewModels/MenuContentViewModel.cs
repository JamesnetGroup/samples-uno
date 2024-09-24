using Jamesnet.Core;

namespace BicycleSharingSystem.Navigate.Local.ViewModels;

public class MenuContentViewModel : ViewModelBase
{
    private readonly IContainer _container;

    public ICommand MenuCommand { get; init; }

    public MenuContentViewModel(IContainer container)
    {
        _container = container;

        MenuCommand = new RelayCommand<string>(MenuChanged);
    }

    private void MenuChanged(string menu)
    {
        ILayerManager layer =_container.Resolve<ILayerManager>();

        IView content = _container.Resolve<IView>($"{menu}Content");

        layer.Show("ContentLayer", content);
    }
}
