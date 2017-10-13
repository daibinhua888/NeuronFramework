using System;
using System.Collections.Generic;
using System.Text;

namespace NNeuronFramework
{
    public interface IAlgorithm
    {
        void Process(List<double> x_vector, List<double> y_vector);
    }
}
