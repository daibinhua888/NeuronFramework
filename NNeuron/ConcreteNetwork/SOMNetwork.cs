using System;
using System.Collections.Generic;
using System.Text;

namespace NNeuron.ConcreteNetwork
{
    public class SOMNetwork : NeuronNetwork
    {
        public int OutputNeuronCountPerDim { get; set; }
        public static SOMNetwork Create(int inputDim, int outputNeuronCountPerDim)
        {
            SOMNetwork network = new SOMNetwork();
            network.OutputNeuronCountPerDim = outputNeuronCountPerDim;

            var inputLayer = new InputLayer();
            inputLayer.InitNeurons(inputDim, false, ActivationFunction.LINEAR);
            network.InputLayer = inputLayer;

            var outputLayer = new OutputLayer();
            outputLayer.InitNeurons(outputNeuronCountPerDim* outputNeuronCountPerDim, false, ActivationFunction.SIGMOID);
            network.OutputLayer = outputLayer;

            network.ConstructConnectionsAndRandonizeWeights();

            return network;
        }
        
        public override void Calculate()
        {
            //赋输入层神经单元值
            for (var inputIndex = 0; inputIndex < inputs.Count; inputIndex++)
                this.InputLayer.Neurons[inputIndex].ActivationFunctionValue = this.inputs[inputIndex];

            //计算网络
            List<BaseLayer> allLayers = new List<BaseLayer>();
            allLayers.Add(this.InputLayer);
            allLayers.Add(this.OutputLayer);

            foreach (var layer in allLayers)
                foreach (var neuron in layer.Neurons)
                    neuron.Calculate();

            //获取网络输出值
            this.OutputLayer.Neurons.ForEach(neuron => {
                this.outputs.Add(neuron.ActivationFunctionValue);
            });
        }

        public override void ConstructConnectionsAndRandonizeWeights()
        {
            List<BaseLayer> allLayers = new List<BaseLayer>();
            allLayers.Add(this.InputLayer);
            allLayers.Add(this.OutputLayer);

            for (var layerIndex = 0; layerIndex < allLayers.Count - 1; layerIndex++)
            {
                foreach (var curLayerNeuron in allLayers[layerIndex].Neurons)
                {
                    foreach (var nextLayerNeuron in allLayers[layerIndex + 1].Neurons)
                    {
                        var connection = new NeuronConnection(curLayerNeuron, Utils.Utils.GenerateRandomValue());
                        nextLayerNeuron.IncomeConnections.Add(connection);
                    }
                }
            }

            //归一化weights
            NormalizeWeights();
        }

        private void NormalizeWeights()
        {
            //var totalWeights = this.GetTotalOutputLayerWeights();

            foreach (var neuron in this.OutputLayer.Neurons)
            {
                double totalWeights = 0;
                foreach (var connection in neuron.IncomeConnections)
                    totalWeights += connection.Weight * connection.Weight;

                totalWeights = Math.Sqrt(totalWeights);

                foreach (var connection in neuron.IncomeConnections)
                    connection.Weight = connection.Weight / totalWeights;
            }
        }

        //private double GetTotalOutputLayerWeights()
        //{
        //    double sum = 0;

        //    foreach (var neuron in this.OutputLayer.Neurons)
        //        foreach (var connection in neuron.IncomeConnections)
        //            sum += connection.Weight* connection.Weight;

        //    return sum;
        //}
    }
}
