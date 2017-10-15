using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class TrainingSets
    {
        public TrainingSets()
        {
            this.XList = new List<List<double>>();
            this.YList = new List<List<double>>();
        }

        public List<List<double>> XList { get; internal set; }
        public List<List<double>> YList { get; internal set; }
    }
}
