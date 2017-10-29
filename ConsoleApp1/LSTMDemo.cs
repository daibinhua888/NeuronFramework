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
            LSTMNetwork network = LSTMNetwork.Create(32, 5);

            network.DisplayByDot();

            double[] inputs = new double[] {1,2,3,4,5 };

            network.Run(inputs);

        }
    }
}
