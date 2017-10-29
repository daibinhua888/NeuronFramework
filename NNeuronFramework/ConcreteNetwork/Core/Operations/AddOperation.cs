using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.Operations
{
    public class AddOperation : Operation
    {
        public override double[] Calc(GraphNode node, List<double[]> data)
        {
            if (data.Count == 0)
                throw new Exception("无输入数据");

            var results = new List<double>();

            int size = data.First().Length;
            for (var index = 0; index < size; index++)
            {
                double sum = 0;

                foreach (var d in data)
                    sum += d[index];

                results.Add(sum);
            }

            return results.ToArray();
        }
    }
}
