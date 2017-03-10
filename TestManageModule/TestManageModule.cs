using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using System.ComponentModel.Composition;
using Prism.Mef.Modularity;
using System.Data.SqlClient;
using Prism.Regions;
using System.ComponentModel.Composition.Hosting;

namespace Teflon.Modules
{
    [ModuleExport(typeof(TestManageModule))]
    public class TestManageModule:IModule
    {
        public IRegionManager RegionManager { get; private set; }

        [ImportingConstructor]
        public TestManageModule(IRegionManager regionManager)
        {
            RegionManager = regionManager;
        }

        public void Initialize()
        {
            RegionManager.RegisterViewWithRegion("ProductsRegion", typeof(ProductsView));
        }
    }
}
