using AES.EncryptDecrypt.algorithm;
using AES.EncryptDecrypt.utility;
using System;

namespace AES.ConsoleTest
{
    public class Program
    {


        public static string INPUT_FILE_PATH = "../../../AES.EncryptDecrypt/fileReader/input.txt";
        public static string INPUT_FILE_PATH2 = "../../../AES.EncryptDecrypt/fileReader/input.pdf";
        public static string OUTPUT_FILE_PATH = "../../../AES.EncryptDecrypt/fileReader";
        public static string OUTPUT_FILE_PATH2 = "../../../AES.EncryptDecrypt/fileReader";

        public static void Main(string[] args)
        {

            Parameter par = new Parameter();
            par.Key = "Thats my Kung Fu";
            par.InitialVector = "ABCDEFGHIPQRSTUV";
            par.Type = "e";
            par.Mode = "ecb";
            par.InputFilePath = INPUT_FILE_PATH;
            par.OutputFolderPath = OUTPUT_FILE_PATH;
            var enc = new AESAlgorithm(par);
            enc.Execute();

            
            Console.Read();



        }
    }
}
