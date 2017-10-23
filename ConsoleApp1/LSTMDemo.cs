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
            LSTMNetwork network = LSTMNetwork.Create(128, 5);

            network.DisplayByDot();

            network.Run();

        }
    }
}
