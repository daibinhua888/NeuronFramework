﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NNeuronFramework.Utils
{
    public static class FunctionUtils
    {
        public static double Linear(double x)
        {
            return x;
        }

        public static double Sigmoid(double x)
        {
            double one = 1;
            return one / (one + System.Math.Exp(-x));
        }

        /// <summary>
        /// Softplus函数，Softplus函数是Logistic-Sigmoid函数原函数
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Softplus(double x)
        {
            return Math.Log(1+ Math.Exp(x));
        }

        /// <summary>
        /// Rectified Linear Units
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double ReLU(double x)
        {
            return Math.Max(x, 0);
        }

        public static double Tanh(double x)
        {
            return (Math.Exp(x)- Math.Exp(-x)) / (Math.Exp(x) + Math.Exp(-x));
        }

        public static double[] SoftMax(double[] xs)
        {
            var outputs = new List<double>();

            double sum = 0;

            foreach (var x in xs)
                sum += Math.Pow(Math.E, x);

            foreach (var x in xs)
            {
                var x_output= Math.Pow(Math.E, x);

                outputs.Add(x_output);
            }

            return outputs.ToArray();
        }
    }
}
