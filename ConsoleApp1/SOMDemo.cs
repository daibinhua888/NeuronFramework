using NNeuronFramework.Algorithm;
using NNeuronFramework.ConcreteNetwork;
using NNeuronFramework.ConcreteTrainer;
using NNeuronFramework.OutputConverters;
using NNeuronFramework.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace DemoConsole
{
    class SOMDemo
    {
        public static void Demo()
        {
            SOMNetwork network = SOMNetwork.Create(9, 3);

            network.Display();

            var dataSet = DataGenerator.GenerateDataSet3();

            var som = new SOM(network);

            List<SOMSetting> setting = new List<SOMSetting>();
            setting.Add(new SOMSetting() { FromEpoch = 0, ToEpoch=500, LearningRate=0.8, Radius=2 });
            setting.Add(new SOMSetting() { FromEpoch = 501, ToEpoch = 1000, LearningRate = 0.4, Radius = 1 });

            SOMTrainer trainer = new SOMTrainer(network, 1000, som, setting);

            //Normalizer2 normalizer = new Normalizer2();
            Normalizer2 normalizer = new Normalizer2();

            normalizer.Fit(dataSet.XList);

            var normalizedX = normalizer.Normalize(dataSet.XList);

            trainer.Fit(normalizedX);

            List<List<double>> convertedX = trainer.GetOutputs(normalizedX);

            List<List<double>> convertedX2 = trainer.GetOutputs2(normalizedX);

            network.Display();

            Console.WriteLine("OLD VECTORS===>");
            Utils.DisplayListList(dataSet.XList);

            Console.WriteLine("OLD VECTORS(NORMALIZED)===>");
            Utils.DisplayListList(normalizedX);

            Console.WriteLine("NEW CLUSTERED VECTORS===>");
            Utils.DisplayListList(convertedX);

            Console.WriteLine("NEW CLUSTERED VECTORS(DISTANCE)===>");
            Utils.DisplayListList(convertedX2);

            StringBuilder sb_x = new StringBuilder();
            StringBuilder sb_y = new StringBuilder();

            foreach (var l in convertedX2)
            {
                var maxIndex = l.IndexOf(l.Max());

                for (var x = 0; x < network.OutputNeuronCountPerDim; x++)
                    for (var y = 0; y < network.OutputNeuronCountPerDim; y++)
                        if (x * network.OutputNeuronCountPerDim + y == maxIndex)
                        {
                            sb_x.Append(x + ",");
                            sb_y.Append(y + ",");
                            Console.Write("("+x+","+y+"),");
                        }
                Console.WriteLine();
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("import matplotlib.pyplot as plt");
            sb.AppendLine("x1=[" + sb_x.ToString().TrimEnd(",".ToCharArray()) + "]");
            sb.AppendLine("y1=[" + sb_y.ToString().TrimEnd(",".ToCharArray()) + "]");
            sb.AppendLine("plt.plot(x1,y1,'b^')");
            sb.AppendLine("plt.show()");

            var file = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "display.py");
            File.WriteAllText(file, sb.ToString());
            Console.WriteLine("saved to path: " + file);

            System.Diagnostics.Process.Start("C:\\ProgramData\\Anaconda3\\envs\\keras\\python.exe", "\"" + file + "\"");
            
        }
    }
}
