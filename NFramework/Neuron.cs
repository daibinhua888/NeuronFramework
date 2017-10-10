using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFramework
{
    public class Neuron
    {
        public Neuron()
        {
            this.IncomeConnections = new List<NeuronConnection>();
            this.ActivationFunction = ActivationFunction.STEP;
        }

        public double Calculate()
        {
            return this.ActivationFunctionValue;
        }

        public List<NeuronConnection> IncomeConnections { get; set; }
        public ActivationFunction ActivationFunction { get; set; }
        public double ActivationFunctionValue { get; set; }
        
        public NeuronB B { get; set; }
    }
}
