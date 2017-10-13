using NNeuron;
using NNeuron.Algorithm;
using NNeuron.ConcreteNetwork;
using NNeuron.ConcreteTrainer;
using NNeuron.OutputConverters;
using NNeuron.ValidationUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DemoConsole
{
    class BPNetworkDemo
    {
        public static void Demo()
        {
            //BPNetwork network = BPNetwork.Create(1, new int[] { 1/*, 4, 3*/ }, 2, ActivationFunction.SIGMOID, ActivationFunction.SIGMOID);
            BPNetwork network = BPNetwork.Create(32, new int[] { 10/*, 4, 3*/ }, 1, ActivationFunction.SIGMOID, ActivationFunction.SIGMOID);
            network.Display();

            //var dataSet = DataGenerator.GenerateDataSet1();
            var dataSet = DataGenerator.GenerateDataSet1_2();

            var bp = new BP(network, 0.6);
            BPTrainer trainer = new BPTrainer(network, 500, bp);

            trainer.Fit(dataSet.XList, dataSet.YList);

            List<List<double>> predicted_ys = trainer.Predict(dataSet.XList);

            Console.WriteLine("Network Outputs: ");
            foreach (var ys in predicted_ys)
            {
                Console.Write("     ");
                foreach (var y in ys)
                {
                    Console.Write(y);
                    Console.Write(", ");
                }
                Console.WriteLine();
            }

            network.Display();

            CorrectCalculator correctCalculator = new CorrectCalculator();

            var convertedPredictValue = NeuronOutputConverter.OrderInteger(predicted_ys);

            double correct = correctCalculator.Calculate(convertedPredictValue, dataSet.YList);

            Console.WriteLine("Score: {0}%", correct * 100);



            StringBuilder sb_epoch = new StringBuilder();
            StringBuilder sb_errors = new StringBuilder();

            int step = 0;
            trainer.Epoch_Errors.ForEach(err => {
                sb_epoch.Append(step + ",");

                sb_errors.Append(err + ",");

                step++;
            });

            //生成文本数据，拷贝到python中作图
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("import matplotlib.pyplot as plt");
            sb.AppendLine("x1=[" + sb_epoch.ToString().TrimEnd(",".ToCharArray()) + "]");
            sb.AppendLine("y1=[" + sb_errors.ToString().TrimEnd(",".ToCharArray()) + "]");
            sb.AppendLine("plt.plot(x1,y1,'b^')");
            sb.AppendLine("plt.show()");

            var file = System.IO.Path.Combine(AppContext.BaseDirectory, "display.py");
            File.WriteAllText(file, sb.ToString());
            Console.WriteLine("saved to path: " + file);

            System.Diagnostics.Process.Start("C:\\ProgramData\\Anaconda3\\envs\\keras\\python.exe", "\"" + file + "\"");
        }
    }
}
