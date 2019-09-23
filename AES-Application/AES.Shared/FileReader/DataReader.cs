using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AES.Shared.mixColumn;
using AES.Shared.s_Box;

namespace AES.Shared.FileReader
{
    public class DataReader
    {
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
    }
}
