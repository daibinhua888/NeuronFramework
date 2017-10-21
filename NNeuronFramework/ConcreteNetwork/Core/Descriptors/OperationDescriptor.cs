using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNeuronFramework.ConcreteNetwork.Core.Operations;

namespace NNeuronFramework.ConcreteNetwork.Core.Descriptors
{
    class OperationDescriptor
    {
        public string Name { get; internal set; }
        public Operation Operation { get; internal set; }
    }
}
