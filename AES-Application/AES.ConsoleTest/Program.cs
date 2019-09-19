using AES.Shared.FileReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {

            int x = 255;

            if (x >> 7 == 1)
            {
                Console.WriteLine("ok"+x);
            }
        }
    }
}
