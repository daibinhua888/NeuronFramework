using NNeuronFramework.ConcreteNetwork.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core
{
    public struct GraphNodeConnector
    {
        public GraphNodeConnectorResult OperationResult { get; set; }
        public GraphNode Node{get;set;}
        public Operation Operation { get; set; }
    }
}
