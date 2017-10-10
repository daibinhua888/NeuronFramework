using NNeuron.Algorithm;
using NNeuron.ConcreteNetwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNeuron.ConcreteTrainer
{
    public class PCATrainer: ModelTrainer
    {
        private double stopCondition;
        public PCATrainer(PCANetwork network, int epoch, PCA algorithm, double stopCondition):base(network, epoch, algorithm)
        {
            this.stopCondition = stopCondition;
        }

        public void Fit(List<List<double>> xs)
        {
            PCANetwork n = (PCANetwork)this.network;
            double previousTotalWeights = n.CalculateTotalWeights();

            int epoch_executed = 0;
            while (epoch_executed < this.epoch)
            {
                for (var index = 0; index < xs.Count; index++)
                {
                    var x_vector = xs[index];

                    algorithm.Process(x_vector, null);
                }

                epoch_executed++;
                Console.WriteLine(string.Format("SYS: epoch {0} done.", epoch_executed));

                var currentTotalWeights = n.CalculateTotalWeights();
                var diff = Math.Abs(currentTotalWeights - previousTotalWeights);

                previousTotalWeights = currentTotalWeights;

                Console.WriteLine("DIFF:"+diff);

                if (diff <= this.stopCondition)
                {
                    Console.WriteLine("Condition OK");
                    break;
                }
            }
        }

        public List<List<double>> GetConvertedDim(List<List<double>> xs)
        {
            List<List<double>> results = new List<List<double>>();

            foreach (var x in xs)
            {
                this.network.SetInput(x);

                this.network.Calculate();

                List<double> outputs = this.network.GetOutput();

                results.Add(outputs);
            }

            return results;
        }
    }
}
