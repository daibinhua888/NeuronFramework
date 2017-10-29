using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core
{
    public class NeuronsBlock
    {
        public int neronsCount;
        public List<double> weights = new List<double>();
        public double b;

        public NeuronsBlock(int neronsCount)
        {
            this.neronsCount = neronsCount;

            for (var i = 0; i < neronsCount; i++)
                this.weights.Add(Utils.Utils.GenerateRandomValue());

            b = Utils.Utils.GenerateRandomValue();
        }
        
    }
}
