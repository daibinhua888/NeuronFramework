using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Somethingelse
{
    class Program
    {
        static void Main(string[] args)
        {
            var bytes = imageToByteArray(@"D:\documents\visual studio 2017\Projects\NeuronFramework\Somethingelse\images\2.jpg");

            StringBuilder sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(string.Format("{0},", b));
            }
            Console.WriteLine(bytes.Length);

            File.WriteAllText(@"D:\documents\visual studio 2017\Projects\NeuronFramework\Somethingelse\images\1.jpg.txt", sb.ToString().TrimEnd(",".ToCharArray()));

            Console.ReadKey();
        }

        private static byte[] imageToByteArray(string FilePath)
        {
            using (MemoryStream ms = new MemoryStream())
            {

                using (Image imageIn = Image.FromFile(FilePath))
                {

                    using (Bitmap bmp = new Bitmap(imageIn))
                    {
                        bmp.Save(ms, imageIn.RawFormat);
                    }

                }
                return ms.ToArray();
            }
        }
    }
}
