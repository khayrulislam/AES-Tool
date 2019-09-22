using AES.Shared.utility;
using AES.Encryption.Mode;
using AES.Shared.FileReader;
using AES.Shared.KeyExpand;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AES.Encryption.encrypt;

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
            par.Text = "Two One Nine TwoTwo One Nine TwoTwo One Nine TwoTwo One Nine Two";
            par.InitialVector = "ABCDEFGHIPQRSTUV";
            par.Type = "e";
            par.Mode = "ecb";
            par.InputFilePath = Constants.INPUT_FILE_PATH;
            par.OutputFilePath = Constants.OUTPUT_FILE_PATH;

            var enc = new Encrypt(par);
            enc.SetEncryptionMode(new ECBMode());
            enc.Execute();
            Console.Read();



            Console.Read();



        }
    }
}
