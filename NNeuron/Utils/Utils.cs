using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuron.Utils
{
    public static class Utils
    {
        private static int counted = 0;
        public static double GenerateRandomValue()
        {
            Random r = new Random((int)DateTime.Now.Ticks + counted);
            counted++;
            return r.NextDouble();
        }

        public static void DisplayListList(List<List<double>> x)
        {
            if (x == null)
                return;

            foreach (var vector in x)
            {
                foreach (var v in vector)
                {
                    Console.Write(v);
                    Console.Write(",");
                }
                Console.WriteLine();
            }
        }
    }
}
