using Jaxx.Net.Launch.DefaultLauchner.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Jaxx.Net.Launch.DefaultLauchner
{
    public class DefaultLauchnerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(DefaultLauncherMain));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}