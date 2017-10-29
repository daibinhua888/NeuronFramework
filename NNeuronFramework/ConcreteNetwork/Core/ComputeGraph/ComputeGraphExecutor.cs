using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.ComputeGraph
{
    public class ComputeGraphExecutor
    {
        public void Execute(Dictionary<string, double[]> inputs)
        {
            GraphNodeContext.GetNodes().ForEach(n => n.IsProcessed = false);
            GraphNodeContext.GetNodes().ForEach(n => n.IsWalked = false);
            GraphNodeContext.GetNodes().ForEach(n => n.ComputedResults = null);

            SetInputs2Node(inputs);

            while (HasUnProcessedNodes())
            {
                foreach (var node in GraphNodeContext.GetNodes())
                {
                    VisitNode(node);
                }
            }

            OutputSaveValueNodes();
        }

        private void OutputSaveValueNodes()
        {
            var nodes = GraphNodeContext.GetNodes().Where(n => n.IsMonitoring);
            foreach (var node in nodes)
            {
                Console.Write(node.NodeName + "==>");
                Utils.Utils.DisplayListList(node.ComputedResults);
            }
        }

        private void SetInputs2Node(Dictionary<string, double[]> inputs)
        {
            foreach (var k in inputs)
            {
                foreach (var n in GraphNodeContext.GetNodes())
                {
                    if (n.GetType().Equals(typeof(InputNode)))
                    {
                        InputNode iNode = n as InputNode;
                        if (iNode.NodeName == k.Key)
                            iNode.InputData = k.Value.ToList();
                    }
                }
            }
        }

        private bool HasUnProcessedNodes()
        {
            return GraphNodeContext.GetNodes().Count(n => !n.IsProcessed || !n.IsWalked) > 0;
        }

        private void VisitNode(BaseComputeGraphNode node)
        {
            node.ComputeResult();

            if (!node.IsProcessed)
                return;

            node.IsWalked = true;
        }
    }
}
