using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.DL
{
    public class ConvLayer: BaseLayer
    {
        private Kernel[] kernels;

        public ConvLayer(Kernel[] kernels)
        {
            this.kernels = kernels;
        }

        public override void Process()
        {
            this.ClearOutputs();

            foreach (var kernel in kernels)
            {
                List<Matrix> outputs = new List<Matrix>();
                foreach (var input in this.GetInputs())
                {
                    //生成矩阵
                    Matrix output = Conv(input, kernel);
                    outputs.Add(output);
                }

                //合并输出矩阵,加法
                Matrix kernelOutput=MergeOutputs(outputs);

                this.AddOutput(kernelOutput);
            }
        }

        private Matrix Conv(Matrix input, Kernel kernel)
        {
            if (input.Rows < kernel.Rows || input.Cols < kernel.Cols)
                throw new Exception("Rows/Cols not matched");

            int targetRows= input.Rows- kernel.Rows+1;
            long targetCols = input.Cols-kernel.Cols+1;

            Matrix result = new Matrix(targetRows, targetCols);

            //循环输入矩阵的Rows
            for (var row = 0; row < targetRows; row++)
            {
                for (var col= 0; col < targetCols; col++)
                {
                    Console.Write(string.Format("({0}, {1}),", row, col));

                    double value = CalcConv(input, row, col, kernel);
                    result.SetValue(row, col, value);
                }
                Console.WriteLine();
            }

            return result;
        }

        private double CalcConv(Matrix input, int beginRow, int beginCol, Kernel kernel)
        {
            double sum = 0;

            for (var row = 0; row < kernel.Rows; row++)
            {
                for (var col = 0; col < kernel.Cols; col++)
                {
                    double input_value = input.Data[beginRow+row][beginCol+col];
                    double kernel_value = kernel.Matrix.Data[row][col];

                    double value= input_value * kernel_value;

                    sum += value;
                    Console.WriteLine(string.Format("{0}*{1}={2}    SUM={3}", input_value, kernel_value, value, sum));
                }
            }

            return sum;
        }

        private Matrix MergeOutputs(List<Matrix> outputs)
        {
            Matrix sumed = outputs.First();

            for (var index = 1; index < outputs.Count; index++)
            {
                var m=outputs[index];
                sumed = sumed.Sum(m);
            }

            return sumed;
        }
    }
}
