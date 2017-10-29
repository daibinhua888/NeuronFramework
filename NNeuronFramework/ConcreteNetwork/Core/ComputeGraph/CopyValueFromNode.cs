using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.ComputeGraph
{
    public class CopyValueFromNode : BaseComputeGraphNode
    {
        public string TargetNodeName { get; set; }
        public List<double> InitialValue { get; set; }

        protected override List<double> Compute(List<List<double>> data)
        {
            //是否目标节点已经有计算的value了
            //      有：      RETURN VALUE
            //      没有：    初始化，RETURN VALUE
            BaseComputeGraphNode targetNode =GraphNodeContext.GetNode(this.TargetNodeName);
            if (targetNode.IsProcessed)
            {
                return targetNode.ComputedResults;
            }
            else
            {
                if (InitialValue == null)
                    InitialBy(targetNode);

                return InitialValue;
            }
        }

        private void InitialBy(BaseComputeGraphNode targetNode)
        {
            int dim = targetNode.ResultsDim;

            InitialValue = new List<double>();
            for (var i = 0; i < dim; i++)
                InitialValue.Add(Utils.Utils.GenerateRandomValue());
        }
    }
}
