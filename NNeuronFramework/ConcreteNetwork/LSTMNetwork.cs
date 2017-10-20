using NNeuronFramework.ConcreteNetwork.Core;
using NNeuronFramework.ConcreteNetwork.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork
{
    public class LSTMNetwork: NeuronNetwork
    {
        private NetworkGraph graph;

        public LSTMNetwork(NetworkGraph graph)
        {
            this.graph = graph;
        }

        private LSTMNetwork() { }

        public static LSTMNetwork Create(int hiddenNeuronCount, int inputDim)
        {
            var graph = new NetworkGraph();

            graph.DefineBlock("inputs", new NeuronsBlock(inputDim));

            graph.DefineBlock("previousH", new NeuronsBlock(hiddenNeuronCount), "h(t)");
            graph.DefineBlock("previousC", new NeuronsBlock(hiddenNeuronCount), "c(t)_tanh");

            graph.DefineBlock("f_gate_W", new NeuronsBlock(hiddenNeuronCount));
            graph.DefineBlock("f_gate_U", new NeuronsBlock(hiddenNeuronCount));

            graph.DefineBlock("i_gate_W", new NeuronsBlock(hiddenNeuronCount));
            graph.DefineBlock("i_gate_U", new NeuronsBlock(hiddenNeuronCount));

            graph.DefineBlock("o_gate_W", new NeuronsBlock(hiddenNeuronCount));
            graph.DefineBlock("o_gate_U", new NeuronsBlock(hiddenNeuronCount));

            graph.DefineBlock("c_gate_W", new NeuronsBlock(hiddenNeuronCount));
            graph.DefineBlock("c_gate_U", new NeuronsBlock(hiddenNeuronCount));            

            graph.DefineOperation("+", new AddOperation());
            graph.DefineOperation("*", new MultiplyOperation());
            graph.DefineOperation("sigmoid", new SigmoidOperation());
            graph.DefineOperation("tanh", new TanhOperation());
            graph.DefineOperation("fc", new FullConnectionOperation());

            graph.AddFlow("f_gate_W_fc_out", new string[] { "inputs", "f_gate_W" }, new string[] { "fc" });
            graph.AddFlow("i_gate_W_fc_out", new string[] { "inputs", "i_gate_W" }, new string[] { "fc" });
            graph.AddFlow("o_gate_W_fc_out", new string[] { "inputs", "o_gate_W" }, new string[] { "fc" });
            graph.AddFlow("c_gate_W_fc_out", new string[] { "inputs", "c_gate_W" }, new string[] { "fc" });

            graph.AddFlow("f_gate_U_fc_out", new string[] { "previousH", "f_gate_U" }, new string[] { "fc" });
            graph.AddFlow("i_gate_U_fc_out", new string[] { "previousH", "i_gate_U" }, new string[] { "fc" });
            graph.AddFlow("o_gate_U_fc_out", new string[] { "previousH", "o_gate_U" }, new string[] { "fc" });
            graph.AddFlow("c_gate_U_fc_out", new string[] { "previousH", "c_gate_U" }, new string[] { "fc" });

            graph.AddFlow("f_value", new string[] { "f_gate_W_fc_out", "f_gate_U_fc_out" }, new string[] { "+", "sigmoid" });
            graph.AddFlow("i_value", new string[] { "i_gate_W_fc_out", "i_gate_U_fc_out" }, new string[] { "+", "sigmoid" });
            graph.AddFlow("o_value", new string[] { "o_gate_W_fc_out", "o_gate_U_fc_out" }, new string[] { "+", "sigmoid" });
            graph.AddFlow("c~_value", new string[] { "c_gate_W_fc_out", "c_gate_U_fc_out" }, new string[] { "+", "tanh" });

            graph.AddFlow("f_c_value", new string[] { "f_value", "previousC" }, new string[] {"*" });
            graph.AddFlow("i_c~_value", new string[] { "i_value", "c~_value" }, new string[] { "*" });

            graph.AddFlow("c(t)_tanh", new string[] { "f_c_value", "i_c~_value" }, new string[] { "+", "tanh" });

            graph.AddFlow("h(t)", new string[] { "c(t)_tanh", "o_value" }, new string[] { "*" });

            graph.Compile();

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
