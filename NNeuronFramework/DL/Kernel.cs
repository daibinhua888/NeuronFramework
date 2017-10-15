using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.DL
{
    public class Kernel
    {
        public Matrix Matrix { get; set; }

        public Kernel(Matrix matrix)
        {
            this.Matrix = matrix;
        }

        public int Rows { get { return this.Matrix.Rows; } }
        public long Cols { get { return this.Matrix.Cols; } }
    }
}
