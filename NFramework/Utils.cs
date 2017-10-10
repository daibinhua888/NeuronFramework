using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFramework
{
    public static class Utils
    {
        public static double GenerateRandomValue()
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            return r.NextDouble();
        }
    }
}
