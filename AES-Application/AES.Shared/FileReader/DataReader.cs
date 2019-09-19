using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AES.Shared.mixColumn;
using AES.Shared.S_Box;

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

        public void ReadMatrixConstant()
        {
            string filePath = "../../../AES.shared/MixColumn/Matrixconstant.txt";
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                List<string[]> allLine = new List<string[]>();

                foreach (string line in lines)
                {
                    string[] words = line.Split('\t');
                    allLine.Add(words);
                }

                MixColumn mixcolumnInstance = MixColumn.GetMixColumnInstance;
                mixcolumnInstance.StoreMatrix(allLine);
            }
            else
            {
                Console.WriteLine("S-Box File not found!!!!");
            }
        }
    }
}
