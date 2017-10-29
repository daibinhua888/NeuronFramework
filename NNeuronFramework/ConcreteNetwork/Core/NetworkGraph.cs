using NNeuronFramework.ConcreteNetwork.Core.Descriptors;
using NNeuronFramework.ConcreteNetwork.Core.Operations;
using NNeuronFramework.Configs;
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
        private List<GraphNode> allNodes = new List<GraphNode>();
        
        private NetworkGraphExecutor executor;

        public void Execute(double[] inputs)
        {
            executor.Execute(inputs);
        }

        public void Compile()
        {
            foreach (var node in this.allNodes)
            {
                if (string.IsNullOrEmpty(node.CopyValueFrom))
                    continue;

                List<double> rndValues = new List<double>();

                for (var index = 0; index < node.InputsCount; index++)
                    rndValues.Add(Utils.Utils.GenerateRandomValue());

                node.Value = rndValues.ToArray();
            }

            executor = new NetworkGraphExecutor(/*this.graphHeaders, */this.allNodes);
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

            foreach (var node in graphHeaders)
                lst.AddRange(node.ParseDirections());
            
            foreach (var node in savableNodes)
                lst.Add(string.Format("\"{0}\"[color=orange]", node.Name));

            foreach (var node in graphHeaders)
                lst.Add(string.Format("\"{0}\"[color=lightblue]", node.Name));

            foreach (var node in allNodes)
                if(!graphHeaders.Contains(node))
                    if (node.Value!=null)
                        lst.Add(string.Format("\"{0}\"[color=yellow]", node.Name));

            return lst;
        }

        public GraphNode Connect(string name, GraphNode[] nodes2Merge, GraphNode[] nextOps, bool isSavable = false)
        {
            GraphNode lastNode = null;

            bool isFCNeurons = false;
            if (nodes2Merge.Length == 2)
            {
                var neuron = nodes2Merge.ToList().Find(o => o.NodeType == NodeType.NeuronsLayer);
                var input = nodes2Merge.ToList().Find(o => o.NodeType != NodeType.NeuronsLayer);

                if (neuron != null)
                {
                    SetConnection(input, neuron, new CopyOperation());
                    isFCNeurons = true;
                    lastNode = neuron;
                }
            }

            if (!isFCNeurons)
            {
                GraphNode mergeNode = new GraphNode();

                foreach (var n in nodes2Merge)
                {
                    SetConnection(n, mergeNode, new CopyOperation());
                }
                lastNode = mergeNode;
                allNodes.Add(mergeNode);
            }

            foreach (var nextOp in nextOps)
            {
                var opGraphNode = new GraphNode();
                opGraphNode.CopyValueFrom = nextOp.CopyValueFrom;
                opGraphNode.IsProcessed = nextOp.IsProcessed;
                opGraphNode.Name = Guid.NewGuid().ToString("N");
                opGraphNode.NodeType = nextOp.NodeType;
                opGraphNode.Operation = null;
                opGraphNode.Value = nextOp.Value;

                SetConnection(lastNode, opGraphNode, nextOp.Operation);

                //判断是否FC，需要初始化，根据输入节点
                if (nextOp.Operation.GetType() == typeof(FullConnectionOperation))
                {
                    //全连接操作，预先进行初始化权重
                    lastNode.IsWeightsNode = true;
                    lastNode.RandomizeWeights();
                }


                lastNode = opGraphNode;

                allNodes.Add(lastNode);
            }

            lastNode.Name = name;
            lastNode.IsTempNode = false;

            if (isSavable)
                savableNodes.Add(lastNode);

            return lastNode;
        }

        

        public GraphNode Block(string name, int parameterCount, string copyValueFrom="", bool isStartable=false, bool isSavable=false, bool isRandomizeInit=false)
        {
            GraphNode node = new GraphNode();

            node.Name = name;
            node.IsTempNode = false;
            node.NodeType = NodeType.SaveValueNode;
            node.Operation = null;
            node.IsProcessed = false;
            node.CopyValueFrom = copyValueFrom;
            node.IsSavable = isSavable;
            node.IsStartable = isStartable;
            node.InputsCount = parameterCount;

            if (isStartable)
                graphHeaders.Add(node);

            if (isSavable)
                savableNodes.Add(node);

            allNodes.Add(node);

            return node;
        }

        public GraphNode NeuronLayer(string name, int neuronCount)
        {
            GraphNode node = new GraphNode();

            node.Name = name;
            node.IsTempNode = false;
            node.NodeType = NodeType.NeuronsLayer;
            node.Operation = null;
            node.IsProcessed = false;
            node.CopyValueFrom = string.Empty;
            node.IsSavable = true;
            node.IsStartable = false;
            node.NeuronsCount = neuronCount;

            allNodes.Add(node);

            return node;
        }

        public GraphNode Operation(string opName, Operation operation)
        {
            GraphNode node = new GraphNode();

            node.Name = opName;
            node.IsTempNode = false;
            node.NodeType = NodeType.Operation;
            node.Operation = operation;
            node.IsProcessed = false;
            node.CopyValueFrom = string.Empty;

            allNodes.Add(node);

            return node;
        }

        private void SetConnection(GraphNode node1, GraphNode node2, Operation op)
        {
            var resultPlaceHolder = new GraphNodeConnectorResult();

            {
                var connector = new GraphNodeConnector();

                connector.Node = node2;
                connector.Operation = op;
                connector.OperationResult = resultPlaceHolder;

                node1.Nexts.Add(connector);
            }

            {
                var connector = new GraphNodeConnector();

                connector.Node = node1;
                connector.Operation = op;
                connector.OperationResult = resultPlaceHolder;

                node2.Previouses.Add(connector);
            }
        }
    }
}
