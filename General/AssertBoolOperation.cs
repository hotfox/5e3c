using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Teflon.SDK.Interfaces;
using System.ComponentModel.Composition;
using Teflon.SDK.Exceptions;

namespace Teflon.SDK.Operations
{
    [Export(typeof(IOperation))]
    [ExportMetadata("DeviceType", "General")]
    public sealed class AssertBoolOperation : CodeActivity, IOperation
    {
        public InArgument<bool> Value { get; set; }
        public InArgument<bool> TargetValue { get; set; }

        public Activity Activity { get { return this; } }
        public string OperationName { get; } = "AssertBoolOperation";
        public ExecuteDelegate ExecuteDelegate { get; set; } = null;
        public bool IsRuntimeOperation { get; } = false;

        protected override void Execute(CodeActivityContext context)
        {
            bool value = context.GetValue<bool>(Value);
            bool target_value = context.GetValue<bool>(TargetValue);

            if (target_value!=value)
            {
                throw new AssertFailExeception($"TargetValue:{target_value},Value:{value}");
            }
        }
    }
}
