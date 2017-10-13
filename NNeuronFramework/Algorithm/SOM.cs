using NNeuronFramework.ConcreteNetwork;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace NNeuronFramework.Algorithm
{
    public class SOM : IAlgorithm
    {
        private SOMNetwork network;

        public double LearningRate { get; internal set; }
        public int Radius { get; internal set; }

        public SOM(SOMNetwork network)
        {
            this.network = network;
        }

        public void Process(List<double> x_vector, List<double> y_vector)
        {
            Forward(x_vector);

            var winnerNeuron = GetWinnerNeuron();

            AdjustWeights(winnerNeuron);
        }

        private Neuron GetWinnerNeuron()
        {
            var winner = this.network.OutputLayer.Neurons.First();
            var winnerDistance= CalculateDistance(winner);

            foreach (var neuron in this.network.OutputLayer.Neurons)
            {
                var distance=CalculateDistance(neuron);
                if (distance < winnerDistance)
                {
                    winner = neuron;
                    winnerDistance = distance;
                }
            }

            return winner;
        }

        public static double CalculateDistance(Neuron neuron)
        {
            double distance = 0;
            foreach (var c in neuron.IncomeConnections)
            {
                var diff = c.Weight - c.FromNeuron.ActivationFunctionValue;

                distance += Math.Pow(diff * diff, 2);
            }

            distance = Math.Sqrt(distance);
            return distance;
        }

        private void Forward(List<double> x_vector)
        {
            this.network.SetInput(x_vector);

            this.network.Calculate();
        }

        private void AdjustWeights(Neuron winner)
        {
            List<SOMNeighbor> neighbors=GetClosestNeurons(winner, this.Radius);

            foreach (var neighbor in neighbors)
            {
                //adjust weights
                //learning rate, radius
                foreach (var neuron in neighbor.Neurons)
                {
                    foreach (var connection in neuron.IncomeConnections)
                    {
                        double delta = (this.LearningRate / (neighbor.Distance + 1)) * (connection.FromNeuron.ActivationFunctionValue-connection.Weight);
                        connection.Weight += delta;
                    }
                }
            }
        }

        private List<SOMNeighbor> GetClosestNeurons(Neuron winner, int radius)
        {
            MatrixMapper matrix = new MatrixMapper(network.OutputNeuronCountPerDim, network.OutputLayer.Neurons);
            return matrix.ClosestNeurons(winner, radius);
        }
    }
}
