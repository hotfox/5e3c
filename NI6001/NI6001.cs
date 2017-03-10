using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teflon.SDK.Interfaces;
using System.ComponentModel.Composition;
using NationalInstruments.DAQmx;

namespace Teflon.SDK.Devices
{
    [Export(typeof(IDevice))]
    [ExportMetadata("DeviceName","Dev1")]
    public class NI6001:IDevice
    {
        public object ExecuteOperation(params object[] args)
        {
            IOperation op = args[0] as IOperation;
            switch (op.OperationName)
            {
                case "ReadVoltagePortOperation": return ReadVoltagePortOperation(args);
                case "ReadCurrentPortOperation": return ReadCurrentPortOperation(args);
                case "ReadDIPortOperation":return ReadDIPortOperation(args);
                case "WriteDOPortOperation":return WriteDOPortOperation(args);
                case "WriteVoltagePortOperation":return WriteVoltagePortOperation(args);
                default:
                    throw new NotImplementedException(op.OperationName);
            }
        }

        private object ReadVoltagePortOperation(params object[] args)
        {
            args.AsParallel().ForAll(o => Console.WriteLine(o.ToString()));

            Task task = new Task();
            string dev_name = args[1] as string;
            string port_num = args[2] as string;

            task.AIChannels.CreateVoltageChannel($"{dev_name}/{port_num}", "", AITerminalConfiguration.Rse, -10, 10, AIVoltageUnits.Volts);
            AnalogSingleChannelReader reader = new AnalogSingleChannelReader(task.Stream);
            task.Start();
            double[] samples = reader.ReadMultiSample(50);
            double sample = samples.Average();
            Console.WriteLine(sample.ToString());
            task.Stop();
            task.Dispose();
            return sample;

        }

        private object ReadCurrentPortOperation(params object[] args)
        {
            args.AsParallel().ForAll(o => Console.WriteLine(o.ToString()));

            Task task = new Task();
            string dev_name = args[1] as string;
            string port_num = args[2] as string;

            task.AIChannels.CreateVoltageChannel($"{dev_name}/{port_num}", "", AITerminalConfiguration.Rse, -10, 10, AIVoltageUnits.Volts);
           // task.AIChannels.CreateCurrentChannel($"{dev_name}/{port_num}", "",AITerminalConfiguration.Differential, -5, 5,AICurrentUnits.Amps);
             AnalogSingleChannelReader reader = new AnalogSingleChannelReader(task.Stream);
             task.Start();
             double[] samples = reader.ReadMultiSample(50);
             double sample = samples.Average();
             Console.WriteLine(sample.ToString());
             task.Stop();
            task.Dispose();
             return sample;
        }

        private object WriteDOPortOperation(params object[] args)
        {
            args.AsParallel().ForAll(o => Console.WriteLine(o.ToString()));

            Task task = new Task();
            string dev_name = args[1] as string;
            string port_num = args[2] as string;
            string port = port_num.Split(new char[] { '.'})[0];
            string line = port_num.Split(new char[] { '.' })[1];

            task.DOChannels.CreateChannel($"{dev_name}/port{port}/line{line}", "", ChannelLineGrouping.OneChannelForEachLine);
            DigitalSingleChannelWriter writer = new DigitalSingleChannelWriter(task.Stream);
            task.Start();
            writer.WriteSingleSampleSingleLine(false, (bool)args[3]);
            task.Stop();
            task.Dispose();
            return true;
        }
        private object WriteVoltagePortOperation(params object[] args)
        {
            args.AsParallel().ForAll(o => Console.WriteLine(o.ToString()));

            Task task = new Task();
            string dev_name = args[1] as string;
            string port_num = args[2] as string;

            task.AOChannels.CreateVoltageChannel($"{dev_name}/{port_num}","",-10,10,AOVoltageUnits.Volts);
            AnalogSingleChannelWriter writer = new AnalogSingleChannelWriter(task.Stream);
            task.Start();
            writer.WriteSingleSample(false, (double)args[3]);
            task.Stop();
            task.Dispose();
            return true;
        }

        private object ReadDIPortOperation(params object[] args)
        {
            args.AsParallel().ForAll(o => Console.WriteLine(o.ToString()));

            Task task = new Task();
            string dev_name = args[1] as string;
            string port_num = args[2] as string;
            string port = port_num.Split(new char[] { '.' })[0];
            string line = port_num.Split(new char[] { '.' })[1];

            task.DIChannels.CreateChannel($"{dev_name}/port{port}/line{line}", "",ChannelLineGrouping.OneChannelForEachLine);
            DigitalSingleChannelReader reader = new DigitalSingleChannelReader(task.Stream);
            task.Start();
            bool res = reader.ReadSingleSampleSingleLine();
            Console.WriteLine(res.ToString());
            task.Stop();
            return res;

        }
    }
}
