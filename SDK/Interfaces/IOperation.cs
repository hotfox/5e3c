using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;

namespace Teflon.SDK.Interfaces
{
    public delegate object ExecuteDelegate(params object[] args);

    public interface IOperation
    {
        Activity Activity { get; }
        string OperationName { get; }
        ExecuteDelegate ExecuteDelegate { get; set; }
        bool IsRuntimeOperation { get; }
    }

    public interface IOperationData
    {
        string DeviceType { get; }
    }

    public interface IDevice
    {
        object ExecuteOperation(params object[] args);
    }
    public interface IDeviceData
    {
        string DeviceName { get; }
    }
}
