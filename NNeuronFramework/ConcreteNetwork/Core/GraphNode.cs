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
            this.IsWalked = false;
        }

        public NodeType NodeType { get; set; }
        public NeuronsBlock Block { get; set; }
        public Operation Operation { get; set; }
        public bool IsProcessed { get; set; }
        public string CopyValueFrom { get; set; }
        public string Name { get; internal set; }
        public List<GraphNodeConnector> Nexts { get; set; }
        public List<GraphNodeConnector> Previouses { get; set; }

        public bool IsWalked { get; set; }
        public bool IsSavable { get; internal set; }

        public List<string> ParseDirections()
        {
            if (this.IsWalked)
                return new List<string>();

            var lst = new List<string>();

            foreach (var connect in Nexts)
                lst.AddRange(connect.Node.ParseDirections());

            foreach (var connect in Nexts)
                lst.Add(string.Format("\"{0}\"->\"{1}\"[label=\"{2}\"]", this.Name, connect.Node.Name, connect.Operation.GetType().Name));

            this.IsWalked = true;

            return lst;
        }
    }
}
