using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Activities;
using Teflon.SDK.Interfaces;
using System.Linq;

namespace Teflon.Modules
{
    [Export(typeof(IDesigntime))]
    public class SimpleDesigntime:IDesigntime
    {
        [ImportMany]
        IEnumerable<Lazy<IOperation, IOperationData>> extensions;

        public IEnumerable<Activity> GetDeviceOperatipons(string deviceType)
        {
            return new List<Activity>(from op in extensions
                                      where op.Metadata.DeviceType.Equals(deviceType)
                                      select op.Value.Activity);
        }
    }
}
