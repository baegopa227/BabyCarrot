using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BabyCarrot.Tools;
using BabyCarrot.Extensions;

namespace BabyCarrotTest1 
{
    class Program
    {
        static void Main(String[] args)
        {
            //EmailManager.Send(to, subejct, contents);
            DBManager.connect();
        }
    }


}