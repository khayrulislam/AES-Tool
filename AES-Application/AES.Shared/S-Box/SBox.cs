using AES.Shared.FileReader;
using AES.Shared.utility;
using AES.Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Shared.s_Box
{
    public sealed class SBox
    {
        private byte[][] sBox;
        private byte[][] inverseSBox;

        public static SBox sBoxInstance = null;

        private SBox()
        {
            InitializeBox();
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
        public byte GetSubstituteByte(byte value,bool isInverse)
        {
            int row = GetRowNumber(value);
            int col = GetColumnNumber(value);
            return (isInverse ? inverseSBox[row][col] : sBox[row][col]);
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
        private void InitializeBox()
        {
            InitializeArray();

            DataReader dReader = new DataReader();
            List<string[]> sBoxlines = dReader.GetLinesOfWordsFromFile(Constants.S_BOX_FILE_PATH);
            List<string[]> inverseSBoxlines = dReader.GetLinesOfWordsFromFile(Constants.INVERSE_S_BOX_FILE_PATH);

            for (int i = 0; i < sBoxlines.Count; i++)
            {
                for (int j = 0; j < sBoxlines[i].Length; j++)
                {
                    sBox[i][j] = Util.GetByte(sBoxlines[i][j]);
                    inverseSBox[i][j] = Util.GetByte(inverseSBoxlines[i][j]);
                }
            }
        }

        private void InitializeArray()
        {
            sBox = new byte[Constants.BOX_SIZE][];
            inverseSBox = new byte[Constants.BOX_SIZE][];
            for (int i = 0; i < Constants.BOX_SIZE; i++)
            {
                sBox[i] = new byte[Constants.BOX_SIZE];
                inverseSBox[i] = new byte[Constants.BOX_SIZE];
            }
        }
    }
}
