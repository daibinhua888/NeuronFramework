using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.ComputeGraph
{
    public class InputNode : BaseComputeGraphNode
    {
        public List<double> InputData { get; set; }

        protected override List<double> Compute(List<List<double>> data)
        {
            return this.InputData;
        }
    }
}
