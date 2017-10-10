using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuron
{
    public abstract class BaseLayer
    {
        public List<Neuron> Neurons { get; set; }

        public void InitNeurons(int count, bool withB, ActivationFunction activationFunction)
        {
            this.Neurons = new List<Neuron>();
            for (var i = 0; i < count; i++)
            {
                var n = new Neuron(activationFunction);

                if (withB)
                    n.B = new NeuronB();

                this.Neurons.Add(n);
            }
        }
    }
}
