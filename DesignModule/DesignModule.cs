using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Prism.Mef.Modularity;
using System.Collections.ObjectModel;
using System.Activities.Presentation.Toolbox;

namespace Teflon.Modules
{
    [ModuleExport(typeof(DesignModule))]
    public class DesignModule:IModule
    {
        public IRegionManager RegionManager { get; private set; }

        [ImportingConstructor]
        public DesignModule(IRegionManager regionManager)
        {
            RegionManager = regionManager;
        }

        public void Initialize()
        {
            RegionManager.RegisterViewWithRegion("DesignRegion", typeof(DesignView));
        }

    }
}
