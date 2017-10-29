using NNeuronFramework.ConcreteNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class LSTMDemo
    {
        public static void Demo()
        {
            LSTMNetwork network = LSTMNetwork.Create2(32, 5);

            network.DisplayByDot2();

            Dictionary<string, double[]> inputs = new Dictionary<string, double[]>();
            inputs.Add("inputs", new double[] { 1, 2, 3, 4, 5 });

            network.Run2(inputs);

        }
    }
}
