using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Teflon.Modules;
using System.Activities.Statements;
using System.Activities.Presentation;
using System.Xaml;
using System.Activities.XamlIntegration;
using Teflon.SDK.Interfaces;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Teflon.SDK.Operations;
using System.Activities;
using Teflon.SDK.Exceptions;
using System.Threading;

namespace Teflon.SDK
{
    class SynchronousSynchronizationContext : SynchronizationContext
    {
        public override void Post(SendOrPostCallback d, object state)
        {
            this.Send(d, state);
        }
    }

    public class SimpleRuntime
    {
        public string TestItemDirectoryPath { get; private set; }
        public string DeviceLibrayPath { get; private set; }

        public SimpleRuntime(string testItemsDirPath=".\\Tests",string deviceLibPath=".\\Devices")
        {
            TestItemDirectoryPath = testItemsDirPath;
            DeviceLibrayPath = deviceLibPath;
            Initialize();
        }
        public bool RunAll()
        {
            foreach(var value in testitemsDictionary)
            {
                Run(value.Key);
            }
            return true;
        }
        public bool Run(string testName)
        {
             Console.WriteLine($"****************{testName}******************");
             WorkflowApplication app = new WorkflowApplication(testitemsDictionary[testName]);
             app.OnUnhandledException = OnUnhandledException;
             app.SynchronizationContext = new Teflon.SDK.SynchronousSynchronizationContext();
             app.Run();
             return true;
        }

        private void LoadTestItems()
        {
            if (TestItemDirectoryPath != null)
            {
                DirectoryInfo di = new DirectoryInfo(TestItemDirectoryPath);
                if(di.Exists)
                {
                    foreach( FileInfo fi in di.GetFiles("*.test"))
                    {
                        BuildTestItem(fi);
                    }
                }
            }
        }

        private void LoadDevices()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(DeviceLibrayPath));

            container = new CompositionContainer(catalog);

            try
            {
                this.container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        private void BuildTestItem(FileInfo fi)
        {
            try
            {
                string pure_text = File.ReadAllText(fi.FullName);
                string text = Encrypt.DecryptString(pure_text, "hotdog");
                testitemsDictionary.Add(fi.Name, (Flowchart)ActivityXamlServices.Load(new StringReader(text)));
            }
            catch(Exception e)
            {
                throw new TestFormatException(fi.Name);
            }
        }

        private void FillOperations()
        {
            Func<Flowchart, int> fill_operation = fc =>
               {
                   foreach(FlowStep step in fc.Nodes)
                   {
                       IOperation op = step.Action as IOperation;
                       if(op!=null&&op.IsRuntimeOperation)
                       {
                           op.ExecuteDelegate = DispatchOperation;
                       }
                   }
                   return 0;
               };
            foreach(var value in testitemsDictionary.Values)
            {
                fill_operation(value);
            }
        }

        private object DispatchOperation(params object[] args)
        {
            IOperation op = args[0] as IOperation;
            string device_name = args[1] as string;
            IDevice device = devices.First(d => d.Metadata.DeviceName.Equals(device_name)).Value;
            return device.ExecuteOperation(args);
        }

        private void Initialize()
        {
            LoadTestItems();
            LoadDevices();
            FillOperations();
        }

        private UnhandledExceptionAction OnUnhandledException(WorkflowApplicationUnhandledExceptionEventArgs args)
        {
            throw args.UnhandledException;
        }

        private Dictionary<string, Flowchart> testitemsDictionary = new Dictionary<string, Flowchart>();
        private CompositionContainer container;

        [ImportMany]
        private IEnumerable<Lazy<IDevice, IDeviceData>> devices;
    }

    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static public void Main(string[] args)
        {
            SimpleRuntime runtime = new SimpleRuntime();
            runtime.RunAll();
            Console.Read();
        }
    }
}
