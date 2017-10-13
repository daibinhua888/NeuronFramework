using NNeuronFramework.ConcreteNetwork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NNeuronFramework.Algorithm
{
    public class PCA: IAlgorithm
    {
        private PCANetwork network;
        private double learningRate;
        public PCA(PCANetwork network, double learningRate)
        {
            this.network = network;
            this.learningRate = learningRate;
        }

        public void Process(List<double> x_vector, List<double> y_vector)
        {
            Forward(x_vector);

            AdjustWeights();
        }

        private void Forward(List<double> x_vector)
        {
            this.network.SetInput(x_vector);

            this.network.Calculate();
        }

        private void AdjustWeights()
        {
            for (var index = 0; index < this.network.OutputLayer.Neurons.Count; index++)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("neuron " + index);

                var neuron = this.network.OutputLayer.Neurons[index];

                sb.AppendLine(" (" + neuron.ActivationFunctionValue+")");
                sb.AppendLine();

                int i = 0;
                foreach (var connection in neuron.IncomeConnections)
                {
                    sb.AppendLine("processing connection "+i);
                    //计算上方神经元的权重总计
                    double upperSum = 0;
                    //找到上面引用了此connection的FromNeuron的输出单元
                    var upperOutputNeurons = new List<Neuron>();
                    for (var upperIndex = 0; upperIndex < index; upperIndex++)
                    {
                        var upperNeuron = this.network.OutputLayer.Neurons[upperIndex];
                        var upperNeuron_Connection=upperNeuron.IncomeConnections.Find(c=>c.FromNeuron==connection.FromNeuron);

                        upperSum += upperNeuron.ActivationFunctionValue * upperNeuron_Connection.Weight;
                        //Console.WriteLine("upperSum("+upperIndex+")-->" + upperSum+",value: "+ upperNeuron.ActivationFunctionValue + ",weight:"+ upperNeuron_Connection.Weight);
                    }
                    sb.AppendLine("upperSum:" + upperSum);
                    sb.AppendLine("connection weight:" + connection.Weight);

                    var newIncomeValue = connection.FromNeuron.ActivationFunctionValue - upperSum;
                    var deltaWeight = (newIncomeValue - neuron.ActivationFunctionValue * connection.Weight) * neuron.ActivationFunctionValue * this.learningRate;
                    //var deltaWeight = (newIncomeValue - neuron.ActivationFunctionValue * connection.Weight) * this.learningRate;

                    sb.AppendLine("deltaWeight:" + deltaWeight);

                    sb.AppendLine("newIncomeValue - neuron.ActivationFunctionValue * connection.Weight:" + (newIncomeValue - neuron.ActivationFunctionValue * connection.Weight));
                    sb.AppendLine("neuron.ActivationFunctionValue * this.learningRate:" + (neuron.ActivationFunctionValue * this.learningRate));

                    sb.AppendLine("newIncomeValue:" + newIncomeValue+"("+ connection.FromNeuron.ActivationFunctionValue + "-"+upperSum+")");
                    
                    sb.AppendLine("neuron output value:" + neuron.ActivationFunctionValue);

                    connection.Weight += deltaWeight;
                    sb.AppendLine("connection weight(new):" + connection.Weight);


                    i++;
                }

                sb.AppendLine();

                File.AppendAllText("D:\\1.txt", sb.ToString());
            }
        }
    }
}
