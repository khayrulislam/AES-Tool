using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AES.EncryptDecrypt.mixColumn;
using AES.EncryptDecrypt.s_Box;
using AES.EncryptDecrypt.utility;

namespace AES.EncryptDecrypt.FileReader
{
    public class File_Reader
    {
        public bool isNotOutputFileExist;

        public List<string[]> GetLinesOfWordsFromFile(string filePath)
        {
            List<string[]> allLine = new List<string[]>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] words = line.Split('\t');
                    allLine.Add(words);
                }
            }
            else
            {
                Console.WriteLine("File not found!!!!");
            }
            return allLine;
        }

        public void FileWrite(byte[] output, string filePath)
        {
            if (isNotOutputFileExist)
            {
                CreateFile(filePath);
                isNotOutputFileExist = false;
            }
            using (FileStream fs = new FileStream(@filePath, FileMode.Append))
            {
                fs.Write(output, 0, output.Length);
                fs.Close();
            }

        }
        private void CreateFile(string filePath)
        {
            FileStream fs;
            if (File.Exists(@filePath))
            {
                File.Delete(@filePath);
            }
            fs = File.Create(@filePath);
            fs.Close();
        }

        public byte[] FileRead(string filePath, int startPosition)
        {
            byte[] byteArray = new byte[Constants.INPUT_BLOCK_SIZE];

            using (FileStream fileStram = new FileStream(@filePath, FileMode.Open, FileAccess.Read))
            {
                fileStram.Seek(startPosition, SeekOrigin.Begin);
                int bytesRead = fileStram.Read(byteArray, 0, Constants.INPUT_BLOCK_SIZE);
                if (bytesRead < Constants.INPUT_BLOCK_SIZE)
                {
                    int nullByteCount = Constants.INPUT_BLOCK_SIZE - bytesRead;
                    byteArray[byteArray.Length - 1] = Convert.ToByte(nullByteCount);
                }
            }
            return byteArray;
        }

        public byte[] RemovePadding(byte[] plainText)
        {
            int paddingLength = plainText[Constants.INPUT_BLOCK_SIZE - 1];
            if (paddingLength > 15) paddingLength = 0;
            byte[] output = new byte[Constants.INPUT_BLOCK_SIZE - paddingLength ];
            for (int i = 0; i < output.Length; i++) output[i] = plainText[i];
            return output;
        }

        public long GetFileBlockSize(string @filePath)
        {
            FileInfo info = new FileInfo(filePath);
            if (info.Length % Constants.INPUT_BLOCK_SIZE != 0) return 1 + info.Length / Constants.INPUT_BLOCK_SIZE;
            return info.Length / Constants.INPUT_BLOCK_SIZE;
        }
    }
}
