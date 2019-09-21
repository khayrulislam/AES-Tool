using AES.Shared.encryption;
using AES.Shared.FileReader;
using AES.Shared.KeyExpand;
using AES.Shared.Utility;
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

/*            string inputText = "Two One Nine Two";
            string key = "Thats my Kung Fu";
*/
            string inputText = "qwertyuiopasdfgh";
            string key = "1122334455667788";


            byte[] inputBytes = Encoding.ASCII.GetBytes(inputText);
            byte[] inputKeys = Encoding.ASCII.GetBytes(key);

            Encryption enc = new Encryption(inputKeys);
            Util.PrintHex(enc.Encrypt(inputBytes));
            Console.Read();


        }
    }
}
