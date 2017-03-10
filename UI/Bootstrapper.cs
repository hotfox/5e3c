using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mef;
using System.ComponentModel.Composition.Hosting;
using Teflon.Modules;
using System.IO;

namespace UI
{
    public class Bootstrapper:MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            Shell shell = Container.GetExportedValue<Shell>();
            return shell;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow.Content = Shell;

        }
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(DesignModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new DirectoryCatalog(designDir));
        }

        private const string designDir = @"\\CH3UW1050\Temporary Transfer\cheng Dewi\Desgin";
    }
}
