using AES.EncryptDecrypt.algorithm;
using AES.EncryptDecrypt.utility;
using System;

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
            par.Mode = "ecb";
            par.InputFilePath = Constants.INPUT_FILE_PATH;
            par.OutputFolderPath = Constants.OUTPUT_FILE_PATH;
            var enc = new AESAlgorithm(par);
            enc.Execute();

            
            Console.Read();



        }
    }
}
