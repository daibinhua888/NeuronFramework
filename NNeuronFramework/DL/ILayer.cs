using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.DL
{
    public interface ILayer
    {
        void SetInputs(Matrix[] m);
        Matrix[] GetInputs();
        void Process();
        Matrix[] GetOutputs();
    }
}
