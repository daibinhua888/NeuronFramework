using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.ComputeGraph
{
    public class MultiplyNode : BaseComputeGraphNode
    {
        protected override List<double> Compute(List<List<double>> data)
        {
            int vectorLength = data.First().Count;

            //按列来*
            var results = new List<double>();

            for (var index = 0; index < vectorLength; index++)
            {
                double sum = 1;

                foreach (var datas in data)
                    sum *= datas[index];

                results.Add(sum);
            }

            return results;
        }
    }
}
