using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.Operations
{
    public class FullConnectionOperation: Operation
    {
        public override double[] Calc(GraphNode node, List<double[]> data)
        {
            var xs = data.First();
            var weights=node.Weights;

            var lst = new List<double>();

            //double sum = 0;

            //for (var x_index = 0; x_index < xs.Length; x_index++)
            //    for (var w_index = 0; w_index < ws.Length; w_index++)
            //        sum+=xs[x_index] *ws[w_index];
            for (var i = 0; i < node.NeuronsCount; i++)//占位
                lst.Add(Utils.Utils.GenerateRandomValue());

            return lst.ToArray();
        }
    }
}
