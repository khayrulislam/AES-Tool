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
        public void ReadSBoxData()
        {
            string filePath = "../../../AES.shared/S-Box/s-box.txt";
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                List<string[]> allLine = new List<string[]>();

                foreach (string line in lines)
                {
                    string[] words = line.Split('\t');
                    allLine.Add(words);
                }

                SBox sBoxInstance = SBox.GetSBoxInstance;
                sBoxInstance.StoreSBox(allLine);
            }
            else
            {
                Console.WriteLine("S-Box File not found!!!!");
            }
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
