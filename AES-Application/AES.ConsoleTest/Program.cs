using AES.Shared.utility;
using AES.EncryptOrDecrypt.Mode;
using AES.Shared.FileReader;
using AES.Shared.KeyExpand;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AES.EncryptOrDecrypt.encrypt_decrypt;

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
            par.OutputFilePath = Constants.OUTPUT_FILE_PATH;
            var enc = new EncryptDecryptOperation(par);
            enc.Execute();

            //
            /*

                        Parameter par = new Parameter();

                        byte[] keyarr = new byte[] { 0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x0a,0x0b,0x0c,0x0d,0x0e,0x0f};
                        //byte[] textarr = new byte[] { 0x00,0x11,0x22,0x33,0x44,0x55,0x66,0x77,0x88,0x99,0xaa,0xbb,0xcc,0xdd,0xee,0xff};

                        byte[] textarr = new byte[] { 0x69,0xc4,0xe0,0xd8,0x6a,0x7b,0x04,0x30,0xd8,0xcd,0xd7,0x80,0x70,0xb4,0xc5,0x5a};

                        //par.Key = System.Text.Encoding.UTF8.GetString(keyarr, 0, keyarr.Length);
                        par.Key = "Thats my Kung Fu";
                        par.Text = System.Text.Encoding.Default.GetString(textarr, 0, textarr.Length);
                        par.InitialVector = "ABCDEFGHIPQRSTUV";
                        par.Type = "d";
                        par.Mode = "ecb";
                        par.InputFilePath = Constants.OUTPUT_FILE_PATH;
                        par.OutputFilePath = Constants.OUTPUT_FILE_PATH2;
                        var enc = new Encrypt(par);
                        enc.Execute();*/
            /*
                        byte x = 0xb7;
                        byte y = 0xa7;

                        int xx = x;
                        int yy = y;

                        int ans = xx ^ yy;
                        byte aa = (byte)ans;

                        Console.WriteLine(aa);
            */
            Console.Read();



        }
    }
}
