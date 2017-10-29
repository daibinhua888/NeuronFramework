using NNeuronFramework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.Operations
{
    public class SigmoidOperation : Operation
    {
        public override double[] Calc(GraphNode node, List<double[]> data)
        {
            if (data.Count == 0)
                throw new Exception("无输入数据");

            if (data.Count > 1)
                throw new Exception("不支持多个数据组");

            var results = new List<double>();

            foreach (var d in data.First())
                results.Add(FunctionUtils.Sigmoid(d));

            return results.ToArray();
        }
    }
}
