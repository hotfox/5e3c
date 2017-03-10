using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using Teflon.SDK.Models;

namespace Teflon.SDK.Events
{
    public class TestItem
    {
        public Test Test{ get; set; }
    }

    public class NewTestItemEvent:PubSubEvent<TestItem>
    {

    }
}
