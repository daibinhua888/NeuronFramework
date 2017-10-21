using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.Descriptors
{
    class ConnectionDescriptor
    {
        public string Name { get; internal set; }
        public string[] MergeNames { get; internal set; }
        public string[] PostNames { get; internal set; }
    }
}
