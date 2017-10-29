using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.ComputeGraph
{
    public abstract class BaseComputeGraphNode
    {
        public BaseComputeGraphNode()
        {
            this.Nexts = new List<BaseComputeGraphNode>();
            this.Prevs = new List<BaseComputeGraphNode>();
            this.ComputedResults = null;
        }

        public string NodeName { get; set; }
        public List<double> ComputedResults { get; set; }

        /// <summary>
        /// 计算结果维度
        /// </summary>
        public int ResultsDim { get; set; }

        public List<BaseComputeGraphNode> Nexts { get; set; }
        public List<BaseComputeGraphNode> Prevs { get; set; }

        public bool IsProcessed { get; set; }
        public bool IsWalked { get; set; }
        public bool IsMonitoring { get; set; }

        public void ComputeResult()
        {
            IsProcessed = false;
            
            //检查parents是否都已经处理了，并且有结果了
            foreach (var node in this.Prevs)
            {
                if (node.ComputedResults == null)
                    return;
                if (node.ComputedResults.Count==0)
                    return;
            }

            List<List<double>> incoming_data = new List<List<double>>();
            foreach (var node in this.Prevs)
                incoming_data.Add(node.ComputedResults);

            var results = Compute(incoming_data);

            this.ComputedResults = results;

            this.IsProcessed = true;
        }
        protected abstract List<double> Compute(List<List<double>> data);
        
    }
}
