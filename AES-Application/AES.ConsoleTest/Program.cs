using AES.EncryptDecrypt.utility;
using AES.EncryptDecrypt.mode;
using AES.EncryptDecrypt.FileReader;
using AES.EncryptDecrypt.KeyExpand;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AES.EncryptDecrypt.algorithm;

namespace AES.ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Parameter par = new Parameter();
            par.Key = "Thats my Kung Fu";
            par.Text = "Two One Nine TwoTwo One Nine TwoTwo One Nine Two";
            par.InitialVector = "ABCDEFGHIPQRSTUV";
            par.Type = "e";
            par.Mode = "cbc";
            par.InputFilePath = Constants.INPUT_FILE_PATH2;
            par.OutputFolderPath = Constants.OUTPUT_FILE_PATH;
            var enc = new AESAlgorithm(par);
            enc.Execute();

            
            Console.Read();



        }
    }
}
