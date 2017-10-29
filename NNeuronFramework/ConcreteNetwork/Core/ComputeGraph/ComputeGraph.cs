using NNeuronFramework.Configs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.ComputeGraph
{
    public class ComputeGraph
    {
        private ComputeGraphExecutor executor;

        public InputNode DeclareInput(string name, int vectorSize, string copyValueFrom="")
        {
            InputNode node = new InputNode();

            node.NodeName = name;
            node.ResultsDim = vectorSize;

            GraphNodeContext.AddNode(node);

            return node;
        }

        public CopyValueFromNode DeclareCopyFrom(string name, int neuronsSize, string copyFromNodeName)
        {
            CopyValueFromNode node = new CopyValueFromNode();

            node.NodeName = name;
            node.ResultsDim = neuronsSize;
            node.TargetNodeName = copyFromNodeName;

            GraphNodeContext.AddNode(node);

            return node;
        }
        
        public FCNode DeclareFC(string name, int inputDim, int neuronsSize, BaseComputeGraphNode inputNode)
        {
            FCNode node = new FCNode(inputDim, neuronsSize);

            node.NodeName = name;

            foreach (var n in new BaseComputeGraphNode[] { inputNode })
            {
                node.Prevs.Add(n);
                n.Nexts.Add(node);
            }

            GraphNodeContext.AddNode(node);

            return node;
        }

        public PlusNode DeclarePlus(string name, params BaseComputeGraphNode[] parents)
        {
            PlusNode node = new PlusNode();

            node.NodeName = name;
            foreach (var n in parents)
            {
                node.Prevs.Add(n);
                n.Nexts.Add(node);
            }

            GraphNodeContext.AddNode(node);

            return node;
        }

        public SigmoidNode DeclareSigmoid(string name, params BaseComputeGraphNode[] parents)
        {
            SigmoidNode node = new SigmoidNode();

            node.NodeName = name;
            foreach (var n in parents)
            {
                node.Prevs.Add(n);
                n.Nexts.Add(node);
            }

            GraphNodeContext.AddNode(node);

            return node;
        }

        public MultiplyNode DeclareMultiply(string name, params BaseComputeGraphNode[] parents)
        {
            MultiplyNode node = new MultiplyNode();

            node.NodeName = name;
            foreach (var n in parents)
            {
                node.Prevs.Add(n);
                n.Nexts.Add(node);
            }

            GraphNodeContext.AddNode(node);

            return node;
        }

        public TanhNode DeclareTanh(string name, params BaseComputeGraphNode[] parents)
        {
            TanhNode node = new TanhNode();

            node.NodeName = name;
            foreach (var n in parents)
            {
                node.Prevs.Add(n);
                n.Nexts.Add(node);
            }

            GraphNodeContext.AddNode(node);

            return node;
        }

        public void Compile()
        {
            executor = new ComputeGraphExecutor();
        }
        public void Execute(Dictionary<string, double[]> inputs)
        {   
            executor.Execute(inputs);
        }












        internal void Display()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"digraph G{");

            List<string> directions = ParseDirections();
            foreach (var d in directions)
                sb.AppendLine(d);

            sb.AppendLine(@"}");

            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lstm.dot");

            File.WriteAllText(filePath, sb.ToString());

            Process p = new Process();
            p.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            p.StartInfo.FileName = Settings.DotExePath;
            p.StartInfo.Arguments = "-Tpng lstm.dot -o sample.png";
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            Console.WriteLine("started");
            p.WaitForExit();
            Console.WriteLine("exited");

            p = new Process();
            p.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            p.StartInfo.FileName = "sample.png";
            p.StartInfo.CreateNoWindow = false;
            p.Start();
        }

        private List<string> ParseDirections()
        {
            List<string> lst = new List<string>();

            foreach (var node in GraphNodeContext.GetNodes())
                lst.AddRange(ParseDirections(node));

            //foreach (var node in savableNodes)
            //    lst.Add(string.Format("\"{0}\"[color=orange]", node.Name));

            //foreach (var node in graphHeaders)
            //    lst.Add(string.Format("\"{0}\"[color=lightblue]", node.Name));

            //foreach (var node in allNodes)
            //    if (!graphHeaders.Contains(node))
            //        if (node.Value != null)
            //            lst.Add(string.Format("\"{0}\"[color=yellow]", node.Name));

            return lst;
        }

        private Dictionary<string, bool> processedParses = new Dictionary<string, bool>();
        private List<string> ParseDirections(BaseComputeGraphNode node)
        {
            List<string> results = new List<string>();

            foreach (var n in node.Prevs)
            {
                var key = string.Format("{0}-->{1}", n.NodeName, node.NodeName);
                if (!processedParses.ContainsKey(key))
                {
                    results.Add(string.Format("\"{0}\"->\"{1}\"", n.NodeName, node.NodeName));
                    processedParses.Add(key, true);
                }
            }

            foreach (var n in node.Nexts)
            {
                var key = string.Format("{1}-->{0}", n.NodeName, node.NodeName);
                if (!processedParses.ContainsKey(key))
                {
                    results.Add(string.Format("\"{0}\"->\"{1}\"", node.NodeName, n.NodeName));
                    processedParses.Add(key, true);
                }
            }

            return results;
        }
    }
}
