using NNeuronFramework.Algorithm;
using NNeuronFramework.ConcreteNetwork;
using NNeuronFramework.OutputConverters;
using System;
using System.Collections.Generic;
using System.Text;
using NNeuronFramework.Utils;


namespace NNeuronFramework.ConcreteTrainer
{
    public class SOMTrainer: ModelTrainer
    {
        public List<SOMSetting> setting;
        public SOMTrainer(SOMNetwork network, int epoch, SOM algorithm, List<SOMSetting> setting):base(network, epoch, algorithm)
        {
            this.setting = setting;
        }

        public void Fit(List<List<double>> xs)
        {
            SOMNetwork n = (SOMNetwork)this.network;
            var som = (SOM)algorithm;

            int epoch_executed = 0;
            while (epoch_executed < this.epoch)
            {
                SOMSetting setting =GetSettingByEPoch(epoch_executed);

                for (var index = 0; index < xs.Count; index++)
                {
                    var x_vector = xs[index];

                    som.LearningRate = setting.LearningRate;
                    som.Radius = setting.Radius;

                    som.Process(x_vector, null);
                }

                //network.Display();
                //Console.ReadKey();

                epoch_executed++;
                Console.WriteLine(string.Format("SYS: epoch {0} done.", epoch_executed));
            }
        }

        private SOMSetting GetSettingByEPoch(int epoch)
        {
            foreach (var config in this.setting)
                if (epoch >= config.FromEpoch && epoch <= config.ToEpoch)
                    return config;

            return null;
        }

        public List<List<double>> GetOutputs(List<List<double>> xs)
        {
            //SOMNetwork n = (SOMNetwork)this.network;

            //MatrixMapper matrix = new MatrixMapper(n.OutputNeuronCountPerDim, n.OutputLayer.Neurons);

            //List<List<double>> results = new List<List<double>>();

            //for (var x = 0; x < matrix.RowsCount; x++)
            //{
            //    var columnCells = new List<double>();
            //    for (var y = 0; y < matrix.ColumnsCount; y++)
            //    {
            //        Neuron neuron=matrix.GetCell(x, y);

            //        columnCells.Add(neuron.ActivationFunctionValue);
            //    }

            //    results.Add(columnCells);
            //}

            //return results;
            List<List<double>> results = new List<List<double>>();

            foreach (var x in xs)
            {
                this.network.SetInput(x);

                this.network.Calculate();

                List<double> outputs = this.network.GetOutput();

                results.Add(outputs);
            }

            Console.WriteLine("==================>");
            Utils.Utils.DisplayListList(results);

            return NeuronOutputConverter.MaxOneHotEncode(results);
            //return results;
        }

        public List<List<double>> GetOutputs2(List<List<double>> xs)
        {
            List<List<double>> results = new List<List<double>>();

            foreach (var x in xs)
            {
                this.network.SetInput(x);

                this.network.Calculate();

                List<double> outputs = new List<double>();

                this.network.OutputLayer.Neurons.ForEach(n => {
                    outputs.Add(SOM.CalculateDistance(n));
                });

                results.Add(outputs);
            }

            return NeuronOutputConverter.MinOneHotEncode(results);
        }
    }
}
