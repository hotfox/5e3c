using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teflon.SDK.Models;

namespace Teflon.Modules.Interfaces
{
    public interface IDataService
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Test> Tests { get; }
    }
}
