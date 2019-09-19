using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Shared.FileReader
{
    public class DataReader
    {
        public void ReadSBoxData()
        {
            string filePath = "";
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                List<string[]> allLine = new List<string[]>();

                foreach (string line in lines)
                {
                    string[] words = line.Split(',');
                    allLine.Add(words);
                }

            }
            else
            {
                Console.WriteLine("S-Box File not found!!!!");
            }
        }
    }
}
