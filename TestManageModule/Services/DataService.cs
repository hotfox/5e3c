using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teflon.Modules.Interfaces;
using Teflon.Modules;
using Teflon.SDK.Models;

namespace Teflon.Modules.Services
{
    [Export(typeof(IDataService))]
    public class DataService:IDataService
    {
        [ImportingConstructor]
        public DataService()
        {
            context = new TestContext();
        }

        public IEnumerable<Product> Products
        {
            get
            {
                return (from p in context.Products
                       select p).ToArray();
            }
        }

        public IEnumerable<Test> Tests
        {
            get
            {
                return (from t in context.Tests
                       select t).ToArray();
            }
        }

        private TestContext context;
    }
}
