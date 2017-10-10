using System;
using System.Collections.Generic;
using System.Text;

namespace NNeuron.ConcreteNetwork
{
    public class PCANetwork:NeuronNetwork
    {
        private PCANetwork() { }

        public static PCANetwork Create(int inputDim, int targetDim) 
        {
            PCANetwork network = new PCANetwork();

            var inputLayer = new InputLayer();
            inputLayer.InitNeurons(inputDim, false, ActivationFunction.LINEAR);
            network.InputLayer = inputLayer;

            var outputLayer = new OutputLayer();
            outputLayer.InitNeurons(targetDim, false, ActivationFunction.SIGMOID);
            network.OutputLayer = outputLayer;

            network.ConstructConnectionsAndRandonizeWeights();

            return network;
        }

        public double CalculateTotalWeights()
        {
            double sum = 0;
            foreach (var n in this.OutputLayer.Neurons)
            {
                foreach (var c in n.IncomeConnections)
                {
                    sum += c.Weight* c.Weight;
                }
            }
            return Math.Sqrt(sum);
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
        }
    }
}
