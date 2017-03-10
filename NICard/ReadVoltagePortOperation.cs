using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teflon.SDK.Interfaces;

namespace Teflon.SDK.Operations
{
    [Export(typeof(IOperation))]
    [ExportMetadata("DeviceType", "NICard")]
    public sealed class ReadVoltagePortOperation : CodeActivity<Double>, IOperation
    {
        public InArgument<string> PortNum { get; set; }
        public InArgument<string> DeviceName { get; set; } = "Dev1";

        public Activity Activity { get { return this; } }
        public string OperationName { get; } = "ReadVoltagePortOperation";
        public ExecuteDelegate ExecuteDelegate { get; set; } = null;
        public bool IsRuntimeOperation { get; } = true;

        protected override double Execute(CodeActivityContext context)
        {
            string portNum = context.GetValue(this.PortNum);
            string devName = context.GetValue(this.DeviceName);

            return (double)ExecuteDelegate.Invoke(this, devName, portNum);
        }
    }
}
