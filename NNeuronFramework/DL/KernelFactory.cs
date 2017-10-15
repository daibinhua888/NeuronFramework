using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.DL
{
    public static class KernelFactory
    {
        public static Kernel[] Create(Matrix[] matrixes)
        {
            List<Kernel> kernels = new List<Kernel>();

            foreach (var matrix in matrixes)
                kernels.Add(new Kernel(matrix));

            return kernels.ToArray();
        }

        public static Kernel[] Create(string[] kernelNames)
        {
            List<Kernel> kernels = new List<Kernel>();

            foreach (var name in kernelNames)
            {
                Kernel kernel = null;

                if (true)
                {
                    Matrix m = new Matrix(3, 3);//设置矩阵

                    m.Data = DemoData;

                    kernel = new Kernel(m);
                }

                kernels.Add(kernel);
            }

            return kernels.ToArray();
        }

        private static double[][] DemoData
        {
            get
            {
                double[][] kernel = new double[3][];

                kernel[0] = new double[3];
                kernel[1] = new double[3];
                kernel[2] = new double[3];

                kernel[0][0] = -1; kernel[0][1] = 0; kernel[0][2] = 1;
                kernel[1][0] = -1; kernel[1][1] = 0.2; kernel[1][2] = 1;
                kernel[2][0] = -1; kernel[2][1] = 0; kernel[2][2] = 0.9;

                return kernel;
            }
        }
    }
}
