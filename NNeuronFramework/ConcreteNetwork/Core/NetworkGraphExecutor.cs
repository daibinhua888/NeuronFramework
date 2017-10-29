using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core
{
    public class NetworkGraphExecutor
    {
        private List<GraphNode> allNodes;

        public NetworkGraphExecutor(List<GraphNode> allNodes)
        {
            this.allNodes = allNodes;
        }

        public void Execute(double[] inputs)
        {
            SetInputs2Node(inputs);

            allNodes.ForEach(n => n.IsProcessed = false);
            allNodes.ForEach(n => n.IsWalked = false);
            allNodes.ForEach(n => {
                n.Nexts.ForEach(next=> 
                {
                    next.OperationResult.Data = null;
                });
            });

            while (HasUnProcessedNodes())
            {
                foreach (var node in this.allNodes)
                {
                    VisitNode(node);
                }
            }

            OutputSaveValueNodes();
        }

        private void OutputSaveValueNodes()
        {
            var saveNodes = this.allNodes.Where(n=>n.IsMonitoringNode);
            foreach (var node in saveNodes)
            {
                Console.Write(node.Name+"==>");
                Utils.Utils.DisplayListList(node.Value.ToList());
            }
        }

        private void SetInputs2Node(double[] inputs)
        {
            foreach (var n in this.allNodes)
            {
                if (n.IsStartable && n.Name == "inputs")//后续修改
                    n.Value = inputs;
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

            bool all_processed=ProcessOP_And_Push_To_Nexts(node);

            if (all_processed)
            {
                node.IsProcessed = true;
                node.IsWalked = true;
            }
        }
        
        private bool ProcessOP_And_Push_To_Nexts(GraphNode node)
        {
            List<double[]> input_data = new List<double[]>();
            if (node.Value!=null&& node.Value.Length>0)
            {
                input_data.Add(node.Value);//设置初始值
            }
            else
            {
                foreach (var nc in node.Previouses)
                {
                    if (nc.OperationResult.Data == null)
                        return false;

                    input_data.Add(nc.OperationResult.Data as double[]);
                }
            }
            //将当前node的DATA和各个nexts的op进行计算，保存在connection上

            //执行某些计算，先忽略，占位
            if (node.NodeType == NodeType.SaveValueNode)
            {
                node.OutputValue = input_data;
                //Console.WriteLine(string.Format("{1}({0})", node.NodeType.ToString(), node.Name));
            }

            if(input_data.Count>0)
                node.Value = input_data.First().ToArray();

            for (var i=0;i<node.Nexts.Count;i++)
            {
                var n = node.Nexts[i];

                double[] result = n.Operation.Calc(node, input_data);

                n.OperationResult.Data = result;
            }

            return true;
        }

        private bool IsAllIncomingOK(GraphNode node)
        {
            if (node.Value != null && node.Value.Length > 0)
                return true;
            //if (node.IsStartable&&node.Value!=null)   //是输入节点，则直接返回准备好状态
            //    return true;
            //if (node.IsRandomizeInit && node.Value != null)   //是权重节点，则直接返回准备好状态
            //    return true;

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
