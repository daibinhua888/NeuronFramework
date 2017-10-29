using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.Operations
{
    public class MultiplyOperation : Operation
    {
        public override double[] Calc(GraphNode node, List<double[]> data)
        {
            if (data.Count == 0)
                throw new Exception("无输入数据");

            var results = new List<double>();

            var xs = data.First();
            var ws = data.Take(2).Skip(1).First();

            int size = xs.Length;

            for (var index = 0; index < size; index++)
            {
                double sum = 0;

                sum = xs[index] * ws[index];

                results.Add(sum);
            }

            return results.ToArray();
        }
    }
}
