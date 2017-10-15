using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.DL
{
    public class FlattenLayer : BaseLayer
    {
        public override void Process()
        {
            this.ClearOutputs();

            long size = 0;
            foreach (var input in this.GetInputs())
                size += input.Rows * input.Cols;

            Matrix output =new Matrix(1, size);
            int addedCount = 0;

            foreach (var input in this.GetInputs())
            {
                for (var rowIndex = 0; rowIndex < input.Rows; rowIndex++)
                {
                    for (var colIndex = 0; colIndex < input.Cols; colIndex++)
                    {
                        output.Data[0][addedCount] =input.Data[rowIndex][colIndex];

                        addedCount++;
                    }
                }
            }

            this.AddOutput(output);
        }
    }
}
