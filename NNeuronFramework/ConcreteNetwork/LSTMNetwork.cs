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

        public void DisplayByDot()
        {
            this.graph.Display();
        }

        public void Run()
        {
            graph.Execute();
        }

        public LSTMNetwork(NetworkGraph graph)
        {
            this.graph = graph;
        }

        private LSTMNetwork() { }

        public static LSTMNetwork Create(int hiddenNeuronCount, int inputDim)
        {
            var graph = new NetworkGraph();

            var inputs =graph.Block("inputs", new NeuronsBlock(inputDim), "", true);
            var previousH=graph.Block("previousH", new NeuronsBlock(hiddenNeuronCount), "ht", true);
            var previousC=graph.Block("previousC", new NeuronsBlock(hiddenNeuronCount), "ct_tanh", true);

            var f_gate_W = graph.Block("f_gate_W", new NeuronsBlock(hiddenNeuronCount));
            var f_gate_U=graph.Block("f_gate_U", new NeuronsBlock(hiddenNeuronCount));

            var i_gate_W=graph.Block("i_gate_W", new NeuronsBlock(hiddenNeuronCount));
            var i_gate_U=graph.Block("i_gate_U", new NeuronsBlock(hiddenNeuronCount));

            var o_gate_W=graph.Block("o_gate_W", new NeuronsBlock(hiddenNeuronCount));
            var o_gate_U=graph.Block("o_gate_U", new NeuronsBlock(hiddenNeuronCount));

            var c_gate_W=graph.Block("c_gate_W", new NeuronsBlock(hiddenNeuronCount));
            var c_gate_U=graph.Block("c_gate_U", new NeuronsBlock(hiddenNeuronCount));

            var plus=graph.Operation("plus", new AddOperation());
            var multiply=graph.Operation("multiply", new MultiplyOperation());
            var sigmoid=graph.Operation("sigmoid", new SigmoidOperation());
            var tanh=graph.Operation("tanh", new TanhOperation());
            var fc=graph.Operation("fc", new FullConnectionOperation());

            var f_gate_W_fc_out=graph.Connect("f_gate_W_fc_out", new GraphNode[] { inputs, f_gate_W }, new GraphNode[] { fc });
            var i_gate_W_fc_out = graph.Connect("i_gate_W_fc_out", new GraphNode[] { inputs, i_gate_W }, new GraphNode[] { fc });
            var o_gate_W_fc_out = graph.Connect("o_gate_W_fc_out", new GraphNode[] { inputs, o_gate_W }, new GraphNode[] { fc });
            var c_gate_W_fc_out = graph.Connect("c_gate_W_fc_out", new GraphNode[] { inputs, c_gate_W }, new GraphNode[] { fc });

            var f_gate_U_fc_out = graph.Connect("f_gate_U_fc_out", new GraphNode[] { previousH, f_gate_U }, new GraphNode[] { fc });
            var i_gate_U_fc_out = graph.Connect("i_gate_U_fc_out", new GraphNode[] { previousH, i_gate_U }, new GraphNode[] { fc });
            var o_gate_U_fc_out = graph.Connect("o_gate_U_fc_out", new GraphNode[] { previousH, o_gate_U }, new GraphNode[] { fc });
            var c_gate_U_fc_out = graph.Connect("c_gate_U_fc_out", new GraphNode[] { previousH, c_gate_U }, new GraphNode[] { fc });

            var f_value = graph.Connect("f_value", new GraphNode[] { f_gate_W_fc_out, f_gate_U_fc_out }, new GraphNode[] { plus, sigmoid });
            var i_value = graph.Connect("i_value", new GraphNode[] { i_gate_W_fc_out, i_gate_U_fc_out }, new GraphNode[] { plus, sigmoid });
            var o_value = graph.Connect("o_value", new GraphNode[] { o_gate_W_fc_out, o_gate_U_fc_out }, new GraphNode[] { plus, sigmoid });
            var c1_value = graph.Connect("c1_value", new GraphNode[] { c_gate_W_fc_out, c_gate_U_fc_out }, new GraphNode[] { plus, tanh });

            var f_c_value = graph.Connect("f_c_value", new GraphNode[] { f_value, previousC }, new GraphNode[] {multiply });
            var i_c1_value = graph.Connect("i_c1_value", new GraphNode[] { i_value, c1_value }, new GraphNode[] { multiply });

            var ct_tanh = graph.Connect("ct_tanh", new GraphNode[] { f_c_value, i_c1_value }, new GraphNode[] { plus, tanh }, true);

            var ht = graph.Connect("ht", new GraphNode[] { ct_tanh, o_value }, new GraphNode[] { multiply }, true);

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
