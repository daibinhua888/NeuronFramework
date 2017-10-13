using NNeuronFramework;
using NNeuronFramework.Algorithm;
using NNeuronFramework.ConcreteNetwork;
using NNeuronFramework.OutputConverters;
using NNeuronFramework.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //BPNetworkDemo.Demo();
            //PCANetworkDemo.Demo();
            //PCADemo2.Demo();
            //SOMDemo.Demo();

            double[] x = { 0.5, 0.3, 0.369};
            var y = FunctionUtils.SoftMax(x);
            Utils.DisplayListList(y.ToList());

            Console.ReadLine();
        }
    }
}