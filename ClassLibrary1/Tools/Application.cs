using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyCarrot.Tools
{
    public static class Application
    {
        public static String Root
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
                
            }
        }
    }
}
