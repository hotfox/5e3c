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
    [ExportMetadata("DeviceType","NICard")]

    public sealed class WriteDOPortOperation : CodeActivity,IOperation
    {
        public InArgument<bool> TargetValue { get; set; }
        public InArgument<string> PortNum { get; set; }
        public InArgument<string> DeviceName { get; set; } = "Dev1";

        public Activity Activity { get { return this; } }
        public string OperationName { get; } = "WriteDOPortOperation";
        public ExecuteDelegate ExecuteDelegate { get; set; } = null;
        public bool IsRuntimeOperation { get; } = true;

        protected override void Execute(CodeActivityContext context)
        {
            bool value = context.GetValue(TargetValue);
            string devName = context.GetValue(DeviceName);
            string portNum = context.GetValue(this.PortNum);
            ExecuteDelegate.Invoke(this,devName, portNum, value);
        }
    }
}
