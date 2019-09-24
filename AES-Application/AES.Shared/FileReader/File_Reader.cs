﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AES.Shared.mixColumn;
using AES.Shared.s_Box;
using AES.Shared.utility;

namespace AES.Shared.FileReader
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
                if (bytesRead < 16)
                {
                    int nullByte = 0;
                    foreach (byte singleByte in byteArray)
                    {
                        if (singleByte == 0x00) nullByte++;
                    }
                    //byteArray[byteArray.Length - 1] = Convert.ToByte(nullByte - 1);
                }
            }
            return byteArray;
        }

        public long GetFileBlockSize(string @filePath)
        {
            FileInfo info = new FileInfo(filePath);
            if (info.Length % Constants.INPUT_BLOCK_SIZE != 0) return 1 + info.Length / Constants.INPUT_BLOCK_SIZE;
            return info.Length / Constants.INPUT_BLOCK_SIZE;
        }
    }
}