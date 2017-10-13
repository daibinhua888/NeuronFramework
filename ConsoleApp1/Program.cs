using NNeuronFramework;
using NNeuronFramework.Algorithm;
using NNeuronFramework.ConcreteNetwork;
using NNeuronFramework.OutputConverters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //BPNetworkDemo.Demo();
            //PCANetworkDemo.Demo();
            PCADemo2.Demo();
            //SOMDemo.Demo();

            Console.ReadLine();
        }
    }
}