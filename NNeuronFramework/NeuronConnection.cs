using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework
{
    public class NeuronConnection
    {
        public NeuronConnection(Neuron fromNeuron, double weight)
        {
            this.FromNeuron = fromNeuron;
            this.Weight = weight;
        }

        public Neuron FromNeuron { get; set; }
        public double Weight { get; set; }
    }
}
