using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.DL
{
    public class MinPoolLayer : BaseLayer
    {
        private int rows;
        private int cols;

        public MinPoolLayer(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
        }

        public override void Process()
        {
            this.ClearOutputs();

            foreach (var input in this.GetInputs())
            {
                //生成矩阵
                Matrix output = Conv(input, this.rows, this.cols);

                this.AddOutput(output);
            }
        }

        private Matrix Conv(Matrix input, int rows, int cols)
        {
            if (input.Rows < rows || input.Cols < cols)
                throw new Exception("Rows/Cols not matched");

            int targetRows = input.Rows - rows + 1;
            long targetCols = input.Cols - cols + 1;

            Matrix result = new Matrix(targetRows, targetCols);

            //循环输入矩阵的Rows
            for (var row = 0; row < targetRows; row++)
            {
                for (var col = 0; col < targetCols; col++)
                {
                    Console.Write(string.Format("({0}, {1}),", row, col));

                    double value = CalcConv_Min(input, row, col, rows, cols);
                    result.SetValue(row, col, value);
                }
                Console.WriteLine();
            }

            return result;
        }

        private double CalcConv_Min(Matrix input, int beginRow, int beginCol, int calc_rows, int calc_cols)
        {
            double minValue = input.Data[beginRow + 0][beginCol + 0];

            for (var row = 0; row < calc_rows; row++)
            {
                for (var col = 1; col < calc_cols; col++)
                {
                    double input_value = input.Data[beginRow + row][beginCol + col];

                    if (input_value < minValue)
                        minValue = input_value;
                }
            }

            return minValue;
        }
    }
}
