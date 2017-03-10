using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teflon.SDK.Models
{
    public class Product
    {
        public enum Category { LaserScanner}

        public int ProductID { get; set; }

        public string Name { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
