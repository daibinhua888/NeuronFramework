using System;
using System.Collections.Generic;
using System.Text;

namespace NNeuron.Algorithm
{
    public class BP : IAlgorithm
    {
        private NeuronNetwork network;

        public BP(NeuronNetwork network, double learningRate=0.9)
        {
            this.network = network;
            this.LearningRate = learningRate;
        }

        public double LearningRate { get; set; }

        public void Process(List<double> x_vector, List<double> y_vector)
        {
            ForwardPropagation(x_vector);
            BackPropagation(y_vector);
        }
        
        private void ForwardPropagation(List<double> x_vector)
        {
            this.network.SetInput(x_vector);

            this.network.Calculate();
        }

        private void BackPropagation(List<double> y_vector)
        {
            //为输出层计算差异值
            for (var index = 0; index < this.network.OutputLayer.Neurons.Count; index++)
            {
                var neuron = this.network.OutputLayer.Neurons[index];
                var realOutput = neuron.ActivationFunctionValue;
                var expectedOutput = y_vector[index];

                double err = realOutput * (1 - realOutput) * (expectedOutput - realOutput);

                neuron.ErrorValue=err;
            }

            List<BaseLayer> allLayers = new List<BaseLayer>();//输入层不需要计算，因此不加入
            allLayers.AddRange(this.network.HiddenLayers);
            allLayers.Add(this.network.OutputLayer);

            //为隐藏层计算差异值
            for (var index = allLayers.Count-2; index >=0; index--)
            {
                var layer = allLayers[index];
                var nextLayer = allLayers[index+1];

                foreach (var neuron in layer.Neurons)
                {
                    double nextLayerSum = 计算针对指定单元的加权求和(neuron, nextLayer.Neurons);

                    var realOutput = neuron.ActivationFunctionValue;
                    double err = realOutput * (1 - realOutput) * nextLayerSum;

                    neuron.ErrorValue = err;

                }

            }

            //计算权重值
            foreach (var layer in allLayers)
            {
                foreach (var neuron in layer.Neurons)
                {
                    foreach (var connection in neuron.IncomeConnections)
                    {
                        double deltaWeight = this.LearningRate *neuron.ErrorValue*connection.FromNeuron.ActivationFunctionValue;
                        double newWeight = connection.Weight + deltaWeight;

                        connection.Weight = newWeight;
                    }
                }
            }

            //计算偏置值
            foreach (var layer in allLayers)
            {
                foreach (var neuron in layer.Neurons)
                {
                    double deltaBWeight = this.LearningRate * neuron.ErrorValue;
                    neuron.B.Weight +=deltaBWeight;
                }
            }
        }

        private double 计算针对指定单元的加权求和(Neuron neuron, List<Neuron> neurons)
        {
            double sum = 0;

            foreach (var n in neurons)
            {
                foreach (var c in n.IncomeConnections)
                {
                    if (c.FromNeuron == neuron)
                    {
                        sum += c.Weight * n.ErrorValue;
                    }
                }
            }

            return sum;
        }
    }
}
