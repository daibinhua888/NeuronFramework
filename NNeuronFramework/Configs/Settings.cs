using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNeuronFramework.Configs
{
    class Settings
    {
        public static string DotExePath
        {
            get
            {
                return ConfigurationManager.AppSettings["dotExePath"];
            }
        }
    }
}
