using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFramework
{
    public class NeuronNetwork
    {
        private NeuronNetwork()
        { }

        private InputLayer InputLayer;
        private OutputLayer OutputLayer;
        private List<HiddenLayer> HiddenLayers;

        public static NeuronNetwork Create(int inputLayerNeuronCount, int[] hiddenLayerNeuronCount, int outputLayerNeuronCount)
        {
            NeuronNetwork network = new NeuronNetwork();

            var inputLayer= new InputLayer();
            inputLayer.InitNeurons(inputLayerNeuronCount, false);
            network.InputLayer = inputLayer;

            network.HiddenLayers = new List<HiddenLayer>();
            if (hiddenLayerNeuronCount != null)
            {
                hiddenLayerNeuronCount.ToList().ForEach(count=> {
                    network.HiddenLayers.Add(new HiddenLayer());
                });

                for (var i = 0; i < hiddenLayerNeuronCount.Length; i++)
                {
                    network.HiddenLayers[i].InitNeurons(hiddenLayerNeuronCount[i]);
                }
            }

            var outputLayer = new OutputLayer();
            outputLayer.InitNeurons(outputLayerNeuronCount);
            network.OutputLayer = outputLayer;

            network.RandonWeights();

            return network;
        }

        private void RandonWeights()
        {
            List<BaseLayer> allLayers = new List<BaseLayer>();
            allLayers.Add(this.InputLayer);
            allLayers.AddRange(this.HiddenLayers);
            allLayers.Add(this.OutputLayer);

            allLayers.First().Neurons.ForEach(n=> {
                n.IncomeConnections.Add(new NeuronConnection(null, 1));
            });

            for (var layerIndex = 0; layerIndex < allLayers.Count-1; layerIndex++)
            {
                foreach (var curLayerNeuron in allLayers[layerIndex].Neurons)
                {
                    foreach (var nextLayerNeuron in allLayers[layerIndex+1].Neurons)
                    {
                        var connection = new NeuronConnection(curLayerNeuron, Utils.GenerateRandomValue());
                        nextLayerNeuron.IncomeConnections.Add(connection);
                    }
                }
            }

            allLayers.ForEach(layer=> {
                layer.Neurons.ForEach(neuron=> {
                    if (neuron.B != null)
                    {
                        neuron.B.Weight = Utils.GenerateRandomValue();
                        neuron.B.Value = 1;
                    }
                });
            });
        }

        public void Display()
        {
            List<BaseLayer> allLayers = new List<BaseLayer>();
            allLayers.Add(this.InputLayer);
            allLayers.AddRange(this.HiddenLayers);
            allLayers.Add(this.OutputLayer);

            for (var layerIndex = 0; layerIndex < allLayers.Count ; layerIndex++)
            {
                Console.WriteLine("layer #"+layerIndex);
                for (var neuronIndex = 0; neuronIndex < allLayers[layerIndex].Neurons.Count; neuronIndex++)
                {
                    Console.WriteLine("     neuron #" + neuronIndex);
                    Console.Write("         weights:");
                    foreach (var connection in allLayers[layerIndex].Neurons[neuronIndex].IncomeConnections)
                        Console.Write(string.Format("{0} ", connection.Weight));
                    Console.WriteLine();
                    if (allLayers[layerIndex].Neurons[neuronIndex].B != null)
                    {
                        Console.Write("         b:");
                        Console.Write(allLayers[layerIndex].Neurons[neuronIndex].B.Weight);
                        Console.WriteLine();
                    }
                }
            }
        }        
    }
}
