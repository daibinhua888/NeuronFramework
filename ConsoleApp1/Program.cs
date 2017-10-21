using ConsoleApp1;
using NNeuronFramework;
using NNeuronFramework.Algorithm;
using NNeuronFramework.ConcreteNetwork;
using NNeuronFramework.DL;
using NNeuronFramework.OutputConverters;
using NNeuronFramework.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //BPNetworkDemo.Demo();
            //PCANetworkDemo.Demo();
            //PCADemo2.Demo();
            //SOMDemo.Demo();

            //DLDemo1.Demo();
            LSTMDemo.Demo();

            //double[] x = { 0.5, 0.5 };
            //var y = FunctionUtils.Softmax(x);
            //Utils.DisplayListList(y.ToList());


            //Matrix m = new Matrix(3, 3);
            //m.Display();

            Console.ReadLine();
        }
    }
}