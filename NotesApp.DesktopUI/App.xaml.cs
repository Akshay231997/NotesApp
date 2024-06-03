using NotesApp.DesktopUI.Views;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace NotesApp.DesktopUI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<ShellWindowView>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<ShellWindowView>();
    }
}
