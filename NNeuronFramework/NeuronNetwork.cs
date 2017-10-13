using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework
{
    public abstract class NeuronNetwork
    {
        public InputLayer InputLayer;
        public OutputLayer OutputLayer;
        public List<HiddenLayer> HiddenLayers;

        protected List<double> inputs=new List<double>();
        protected List<double> outputs=new List<double>();
        
        public void SetInput(List<double> inputs)
        {
            this.inputs = inputs;
            this.outputs = new List<double>();
        }

        public abstract void Calculate();

        public abstract void ConstructConnectionsAndRandonizeWeights();

        public List<double> GetOutput()
        {
            return this.outputs;
        }
        
        public void Display()
        {
            List<BaseLayer> allLayers = new List<BaseLayer>();

            if(this.InputLayer!=null)
                allLayers.Add(this.InputLayer);

            if(this.HiddenLayers!=null)
                allLayers.AddRange(this.HiddenLayers);

            if(this.OutputLayer!=null)
                allLayers.Add(this.OutputLayer);

            for (var layerIndex = 0; layerIndex < allLayers.Count ; layerIndex++)
            {
                Console.WriteLine("layer #"+layerIndex);
                for (var neuronIndex = 0; neuronIndex < allLayers[layerIndex].Neurons.Count; neuronIndex++)
                {
                    Console.WriteLine("     neuron #" + neuronIndex+"("+ allLayers[layerIndex].Neurons[neuronIndex].ActivationFunctionValue + ")");
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

                    Console.WriteLine("         ErrorValue:"+ allLayers[layerIndex].Neurons[neuronIndex].ErrorValue);
                }
            }
        }        
    }
}
