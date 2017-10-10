using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NNeuron.OutputConverters
{
    public class Normalizer2
    {
        private double min=-1;
        private double max=-1;

        public List<double> Normalize(List<double> vector)
        {
            var results = new List<double>();

            foreach (var v in vector)
                results.Add((v-min) / (max-min));

            return results;
        }
        public List<List<double>> Normalize(List<List<double>> vectors)
        {
            List<List<double>> lst = new List<List<double>>();

            foreach (var vector in vectors)
                lst.Add(Normalize(vector));

            return lst;
        }

        public void Fit(List<List<double>> rows)
        {
            foreach (var vectors in rows)
                foreach (var vector in vectors)
                {
                    if (vector < min)
                        min = vector;

                    if (vector > max)
                        max = vector;
                }
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
                results.Add(v * (max-min)+min);

            return results;
        }
    }
}
