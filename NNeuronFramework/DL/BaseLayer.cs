using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.DL
{
    public abstract class BaseLayer : ILayer
    {
        private List<Matrix> inputs = new List<Matrix>();
        private List<Matrix> outputs = new List<Matrix>();

        public Matrix[] GetOutputs()
        {
            return this.outputs.ToArray();
        }

        public abstract void Process();

        public void SetInputs(Matrix[] m)
        {
            this.inputs = m.ToList();
        }
        public Matrix[] GetInputs()
        {
            return this.inputs.ToArray();
        }

        protected void ClearOutputs()
        {
            this.outputs.Clear();
        }
        protected void AddOutput(Matrix output)
        {
            this.outputs.Add(output);
        }
    }
}
