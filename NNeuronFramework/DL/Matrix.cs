using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.DL
{
    public class Matrix
    {
        public double[][] Data;
        private const double INIT_VALUE=0;
        private int rows;
        private long cols;

        public int Rows { get { return this.rows; } }
        public long Cols { get { return this.cols; } }

        public Matrix(int rows, long cols)
        {
            this.rows = rows;
            this.cols = cols;
            Construct();
        }

        private void Construct()
        {
            this.Data = new double[this.rows][];

            for(var row=0;row<rows;row++)
            {
                this.Data[row] = new double[this.cols];

                for (var col = 0; col < cols; col++)
                {
                    this.Data[row][col] = INIT_VALUE;
                }
            }
        }

        public Matrix Sum(Matrix matrix)
        {
            if (this.Data.GetUpperBound(0) != matrix.Data.GetUpperBound(0))
                throw new Exception("维度不一致，无法进行SUM操作");
            if (this.Data.GetUpperBound(1) != matrix.Data.GetUpperBound(1))
                throw new Exception("维度不一致，无法进行SUM操作");

            var array = new double[this.Data.First().GetUpperBound(0)][];

            Matrix m = new Matrix(this.Rows, this.Cols);

            for (var row = 0; row < m.Rows; row++)
                for (var col = 0; col < m.Cols; col++)
                    m.Data[row][col]=this.Data[row][col] +matrix.Data[row][col];

            return m;
        }
        
        public void SetValue(int row, int col, double value)
        {
            this.Data[row][col] = value;
        }

        public void Display()
        {
            Console.WriteLine("Matrix-->");
            foreach (var data in Data)
            {
                Console.Write("      ");
                foreach (var d in data)
                {
                    Console.Write(d);
                    Console.Write(",");
                }
                Console.WriteLine();
            }
        }

        
    }
}
