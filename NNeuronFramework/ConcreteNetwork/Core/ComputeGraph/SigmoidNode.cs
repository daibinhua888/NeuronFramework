using NNeuronFramework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.ComputeGraph
{
    public class SigmoidNode : BaseComputeGraphNode
    {
        protected override List<double> Compute(List<List<double>> data)
        {
            var results = new List<double>();

            foreach (var data_row in data)
            {
                foreach (var d in data_row)
                {
                    results.Add(FunctionUtils.Sigmoid(d));
                }
            }

            return results;
        }
    }
}
