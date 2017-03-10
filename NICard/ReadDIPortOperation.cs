using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Teflon.SDK.Interfaces;
using System.ComponentModel;
using System.ComponentModel.Composition;

namespace Teflon.SDK.Operations
{

    [Export(typeof(IOperation))]
    [ExportMetadata("DeviceType", "NICard")]
    public sealed class ReadDIPortOperation : CodeActivity<bool>, IOperation
    {
        public InArgument<string> PortNum { get; set; }
        public InArgument<string> DeviceName { get; set; } = "Dev1";

        public Activity Activity { get { return this; } }
        public string OperationName { get; } = "ReadDIPortOperation";
        public ExecuteDelegate ExecuteDelegate { get; set; } = null;
        public bool IsRuntimeOperation { get; } = true;

        protected override bool Execute(CodeActivityContext context)
        {
            string devName = context.GetValue(DeviceName);
            string portNum = context.GetValue(this.PortNum);
            return (bool)ExecuteDelegate.Invoke(this, devName, portNum);
        }
    }
}

