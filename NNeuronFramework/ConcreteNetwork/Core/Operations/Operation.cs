using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.Operations
{
    public abstract class Operation
    {
        public abstract double[] Calc(GraphNode node, List<double[]> data);
    }
}
