using AES.Shared.FileReader;
using AES.Shared.utility;
using AES.Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Shared.S_Box
{
    public sealed class SBox
    {

        private byte[][] box;

        private int sBoxSize = 16;

        private Dictionary<char, byte> byteMap = new Dictionary<char, byte>();

        public static SBox sBoxInstance = null;

        private SBox()
        {
            box = new byte[sBoxSize][];
            for(int i = 0; i < sBoxSize; i++)
            {
                box[i] = new byte[sBoxSize];
            }
            InitializeSBox();
        }

        public static SBox GetSBoxInstance
        {
            get
            {
                if (sBoxInstance == null)
                {
                    sBoxInstance = new SBox();
                }
                return sBoxInstance;
            }
        }
        public byte GetSBoxByte(byte value)
        {
            int row = GetRowNumber(value);
            int col = GetColumnNumber(value);

            return box[row][col];
        }
        private int GetColumnNumber(int value)
        {
            int row = GetRowNumber(value);
            int tempLeft = row << 4;
            return value ^ tempLeft;
        }

        private int GetRowNumber(int value)
        {
            return value >> 4;
        }
        private void InitializeSBox()
        {
            byteMap.Add('0', 0x0);
            byteMap.Add('1', 0x1);
            byteMap.Add('2', 0x2);
            byteMap.Add('3', 0x3);
            byteMap.Add('4', 0x4);
            byteMap.Add('5', 0x5);
            byteMap.Add('6', 0x6);
            byteMap.Add('7', 0x7);
            byteMap.Add('8', 0x8);
            byteMap.Add('9', 0x9);
            byteMap.Add('a', 0xA);
            byteMap.Add('b', 0xB);
            byteMap.Add('c', 0xC);
            byteMap.Add('d', 0xD);
            byteMap.Add('e', 0xE);
            byteMap.Add('f', 0xf);

            DataReader dReader = new DataReader();
            List<string[]> lines = dReader.GetLinesOfWordsFromFile(Constants.S_BOX_FILE_PATH);

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    box[i][j] = GetByte(lines[i][j]);
                }
            }
        }

        private byte GetByte(string value)
        {
            int first = byteMap[value[0]] << 4;
            int second = byteMap[value[1]];
            int result = first ^ second;
            return Convert.ToByte(result);
        }
    }
}
