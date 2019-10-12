using AES.EncryptDecrypt.algorithm;
using AES.EncryptDecrypt.utility;
using System;
using System.IO;

namespace AES.ConsoleTest
{
    public class Program
    {


        public static void Main(string[] args)
        {
            string INPUT_FILE_PATH = @"C:\Users\Mim\Desktop\rr.txt.aes";
            string INPUT_FILE_PATH2 = @"../../../AES.EncryptDecrypt/fileReader/input.txt";
            string OUTPUT_FILE_PATH = @"C:\Users\Mim\Desktop";

            Parameter par = new Parameter();
            par.Key = "Thats my Kung Fu";
            par.InitialVector = "ABCDEFGHIPQRSTUV";
            par.Type = "d";
            par.Mode = "ecb";
            par.InputFilePath = INPUT_FILE_PATH;
            par.OutputFolderPath = OUTPUT_FILE_PATH;


            FileInfo fileInfo = new FileInfo(INPUT_FILE_PATH);



            var enc = new AESAlgorithm(par);
            enc.Execute();

            
            Console.Read();



        }
    }
}
