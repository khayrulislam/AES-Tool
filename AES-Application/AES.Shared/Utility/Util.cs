using AES.Shared.utility;
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

            for (int i = 0; i < row.Length; i++)
            {
                rowShiftResult[i] = row[(i + shiftCount) % row.Length];
            }

            return rowShiftResult;
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

        public static byte[][] Initialize2DArray()
        {
            byte[][] array2d = new byte[Constants.BLOCK_ROW_SIZE][];

            for (int i = 0; i < Constants.BLOCK_ROW_SIZE; i++)
            {
                array2d[i] = new byte[Constants.BLOCK_COLUMN_SIZE];
            }
            return array2d;
        } 

        public static byte[][] Convert1Dto2DArray(byte[] array)
        {
            byte[][] array2d = Initialize2DArray();

            for (int i = 0; i < array.Length; i++)
            {
                array2d[i / Constants.BLOCK_ROW_SIZE][i % Constants.BLOCK_COLUMN_SIZE] = array[i];
            }
            return array2d;
        }

        public static void PrintHex(byte[][] result)
        {
            for(int i = 0; i < Constants.BLOCK_ROW_SIZE; i++)
            {
                for(int j = 0; j < Constants.BLOCK_COLUMN_SIZE; j++)
                {
                    Console.Write("{0:X} ", result[i][j]);
                }
            }
            Console.WriteLine();
        }

    }
}
