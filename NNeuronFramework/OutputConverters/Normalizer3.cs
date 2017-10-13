using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NNeuronFramework.OutputConverters
{
    public class Normalizer3
    {
        private double avg;
        private double standardDiff;//标准差
        double maxRange = 0;

        public void Fit(List<List<double>> rows)
        {
            double sum = 0;
            double count = 0;

            foreach (var vectors in rows)
            {
                foreach (var vector in vectors)
                {
                    sum += vector;
                    count++;
                }
            }

            avg = sum / count;

            double tmpSum = 0;
            foreach (var vectors in rows)
            {
                foreach (var vector in vectors)
                {
                    tmpSum += Math.Pow(vector - avg, 2);
                }
            }

            standardDiff = tmpSum / (count - 1);

            foreach (var vectors in rows)
            {
                foreach (var vector in vectors)
                {
                    var v = (vector - avg) / standardDiff;
                    if (Math.Abs(v) > maxRange)
                        maxRange = Math.Abs(v);
                }
            }
        }

        public List<double> Normalize(List<double> vector)
        {
            //(原数值 - 平均值)/标准差
            var results = new List<double>();

            foreach (var v in vector)
                results.Add((v-avg)/standardDiff/(2*maxRange));

            return results;
        }
        public List<List<double>> Normalize(List<List<double>> vectors)
        {
            List<List<double>> lst = new List<List<double>>();

            foreach (var vector in vectors)
                lst.Add(Normalize(vector));

            return lst;
        }

        

        //public List<List<double>> DeNormalize(List<List<double>> vectors)
        //{
        //    List<List<double>> lst = new List<List<double>>();

        //    foreach (var vector in vectors)
        //        lst.Add(DeNormalize(vector));

        //    return lst;
        //}

        //public List<double> DeNormalize(List<double> vector)
        //{
        //    var results = new List<double>();

        //    foreach (var v in vector)
        //        results.Add(v * (max-min)+min);

        //    return results;
        //}
    }
}
