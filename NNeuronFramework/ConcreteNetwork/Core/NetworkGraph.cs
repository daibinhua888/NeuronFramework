using NNeuronFramework.ConcreteNetwork.Core.Descriptors;
using NNeuronFramework.ConcreteNetwork.Core.Operations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core
{
    public class NetworkGraph
    {
        private List<GraphNode> savableNodes = new List<GraphNode>();
        private List<GraphNode> graphHeaders = new List<GraphNode>();
        private NetworkGraphExecutor executor;

        public void Execute()
        {
            executor.Execute();
        }

        private List<GraphNode> processingNodes = new List<GraphNode>();

        public void Compile()
        {
            executor = new NetworkGraphExecutor(this.graphHeaders);
        }

        public void Display()
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
            p.StartInfo.FileName = @"C:\Program Files (x86)\Graphviz2.38\bin\dot.exe";
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
            p.WaitForExit();
        }

        private List<string> ParseDirections()
        {
            List<string> lst = new List<string>();

            foreach (var node in graphHeaders)
                lst.AddRange(node.ParseDirections());
            
            foreach (var node in savableNodes)
                lst.Add(string.Format("\"{0}\"[color=orange]", node.Name));

            foreach (var node in graphHeaders)
                lst.Add(string.Format("\"{0}\"[color=lightblue]", node.Name));

            return lst;
        }

        public GraphNode Connect(string name, GraphNode[] nodes2Merge, GraphNode[] nextOps, bool isSavable = false)
        {
            GraphNode mergeNode = new GraphNode();

            foreach (var n in nodes2Merge)
            {
                {
                    var connector = new GraphNodeConnector();

                    connector.Node = mergeNode;
                    connector.Operation = new CopyOperation();

                    n.Nexts.Add(connector);
                }

                {
                    var connector = new GraphNodeConnector();

                    connector.Node = n;
                    connector.Operation = new CopyOperation();

                    mergeNode.Previouses.Add(connector);
                }
            }

            var lastNode = mergeNode;

            foreach (var nextOp in nextOps)
            {
                var opGraphNode = new GraphNode();
                opGraphNode.Block = nextOp.Block;
                opGraphNode.CopyValueFrom = nextOp.CopyValueFrom;
                opGraphNode.IsProcessed = nextOp.IsProcessed;
                opGraphNode.Name = Guid.NewGuid().ToString("N");
                opGraphNode.NodeType = NodeType.SaveValueNode;
                opGraphNode.Operation = null;

                {
                    var connector = new GraphNodeConnector();

                    connector.Node = opGraphNode;
                    connector.Operation = nextOp.Operation;

                    lastNode.Nexts.Add(connector);
                }

                {
                    var connector = new GraphNodeConnector();

                    connector.Node = lastNode;
                    connector.Operation = new CopyOperation();

                    opGraphNode.Previouses.Add(connector);
                }

                lastNode = opGraphNode;
            }

            lastNode.Name = name;

            if (isSavable)
                savableNodes.Add(lastNode);

            return lastNode;
        }

        public GraphNode Block(string name, NeuronsBlock neuronsBlock, string copyValueFrom="", bool isStartable=false, bool isSavable=false)
        {
            GraphNode node = new GraphNode();

            node.Name = name;
            node.Block = neuronsBlock;
            node.NodeType = NodeType.BlockNode;
            node.Operation = null;
            node.IsProcessed = false;
            node.CopyValueFrom = copyValueFrom;
            node.IsSavable = isSavable;

            if (isStartable)
                graphHeaders.Add(node);

            if (isSavable)
                savableNodes.Add(node);

            return node;
        }

        public GraphNode Operation(string opName, Operation operation)
        {
            GraphNode node = new GraphNode();

            node.Name = opName;
            node.Block = null;
            node.NodeType = NodeType.Operation;
            node.Operation = operation;
            node.IsProcessed = false;
            node.CopyValueFrom = string.Empty;

            return node;
        }
    }
}
