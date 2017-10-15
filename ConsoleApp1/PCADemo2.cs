using NNeuronFramework;
using NNeuronFramework.Algorithm;
using NNeuronFramework.ConcreteNetwork;
using NNeuronFramework.ConcreteTrainer;
using NNeuronFramework.OutputConverters;
using NNeuronFramework.Utils;
using NNeuronFramework.ValidationUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class PCADemo2
    {
        public static void Demo()
        {
            PCANetwork network = PCANetwork.Create(64, 8);

            var dataSet = DataGenerator.GenerateDataSet1();

            var pca = new PCA(network, 0.7);

            PCATrainer trainer = new PCATrainer(network, 50, pca, 0.00000005);

            Normalizer3 normalizer = new Normalizer3();
            //Normalizer normalizer = new Normalizer();

            normalizer.Fit(dataSet.XList);

            var normalizedX = normalizer.Normalize(dataSet.XList);


            Utils.DisplayListList(dataSet.XList);
            Console.WriteLine("--------------------------");
            Utils.DisplayListList(normalizedX);
            
            //Utils.DisplayListList(normalizer.DeNormalize(normalizedX));

            trainer.Fit(normalizedX);

            List<List<double>> convertedX = trainer.GetConvertedDim(normalizedX);

            //convertedX=normalizer.DeNormalize(convertedX);

            BPNetwork bpNetwork = BPNetwork.Create(8, new int[] { 4/*, 4, 3*/ }, 3, ActivationFunction.SIGMOID, ActivationFunction.SIGMOID);

            var bp = new BP(bpNetwork, 0.6);
            BPTrainer bpTrainer = new BPTrainer(bpNetwork, 2000, bp);

            bpTrainer.Fit(convertedX, dataSet.YList);

            List<List<double>> predicted_ys = bpTrainer.Predict(convertedX);

            CorrectCalculator correctCalculator = new CorrectCalculator();

            var convertedPredictValue = NeuronOutputConverter.OrderInteger(predicted_ys);

            double correct = correctCalculator.Calculate(convertedPredictValue, dataSet.YList);

            Console.WriteLine("Score: {0}%", correct * 100);
        }
    }
}
