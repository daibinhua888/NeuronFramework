using NNeuronFramework;
using NNeuronFramework.Algorithm;
using NNeuronFramework.ConcreteNetwork;
using NNeuronFramework.ConcreteTrainer;
using NNeuronFramework.OutputConverters;
using NNeuronFramework.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class PCANetworkDemo
    {
        public static void Demo()
        {
            PCANetwork network = PCANetwork.Create(2, 1);

            var dataSet = DataGenerator.GenerateDataSet2();

            var pca = new PCA(network, 0.7);

            PCATrainer trainer = new PCATrainer(network, 50, pca, 0.0000005);

            Normalizer2 normalizer = new Normalizer2();

            normalizer.Fit(dataSet.XList);

            var normalizedX = normalizer.Normalize(dataSet.XList);

            trainer.Fit(normalizedX);

            List<List<double>> convertedX = trainer.GetConvertedDim(normalizedX);

            List<List<double>> denormalX = normalizer.DeNormalize(convertedX);

            network.Display();

            Console.WriteLine("OLD VECTORS===>");
            Utils.DisplayListList(dataSet.XList);

            Console.WriteLine("OLD VECTORS(NORMALIZED)===>");
            Utils.DisplayListList(normalizedX);

            Console.WriteLine("NEW VECTORS(NORMALIZED)===>");
            Utils.DisplayListList(convertedX);

            Console.WriteLine("NEW VECTORS===>");
            Utils.DisplayListList(denormalX);
        }
    }
}
