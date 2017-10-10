using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DemoConsole
{
    class DataGenerator
    {
        public static TrainingSets GenerateDataSet1()
        {
            var dataset = new TrainingSets();

            dataset.XList.Add(ParseDoubles(@"
0,0,0,1,1,0,0,0
0,0,1,0,0,1,0,0
0,1,0,0,0,0,1,0
1,0,0,0,0,0,0,1
1,0,0,0,0,0,0,1
0,1,0,0,0,0,1,0
0,0,1,0,0,1,0,0
0,0,0,1,1,0,0,0
"));

            dataset.XList.Add(ParseDoubles(@"
0,0,0,1,1,0,0,0
0,0,0,1,1,0,0,0
0,0,0,1,1,0,0,0
0,0,0,1,1,0,0,0
0,0,0,1,1,0,0,0
0,0,0,1,1,0,0,0
0,0,0,1,1,0,0,0
0,0,0,1,1,0,0,0
"));
            dataset.XList.Add(ParseDoubles(@"
0,0,1,1,1,0,0,0
0,1,0,0,0,1,0,0
0,0,0,0,0,0,1,0
0,0,0,0,0,1,0,0
0,0,0,0,1,0,0,0
0,0,0,1,0,0,0,0
0,0,1,0,0,0,0,0
0,1,1,1,1,1,1,1
"));

            dataset.YList.Add(ParseDoubles("0,0,1"));//代表0
            dataset.YList.Add(ParseDoubles("0,1,0"));//代表1
            dataset.YList.Add(ParseDoubles("1,0,0"));//代表2


            return dataset;
        }

        public static TrainingSets GenerateDataSet1_2()
        {
            var dataset = new TrainingSets();

            for (var i = 0; i < 100; i++)
            {
                List<double> bits = ParseInt2Doubles(i);

                dataset.XList.Add(bits);

                if (i%2==0)
                    dataset.YList.Add(ParseDoubles("0"));//代表奇数
                else
                    dataset.YList.Add(ParseDoubles("1"));//代表奇数
            }

            return dataset;
        }

        private static List<double> ParseInt2Doubles(int i)
        {
            double[] newD = new double[32];

            for (var j = 0; j < 32; j++)
                newD[j] = (double)((i>>j)&1);

            return newD.ToList();
        }

        internal static TrainingSets GenerateDataSet2()
        {
            /*
             * 人口数，GDP，人均工资
             */
            var dataset = new TrainingSets();

            {
                //SH
                var xs = new List<double>();
                xs.Add(1000000);
                xs.Add(3000000);
                dataset.XList.Add(xs);
            }
            {
                //BJ
                var xs = new List<double>();
                xs.Add(1500000);
                xs.Add(2500000);
                dataset.XList.Add(xs);
            }
            {
                //SZ
                var xs = new List<double>();
                xs.Add(2000000);
                xs.Add(3000000);
                dataset.XList.Add(xs);
            }

            {
                //YC
                var xs = new List<double>();
                xs.Add(50);
                xs.Add(20);
                dataset.XList.Add(xs);
            }

            return dataset;
        }

        internal static TrainingSets GenerateDataSet3()
        {
            var dataset = new TrainingSets();

            dataset.XList.Add(ParseDoubles("1,1,1,1,1,0,0,0,0"));
            dataset.XList.Add(ParseDoubles("0.9,1,1,1,1,0,0,0,0"));
            dataset.XList.Add(ParseDoubles("1,1,0.9,1,1,0,0,0,0"));
            dataset.XList.Add(ParseDoubles("1,1,1,1,1,1,0,0,0"));
            dataset.XList.Add(ParseDoubles("1,1,1,1,1,1,1,0,0"));
            dataset.XList.Add(ParseDoubles("1,1,1,0,1,0,0,0,0"));
            dataset.XList.Add(ParseDoubles("1,1,1,0,1,1,1,1,0"));
            dataset.XList.Add(ParseDoubles("1,1,1,0,1,1,1,1,0"));
            dataset.XList.Add(ParseDoubles("1,1,1,0,1,0,1,1,0"));
            dataset.XList.Add(ParseDoubles("0,0,0,0,0,1,1,1,1"));
            dataset.XList.Add(ParseDoubles("0,0,0,0,0,1,1.1,1,1"));
            dataset.XList.Add(ParseDoubles("0,0,0,0,0,1,1,1,1.1"));


            return dataset;
        }

        private static List<double> ParseDoubles(string s)
        {
            s = s.TrimStart("\r\n".ToCharArray());
            s = s.Replace("\r\n", ",");
            var arr = s.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var lst = new List<double>();

            foreach (var a in arr)
            {
                lst.Add(double.Parse(a));
            }

            return lst;
        }
    }
}
