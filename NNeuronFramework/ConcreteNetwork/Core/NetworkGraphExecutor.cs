using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core
{
    public class NetworkGraphExecutor
    {
        //private List<GraphNode> graphHeaders;
        //private List<GraphNode> processingNodes;
        //private List<GraphNode> processedNodes=new List<GraphNode>();
        //private List<GraphNode> nextProcesseNodes = new List<GraphNode>();
        private List<GraphNode> allNodes;

        public NetworkGraphExecutor(/*List<GraphNode> graphHeaders, */List<GraphNode> allNodes)
        {
            //this.graphHeaders = graphHeaders;
            //this.processingNodes = new List<GraphNode>();
            this.allNodes = allNodes;
        }

        public void Execute()
        {
            allNodes.ForEach(n => n.IsProcessed = false);
            allNodes.ForEach(n => n.IsWalked = false);
            allNodes.ForEach(n => {
                n.Nexts.ForEach(next=> 
                {
                    next.OperationResult = null;
                });
            });

            //processingNodes.AddRange(this.graphHeaders);

            while (HasUnProcessedNodes())
            {
                //processedNodes.Clear();
                //nextProcesseNodes.Clear();

                foreach (var node in this.allNodes)
                {
                    VisitNode(node);
                }

                //处理节点信息
                //      remove processedNodes from processingNodes
                //      add nextProcesseNodes to processingNodes
                //processRemove:
                //for (var i = 0; i < this.processingNodes.Count; i++)
                //{
                //    bool found = false;

                //    foreach (var n in this.processedNodes)
                //        if (this.processingNodes[i] == n)
                //            found = true;

                //    if (found)
                //    {
                //        this.processingNodes.Remove(this.processingNodes[i]);
                //        goto processRemove;
                //    }
                //}

                //processingNodes.AddRange(nextProcesseNodes);
            }
        }

        private bool HasUnProcessedNodes()
        {
            return allNodes.Count(n=>!n.IsProcessed||!n.IsWalked)>0;
        }

        private void VisitNode(GraphNode node)
        {   
            //判断入参是否都DONE了
            //      没有则     EXIT
            //      有则       处理当前结点OP+PUSH输出到NEXTS节点

            if (!IsAllIncomingOK(node))
                return;

            ProcessOP_And_Push_To_Nexts(node);

            node.IsProcessed = true;
            node.IsWalked = true;
        }
        
        private void ProcessOP_And_Push_To_Nexts(GraphNode node)
        {
            //将当前node的DATA和各个nexts的op进行计算，保存在connection上

            //执行某些计算，先忽略，占位
            Console.WriteLine(string.Format("{1}({0})", node.NodeType.ToString(), node.Name));

            for(var i=0;i<node.Nexts.Count;i++)
            {
                var n = node.Nexts[i];

                n.OperationResult.Data = "some result";
            }
        }

        private bool IsAllIncomingOK(GraphNode node)
        {
            int hasResultCount = 0;

            foreach (var c in node.Previouses)
            {
                if (c.OperationResult.Data != null)
                    hasResultCount++;
            }

            return hasResultCount == node.Previouses.Count;
        }
    }
}
