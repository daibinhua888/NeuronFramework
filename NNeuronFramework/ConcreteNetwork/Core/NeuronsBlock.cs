using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core
{
    public class NeuronsBlock
    {
        private int neronsCount;
        public NeuronsBlock(int neronsCount)
        {
            this.neronsCount = neronsCount;
        }

        private List<Neuron> neurons = new List<Neuron>();
        private NeuronB b = new NeuronB();
        
    }
}
