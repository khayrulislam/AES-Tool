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

            //byte x = Convert.ToByte("a2", 8);
            byte x = 0xa;
            byte y = 0x1;
            int z = x << 4;
            DataReader re = new DataReader();
            re.ReadSBoxData();
            Console.WriteLine(z^y);
            Console.ReadLine();
        }
    }
}
