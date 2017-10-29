using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.ComputeGraph
{
    public static class GraphNodeContext
    {
        private static List<BaseComputeGraphNode> allNodes = new List<BaseComputeGraphNode>();

        public static void AddNode(BaseComputeGraphNode node)
        {
            if(!allNodes.Contains(node))
                allNodes.Add(node);
        }

        public static BaseComputeGraphNode GetNode(string nodeName)
        {
            return allNodes.Find(n=>n.NodeName==nodeName);
        }

        public static List<BaseComputeGraphNode> GetNodes()
        {
            return allNodes;
        }
    }
}
