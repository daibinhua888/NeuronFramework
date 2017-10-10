using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuron
{
    public class Neuron
    {
        public Neuron(ActivationFunction activationFunction)
        {
            this.IncomeConnections = new List<NeuronConnection>();
            this.ActivationFunction = activationFunction;
        }

        public double Calculate()
        {
            double sum = CalculateSUM();

            if (this.ActivationFunction == ActivationFunction.SIGMOID)
                this.ActivationFunctionValue = Utils.FunctionUtils.Sigmoid(sum);
            else if (this.ActivationFunction == ActivationFunction.LINEAR)
                this.ActivationFunctionValue = Utils.FunctionUtils.Linear(sum);

            return this.ActivationFunctionValue;
        }

        private double CalculateSUM()
        {
            if (this.IncomeConnections == null || this.IncomeConnections.Count == 0)
                return this.ActivationFunctionValue;

            double sum = 0;
            foreach (var connection in this.IncomeConnections)
                sum += connection.Weight * connection.FromNeuron.ActivationFunctionValue;

            if (this.B != null)
                sum += this.B.Value * this.B.Weight;
            return sum;
        }

        public List<NeuronConnection> IncomeConnections { get; set; }
        public ActivationFunction ActivationFunction { get; set; }
        public double ActivationFunctionValue { get; set; }
        
        public NeuronB B { get; set; }
        public double ErrorValue { get; internal set; }
    }
}
