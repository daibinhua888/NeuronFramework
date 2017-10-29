using NNeuronFramework.ConcreteNetwork.Core;
using NNeuronFramework.ConcreteNetwork.Core.ComputeGraph;
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
        private ComputeGraph graph1;

        public void DisplayByDot()
        {
            this.graph.Display();
        }

        public void DisplayByDot2()
        {
            this.graph1.Display();
        }

        public void Run(double[] inputs)
        {
            graph.Execute(inputs);
        }

        public void Run2(Dictionary<string, double[]> inputs)
        {
            graph1.Execute(inputs);
        }

        public LSTMNetwork(NetworkGraph graph)
        {
            this.graph = graph;
        }

        private LSTMNetwork() { }

        public LSTMNetwork(ComputeGraph graph1)
        {
            this.graph1 = graph1;
        }

        public static LSTMNetwork Create(int hiddenNeuronCount, int inputDim)
        {
            var graph = new NetworkGraph();

            var inputs =graph.Block("inputs", inputDim, "", true);
            var previousH=graph.Block("previousH", hiddenNeuronCount, "ht", true);
            var previousC=graph.Block("previousC", hiddenNeuronCount, "ct_tanh", true);

            var f_gate_W = graph.NeuronLayer("f_gate_W", hiddenNeuronCount);
            var f_gate_U=graph.NeuronLayer("f_gate_U", hiddenNeuronCount);

            var i_gate_W=graph.NeuronLayer("i_gate_W", hiddenNeuronCount);
            var i_gate_U=graph.NeuronLayer("i_gate_U", hiddenNeuronCount);

            var o_gate_W=graph.NeuronLayer("o_gate_W", hiddenNeuronCount);
            var o_gate_U=graph.NeuronLayer("o_gate_U", hiddenNeuronCount);

            var c_gate_W=graph.NeuronLayer("c_gate_W", hiddenNeuronCount);
            var c_gate_U=graph.NeuronLayer("c_gate_U", hiddenNeuronCount);

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

            ct_tanh.IsMonitoringNode = true;
            ht.IsMonitoringNode = true;

            graph.Compile();

            LSTMNetwork network = new LSTMNetwork(graph);

            return network;
        }

        public static LSTMNetwork Create2(int hiddenNeuronCount, int inputDim)
        {
            var graph = new ComputeGraph();

            var inputs = graph.DeclareInput("inputs", inputDim);
            var previousH = graph.DeclareCopyFrom("previousH", hiddenNeuronCount, "ht");
            var previousC = graph.DeclareCopyFrom("previousC", hiddenNeuronCount, "ct_tanh");

            var f_gate_W = graph.DeclareFC("f_gate_W", inputDim, hiddenNeuronCount, inputs);
            var f_gate_U = graph.DeclareFC("f_gate_U", hiddenNeuronCount, hiddenNeuronCount, previousH);

            var i_gate_W = graph.DeclareFC("i_gate_W", inputDim, hiddenNeuronCount, inputs);
            var i_gate_U = graph.DeclareFC("i_gate_U", hiddenNeuronCount, hiddenNeuronCount, previousH);

            var o_gate_W = graph.DeclareFC("o_gate_W", inputDim, hiddenNeuronCount, inputs);
            var o_gate_U = graph.DeclareFC("o_gate_U", hiddenNeuronCount, hiddenNeuronCount, previousH);

            var c_gate_W = graph.DeclareFC("c_gate_W", inputDim, hiddenNeuronCount, inputs);
            var c_gate_U = graph.DeclareFC("c_gate_U", hiddenNeuronCount, hiddenNeuronCount, previousH);
            
            var f_value = graph.DeclarePlus("f_value", f_gate_W, f_gate_U);
            var i_value = graph.DeclarePlus("i_value", i_gate_W, i_gate_U);
            var o_value = graph.DeclarePlus("o_value", o_gate_W, o_gate_U);
            var c1_value = graph.DeclarePlus("c1_value", c_gate_W, c_gate_U);

            var f_sigmoid_value = graph.DeclareSigmoid("f_sigmoid_value", f_value);
            var i_sigmoid_value = graph.DeclareSigmoid("i_sigmoid_value", i_value);
            var o_sigmoid_value = graph.DeclareSigmoid("o_sigmoid_value", o_value);
            var c1_tanh_value = graph.DeclareTanh("c1_tanh_value", c1_value);

            var f_c_value = graph.DeclareMultiply("f_c_value", f_sigmoid_value, previousC);
            var i_c1_value = graph.DeclareMultiply("i_c1_value", i_sigmoid_value, c1_tanh_value);

            var f_c_i_c1_plus = graph.DeclarePlus("f_c_i_c1_plus", f_c_value, i_c1_value);
            var ct_tanh = graph.DeclareTanh("ct_tanh", f_c_i_c1_plus);

            var ht = graph.DeclareMultiply("ht", ct_tanh, o_sigmoid_value);

            ct_tanh.ResultsDim = hiddenNeuronCount;
            ht.ResultsDim = hiddenNeuronCount;

            ct_tanh.IsMonitoring = true;
            ht.IsMonitoring = true;

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
