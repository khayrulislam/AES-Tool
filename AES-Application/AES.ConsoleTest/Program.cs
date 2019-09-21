using AES.Encryption.encrypt;
using AES.Encryption.Mode;
using AES.Shared.FileReader;
using AES.Shared.KeyExpand;
using AES.Shared.Utility;
using System;
using System.Collections.Generic;
using System.IO;
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


            Parameter par = new Parameter();
            par.Key = "Thats my Kung Fu";
            par.Text = "Two One Nine Two";
            par.Type = "e";
            par.Mode = "ecb";

            var enc = new Encrypt(par);
            enc.SetEncryptionMode(new ECBMode());
            enc.Execute();
            Console.Read();
            
            /*           byte[] inputBytes = Encoding.ASCII.GetBytes(inputText);
                       byte[] inputKeys = Encoding.ASCII.GetBytes(key);


                       using(Stream file = File.OpenWrite(@"../../../AES.shared/s-Box/box.txt"))
                       {
                           file.Write(inputBytes,0,inputBytes.Length);
                       }*/

            /*Encryption enc = new Encryption(inputKeys);
            Util.PrintHex(enc.Encrypt(inputBytes));
            Console.Read();*/





        }
    }
}
