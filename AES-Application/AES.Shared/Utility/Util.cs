using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Shared.Utility
{
    public static class Util
    {
        public static byte[] ShiftRow(byte[] row, int shiftCount)
        {
            byte[] rowShiftResult = new byte[row.Length];
            int length = row.Length;

            for (int i = 0; i < length; i++)
            {
                rowShiftResult[i] = row[(i + shiftCount) % length];
            }

            return rowShiftResult; ;
        }

        public static int GetColumnNumber(int value)
        {
            int row = GetRowNumber(value);
            int tempLeft = row << 4;
            return value ^ tempLeft;
        }

        public static int GetRowNumber(int value)
        {
            return value >> 4;
        }

        public static byte[] WordXOR(byte[] word1, byte[] word2)
        {
            byte[] result = new byte[word1.Length];
            for(int i=0;i< word1.Length; i++)
            {
                int word1Byte = word1[i];
                int word2Byte = word2[i];
                result[i] = Convert.ToByte(word1Byte ^ word2Byte);
            }
            return result;
        }

    }
}
