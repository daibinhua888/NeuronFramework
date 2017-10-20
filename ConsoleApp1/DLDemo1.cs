using NNeuronFramework.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class DLDemo1
    {
        public static void Demo()
        {
            Model model = new Model();

            var kernels = KernelFactory.Create(new string[] { "A", "B" });
            model.AddLayer(new ConvLayer(kernels));

            model.AddLayer(new MaxPoolLayer(2, 2));

            model.AddLayer(new FlattenLayer());

            Matrix[] inputs= DataGenerator.GenerateDataSet5_1();

            foreach (var input in inputs)
                input.Display();

            model.Fit(inputs);

            Matrix[] outputs=model.GetOutputs();

            Console.WriteLine("**************OUTPUTS***************");

            foreach(var output in outputs)
                output.Display();
        }
    }
}
