using NNeuronFramework.ConcreteNetwork.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core
{
    public class GraphNode
    {
        public GraphNode()
        {
            this.Nexts = new List<GraphNodeConnector>();
            this.Previouses = new List<GraphNodeConnector>();
            this.IsProcessed = false;
            this.CopyValueFrom = string.Empty;
            this.Name = Guid.NewGuid().ToString("N");
            this.IsTempNode = true;
            this.IsWalked = false;
            //this.IsRandomizeInit = false;
        }

        public NodeType NodeType { get; set; }
        public Operation Operation { get; set; }
        public bool IsProcessed { get; set; }
        public string CopyValueFrom { get; set; }
        public string Name { get; internal set; }
        public List<GraphNodeConnector> Nexts { get; set; }
        public List<GraphNodeConnector> Previouses { get; set; }

        public bool IsWalked { get; set; }
        public bool IsSavable { get; internal set; }
        //public bool IsRandomizeInit { get; set; }

        public bool IsStartable { get; set; }
        public bool IsTempNode { get; set; }
        public double[] Value { get; set; }
        public Dictionary<string, double> Weights { get; set; }
        public List<double[]> OutputValue { get; set; }
        public int InputsCount { get; internal set; }
        public int NeuronsCount { get; internal set; }
        public bool IsWeightsNode { get; internal set; }
        public bool IsMonitoringNode { get; set; }

        public double B;

        public static Dictionary<string, bool> processedParses = new Dictionary<string, bool>();

        public List<string> ParseDirections()
        {
            if (this.IsWalked)
                return new List<string>();

            var lst = new List<string>();

            foreach (var connect in Nexts)
            {
                var key = string.Format("{0}.{1}.{2}", this.Name, connect.Node.Name, connect.Operation.GetType().Name);
                if (!processedParses.ContainsKey(key))
                {
                    lst.Add(string.Format("\"{0}\"->\"{1}\"[label=\"{2}\"]", this.Name, connect.Node.Name, connect.Operation.GetType().Name.TrimEnd("Operation".ToCharArray())));
                    processedParses.Add(key, true);
                }
            }

            foreach (var connect in Previouses)
            {
                var key = string.Format("{0}.{1}.{2}", connect.Node.Name, this.Name, connect.Operation.GetType().Name);
                if (!processedParses.ContainsKey(key))
                {
                    lst.Add(string.Format("\"{1}\"->\"{0}\"[label=\"{2}\"]", this.Name, connect.Node.Name, connect.Operation.GetType().Name.TrimEnd("Operation".ToCharArray())));
                    processedParses.Add(key, true);
                }
            }
            
            this.IsWalked = true;

            foreach (var connect in Nexts)
                lst.AddRange(connect.Node.ParseDirections());

            foreach (var connect in Previouses)
                lst.AddRange(connect.Node.ParseDirections());

            return lst;
        }
        

        public void RandomizeWeights()
        {
            var neuronsCount = this.NeuronsCount;
            var inputsCount = this.Previouses.First().Node.InputsCount;

            Console.WriteLine(neuronsCount);
            Console.WriteLine(inputsCount);
            Console.WriteLine("===============");
            Dictionary<string, double> weights = new Dictionary<string, double>();
            for (var nIndex = 0; nIndex < neuronsCount; nIndex++)
            {
                for (var inIndex = 0; inIndex < inputsCount; inIndex++)
                {
                    string key = string.Format("{0}, {1}", nIndex, inIndex);

                    weights.Add(key, Utils.Utils.GenerateRandomValue());
                }
            }
            this.Weights = weights;
        }
    }
}
