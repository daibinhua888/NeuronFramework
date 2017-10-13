using System;
using System.Collections.Generic;
using System.Text;

namespace NNeuronFramework.ConcreteTrainer
{
    public class SOMSetting
    {
        public int FromEpoch { get; set; }
        public int ToEpoch { get; set; }

        public double LearningRate { get; set; }

        public int Radius { get; set; }
    }
}
