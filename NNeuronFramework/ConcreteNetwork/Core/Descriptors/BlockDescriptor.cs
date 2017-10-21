using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core.Descriptors
{
    class BlockDescriptor
    {
        public string Name { get; internal set; }
        public NeuronsBlock NeuronsBlock { get; internal set; }
        public string CopyValueFrom { get; internal set; }
        public bool IsStartable { get; internal set; }
    }
}
