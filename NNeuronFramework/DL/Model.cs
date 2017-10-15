using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.DL
{
    public class Model
    {
        private List<BaseLayer> layers=new List<BaseLayer>();

        public void AddLayer(BaseLayer layer)
        {
            this.layers.Add(layer);
        }

        public void Fit(Matrix[] inputs)
        {
            Matrix[] latestOutputs = inputs;

            foreach (var layer in this.layers)
            {
                layer.SetInputs(latestOutputs);
                layer.Process();
                latestOutputs=layer.GetOutputs();
            }
        }

        public Matrix[] GetOutputs()
        {
            return layers.Last().GetOutputs();
        }
    }
}
