using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NNeuronFramework.OutputConverters
{
    public static class NeuronOutputConverter
    {
        public static List<List<double>> OrderInteger(List<List<double>> outputs, double lower=0, double upper=1, double middle=0.5)
        {
            List<List<double>> results = new List<List<double>>();

            foreach (var output in outputs)
            {
                var convertedlst = new List<double>();

                foreach (var output_single_value in output)
                {
                    if (output_single_value >= middle)
                        convertedlst.Add(upper);
                    else
                        convertedlst.Add(lower);
                }

                results.Add(convertedlst);
            }

            return results;
        }

        public static List<List<double>> MaxOneHotEncode(List<List<double>> outputs)
        {
            List<List<double>> results = new List<List<double>>();

            foreach (var output in outputs)
            {
                var convertedlst = new List<double>();

                var maxValue = output.Max();

                foreach (var output_single_value in output)
                {
                    if (output_single_value ==maxValue)
                        convertedlst.Add(1);
                    else
                        convertedlst.Add(0);
                }

                results.Add(convertedlst);
            }

            return results;
        }

        public static List<List<double>> MinOneHotEncode(List<List<double>> outputs)
        {
            List<List<double>> results = new List<List<double>>();

            foreach (var output in outputs)
            {
                var convertedlst = new List<double>();

                var minValue = output.Min();

                foreach (var output_single_value in output)
                {
                    if (output_single_value == minValue)
                        convertedlst.Add(1);
                    else
                        convertedlst.Add(0);
                }

                results.Add(convertedlst);
            }

            return results;
        }
    }
}
