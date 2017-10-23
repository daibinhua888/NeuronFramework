using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core
{
    public class NetworkGraphExecutor
    {
        private List<GraphNode> graphHeaders;
        private List<GraphNode> processingNodes;
        private List<GraphNode> processedNodes=new List<GraphNode>();
        private List<GraphNode> nextProcesseNodes = new List<GraphNode>();

        public NetworkGraphExecutor(List<GraphNode> graphHeaders)
        {
            this.graphHeaders = graphHeaders;
            this.processingNodes = new List<GraphNode>();
        }

        public void Execute()
        {
            processingNodes.AddRange(this.graphHeaders);

            while (HasUnProcessedNodes())
            {
                processedNodes.Clear();
                nextProcesseNodes.Clear();

                foreach (var node in this.processingNodes)
                {
                    VisitNode(node);
                }

                //处理节点信息

            }
        }

        private bool HasUnProcessedNodes()
        {
            return false;
        }

        private void VisitNode(GraphNode node)
        {
        }
    }
}
