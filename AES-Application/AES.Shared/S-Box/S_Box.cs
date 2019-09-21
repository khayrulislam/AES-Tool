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
            DataReader dReader = new DataReader();
            List<string[]> lines = dReader.GetLinesOfWordsFromFile(Constants.S_BOX_FILE_PATH);

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    box[i][j] = Util.GetByte(lines[i][j]);
                }
            }
        }

    }
}
