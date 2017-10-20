using NNeuronFramework.ConcreteNetwork.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.ConcreteNetwork.Core
{
    public class NetworkGraph
    {
        public void Compile()
        {
            throw new NotImplementedException();
        }

        public void AddFlow(string name, string[] mergeNames, string[] postNames)
        {
            throw new NotImplementedException();
        }

        public void DefineBlock(string name, NeuronsBlock neuronsBlock, string copyValueFrom="")
        {
            throw new NotImplementedException();
        }

        internal void DefineOperation(string opName, Operation operation)
        {
            throw new NotImplementedException();
        }
    }
}
