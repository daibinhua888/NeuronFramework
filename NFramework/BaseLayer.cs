using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFramework
{
    public abstract class BaseLayer
    {
        public List<Neuron> Neurons { get; set; }

        public void InitNeurons(int count, bool withB=true)
        {
            this.Neurons = new List<Neuron>();
            for (var i = 0; i < count; i++)
            {
                var n = new Neuron();

                if (withB)
                    n.B = new NeuronB();

                this.Neurons.Add(n);
            }
        }
    }
}
