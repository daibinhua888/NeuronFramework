using NNeuronFramework.ConcreteNetwork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork
{
    public class LSTMNetwork: NeuronNetwork
    {
        private LSTMNetwork() { }

        public static LSTMNetwork Create(int hiddenNeuronCount, int inputDim)
        {
            var graph = new NetworkGraph();

            graph.DefineBlock("inputs", new NeuronBlock(inputDim));

            graph.DefineBlock("previousH", new NeuronBlock(hiddenNeuronCount), CopyValueFrom= "h(t)");
            graph.DefineBlock("previousC", new NeuronBlock(hiddenNeuronCount), CopyValueFrom = "c(t)_tanh");

            graph.DefineBlock("f_gate_W", new NeuronBlock(hiddenNeuronCount));
            graph.DefineBlock("f_gate_U", new NeuronBlock(hiddenNeuronCount));

            graph.DefineBlock("i_gate_W", new NeuronBlock(hiddenNeuronCount));
            graph.DefineBlock("i_gate_U", new NeuronBlock(hiddenNeuronCount));

            graph.DefineBlock("o_gate_W", new NeuronBlock(hiddenNeuronCount));
            graph.DefineBlock("o_gate_U", new NeuronBlock(hiddenNeuronCount));

            graph.DefineBlock("c_gate_W", new NeuronBlock(hiddenNeuronCount));
            graph.DefineBlock("c_gate_U", new NeuronBlock(hiddenNeuronCount));            

            graph.DefineBlock("+", new AddOperation());
            graph.DefineBlock("*", new MultiplyOperation());
            graph.DefineBlock("sigmoid", new SigmoidOperation());
            graph.DefineBlock("tanh", new TanhOperation());

            graph.DefineFullConnection("inputs", "f_gate_W");
            graph.DefineFullConnection("inputs", "i_gate_W");
            graph.DefineFullConnection("inputs", "o_gate_W");
            graph.DefineFullConnection("inputs", "c_gate_W");

            graph.DefineFullConnection("previousH", "f_gate_U");
            graph.DefineFullConnection("previousH", "i_gate_U");
            graph.DefineFullConnection("previousH", "o_gate_U");
            graph.DefineFullConnection("previousH", "c_gate_U");

            graph.AddFlow("f_value", new string[] { "f_gate_W", "f_gate_U" }, new string[] { "+", "sigmoid" });
            graph.AddFlow("i_value", new string[] { "i_gate_W", "i_gate_U" }, new string[] { "+", "sigmoid" });
            graph.AddFlow("o_value", new string[] { "o_gate_W", "o_gate_U" }, new string[] { "+", "sigmoid" });
            graph.AddFlow("c~_value", new string[] { "c_gate_W", "c_gate_U" }, new string[] { "+", "tanh" });

            graph.AddFlow("f_c_value", new string[] { "f_value", "previousC" }, new string[] {"*" });
            graph.AddFlow("i_c~_value", new string[] { "i_value", "c~_value" }, new string[] { "*" });

            graph.AddFlow("c(t)_tanh", new string[] { "f_c_value", "i_c~_value" }, new string[] { "+", "tanh" });

            graph.AddFlow("h(t)", new string[] { "c(t)_tanh", "o_value" }, new string[] { "*" });

            graph.Validate();

            LSTMNetwork network = new LSTMNetwork(graph);

            return network;
        }

        public override void Calculate()
        {
            throw new NotImplementedException();
        }

        public override void ConstructConnectionsAndRandonizeWeights()
        {
            throw new NotImplementedException();
        }
    }
}
