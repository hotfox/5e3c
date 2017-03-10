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
    public sealed class AssertDoubleRangeOperation : CodeActivity,IOperation
    {
        public InArgument<double> Min { get; set; }
        public InArgument<double> Max { get; set; }
        public InArgument<double> Value { get; set; }

        public Activity Activity { get { return this; } }
        public string OperationName { get; } = "AssertDoubleRangeOperation";
        public ExecuteDelegate ExecuteDelegate { get; set; } = null;
        public bool IsRuntimeOperation { get; } = false;

        protected override void Execute(CodeActivityContext context)
        {
            double max = context.GetValue<double>(Max);
            double min = context.GetValue<double>(Min);
            double value = context.GetValue<double>(Value);

            if (!(value < max && value >= min))
            {
                throw new AssertFailExeception($"Min:{min},Max:{max},Value{value}");
            }
        }
    }
}
