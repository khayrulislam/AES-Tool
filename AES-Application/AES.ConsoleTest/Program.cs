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

            string inputText = "qwertyuiopasdfgh";
            string key = "1122334455667788";


            Parameter par = new Parameter();
            par.Key = "Thats my Kung Fu";
            par.Text = "Two One Nine Two Two";
            par.InitialVector = "ABCDEFGHIPQRSTUV";
            par.Type = "e";
            par.Mode = "ecb";

            var enc = new Encrypt(par);
            enc.SetEncryptionMode(new ECBMode());
            enc.Execute();
            Console.Read();



            /* int[] arr = new int[4] {10,20,30,40 };
             int[] tt = new int[10];


             Array.Copy(arr,0,tt,0,10-);

             for(int i = 0; i < 10; i++)
             {
                 Console.WriteLine(tt[i]);
             }
 */
            Console.Read();



        }
    }
}
