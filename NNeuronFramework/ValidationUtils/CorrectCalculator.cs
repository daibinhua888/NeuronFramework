using System;
using System.Collections.Generic;
using System.Text;

namespace NNeuronFramework.ValidationUtils
{
    public class CorrectCalculator
    {
        public double Calculate(List<List<double>> predicted_ys, List<List<double>> wanted_ys)
        {
            if (predicted_ys == null)
                return 0;
            if (wanted_ys == null)
                return 0;
            if (predicted_ys.Count != wanted_ys.Count)
                return 0;

            double correctCount = 0;

            for (var index=0;index<predicted_ys.Count;index++)
            {
                var p = predicted_ys[index];
                var w = wanted_ys[index];

                int matchedCount = 0;
                for (var v_index = 0; v_index < p.Count; v_index++)
                {
                    if (p[v_index] == w[v_index])
                        matchedCount++;
                }
                if (matchedCount == p.Count)
                    correctCount++;
            }

            return correctCount / predicted_ys.Count;
        }
    }
}
