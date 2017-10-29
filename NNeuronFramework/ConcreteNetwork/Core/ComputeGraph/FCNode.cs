using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.ComputeGraph
{
    public class FCNode : BaseComputeGraphNode
    {
        public FCNode(int inputXCount, int neuronsCount)
        {
            this.InputXCount = inputXCount;
            this.NeuronsCount = neuronsCount;
            this.Weights = new Dictionary<string, double>();
            InitWeights();
        }

        public Dictionary<string, double> Weights { get; set; }

        public int InputXCount { get; set; }
        public int NeuronsCount { get; set; }

        private void InitWeights()
        {
            for (var nIndex = 0; nIndex < NeuronsCount; nIndex++)
            {
                for (var xIndex = 0; xIndex < InputXCount; xIndex++)
                {
                    string key = string.Format("{0}.{1}", nIndex, xIndex);

                    this.Weights.Add(key, Utils.Utils.GenerateRandomValue());
                }
            }
        }

        protected override List<double> Compute(List<List<double>> data)
        {
            var results = new List<double>();

            var xs = data.First();
            var xs_length = xs.Count;

            if (xs_length != InputXCount)
                throw new Exception("InputXCount 必须和实际输入的X长度一致");

            for (var nIndex = 0; nIndex < NeuronsCount; nIndex++)
            {
                double sum = 0;

                for (var xIndex = 0; xIndex < xs_length; xIndex++)
                {
                    string key = string.Format("{0}.{1}", nIndex, xIndex);
                    
                    sum += xs[xIndex]*this.Weights[key];
                }

                results.Add(sum);
            }

            return results;
        }
    }
}
