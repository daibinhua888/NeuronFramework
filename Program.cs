using NFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuronNetwork network = NeuronNetwork.Create(2, new int[] { 2,4,6 }, 1);
            network.Display();

            Console.ReadLine();
        }
    }
}
