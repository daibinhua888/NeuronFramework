using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NNeuronFramework.ConcreteNetwork
{
    public class BPNetwork: NeuronNetwork
    {
        private BPNetwork() { }

        public static BPNetwork Create(int inputLayerNeuronCount, int[] hiddenLayerNeuronCount, int outputLayerNeuronCount, ActivationFunction hiddenLayerActivationFunction= ActivationFunction.SIGMOID, ActivationFunction outputLayerActivationFunction = ActivationFunction.SIGMOID)
        {
            BPNetwork network = new BPNetwork();

            var inputLayer = new InputLayer();
            inputLayer.InitNeurons(inputLayerNeuronCount, false, ActivationFunction.LINEAR);
            network.InputLayer = inputLayer;

            network.HiddenLayers = new List<HiddenLayer>();
            if (hiddenLayerNeuronCount != null)
            {
                hiddenLayerNeuronCount.ToList().ForEach(count => {
                    network.HiddenLayers.Add(new HiddenLayer());
                });

                for (var i = 0; i < hiddenLayerNeuronCount.Length; i++)
                {
                    network.HiddenLayers[i].InitNeurons(hiddenLayerNeuronCount[i], true, hiddenLayerActivationFunction);
                }
            }

            var outputLayer = new OutputLayer();
            outputLayer.InitNeurons(outputLayerNeuronCount, true, outputLayerActivationFunction);
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
            allLayers.AddRange(this.HiddenLayers);
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
            allLayers.AddRange(this.HiddenLayers);
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

            allLayers.ForEach(layer => {
                layer.Neurons.ForEach(neuron => {
                    if (neuron.B != null)
                    {
                        neuron.B.Weight = Utils.Utils.GenerateRandomValue();
                        neuron.B.Value = 1;
                    }
                });
            });
        }
    }
}
