using System;
using System.Collections.Generic;
using System.Text;

namespace NNeuron.OutputConverters
{
    public class Normalizer
    {
        private double avg;
        public void Fit(List<List<double>> vectors)
        {
            double sum = 0;
            foreach (var vector in vectors)
            {
                foreach (var v in vector)
                    sum += Math.Pow(v, 2);
            }

            this.avg = Math.Sqrt(sum);
        }

        public List<double> Normalize(List<double> vector)
        {
            var results = new List<double>();

            foreach (var v in vector)
                results.Add(v / avg);

            return results;
        }
        public List<List<double>> Normalize(List<List<double>> vectors)
        {
            List<List<double>> lst = new List<List<double>>();

            foreach (var vector in vectors)
                lst.Add(Normalize(vector));

            return lst;
        }

        public List<List<double>> DeNormalize(List<List<double>> vectors)
        {
            List<List<double>> lst = new List<List<double>>();

            foreach (var vector in vectors)
                lst.Add(DeNormalize(vector));

            return lst;
        }

        public List<double> DeNormalize(List<double> vector)
        {
            var results = new List<double>();

            foreach (var v in vector)
                results.Add(v * avg);

            return results;
        }
    }
}
