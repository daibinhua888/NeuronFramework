using System;
using System.Collections.Generic;
using System.Text;

namespace NNeuronFramework
{
    public abstract class ModelTrainer
    {
        protected NeuronNetwork network;
        protected int epoch;
        protected IAlgorithm algorithm;
        

        public ModelTrainer(NeuronNetwork network, int epoch, IAlgorithm algorithm)
        {
            this.network = network;
            this.epoch = epoch;
            this.algorithm = algorithm;
        }
    }
}
