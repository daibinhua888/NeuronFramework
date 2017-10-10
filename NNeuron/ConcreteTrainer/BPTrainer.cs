using NNeuron.Algorithm;
using NNeuron.ConcreteNetwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNeuron.ConcreteTrainer
{
    public class BPTrainer: ModelTrainer
    {
        public List<double> Epoch_Errors { get; set; }

        public BPTrainer(BPNetwork network, int epoch, BP algorithm):base(network, epoch, algorithm)
        {
            this.Epoch_Errors = new List<double>();
        }

        public void Fit(List<List<double>> xs, List<List<double>> ys)
        {
            int epoch_executed = 0;
            while (epoch_executed < this.epoch)
            {
                for (var index = 0; index < xs.Count; index++)
                {
                    var x_vector = xs[index];
                    var y_vector = ys[index];

                    algorithm.Process(x_vector, y_vector);
                }

                double totalError = 0;

                foreach (var neuron in this.network.OutputLayer.Neurons)
                    totalError += Math.Abs(neuron.ErrorValue);

                Epoch_Errors.Add(totalError);

                epoch_executed++;
                Console.WriteLine(string.Format("SYS: epoch {0} done.", epoch_executed));
            }
        }

        public List<List<double>> Predict(List<List<double>> xs)
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
