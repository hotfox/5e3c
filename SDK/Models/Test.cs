using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teflon.SDK.Models
{
    public class Test
    {
        public enum Category { EOL,BLT}

        public int TestID { get; set; }

        public string Name { get; set; }
        public Category TestCategory { get; set; }
    }
}
