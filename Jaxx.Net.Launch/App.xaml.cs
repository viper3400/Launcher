using Prism.Ioc;
using Jaxx.Net.Launch.Views;
using System.Windows;
using Prism.Modularity;
using Jaxx.Net.Launch.DefaultLauchner;

namespace Jaxx.Net.Launch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<DefaultLauchnerModule>();
        }
    }
}
