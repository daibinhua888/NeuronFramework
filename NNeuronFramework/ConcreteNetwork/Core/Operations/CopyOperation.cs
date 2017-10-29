using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.Operations
{
    public class CopyOperation : Operation
    {
        public override double[] Calc(GraphNode node, List<double[]> data)
        {
            if (data.Count == 0)
                throw new Exception("无输入数据");

            if (data.Count > 1)
                throw new Exception("不支持多个数据组");

            return data.First();
        }
    }
}
