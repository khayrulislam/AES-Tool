﻿using AES.EncryptDecrypt.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.EncryptDecrypt.utility
{
    public static class Util
    {
        private static Dictionary<char, byte> byteMap = new Dictionary<char, byte> {
        {'0', 0x0},{'1', 0x1},{'2', 0x2},{'3', 0x3},{'4', 0x4},{'5', 0x5},{'6', 0x6},{'7', 0x7},
        {'8', 0x8},{'9', 0x9},{'a', 0xA},{'b', 0xB},{'c', 0xC},{'d', 0xD},{'e', 0xE},{'f', 0xF},
        };

        public static byte[] ShiftRow(byte[] row, int shiftCount, bool isRightShift)
        {
            byte[] rowShiftResult = new byte[row.Length];

            for (int i = 0; i < row.Length; i++)
            {
                int pos = isRightShift ? ( ( (shiftCount*2 )% row.Length + i + shiftCount) % row.Length ): ((i + shiftCount) % row.Length) ; 
                rowShiftResult[i] = row[pos];
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
            byte[][] array2d = new byte[Properties.Settings.Default.BLOCK_ROW_SIZE][];

            for (int i = 0; i < Properties.Settings.Default.BLOCK_ROW_SIZE; i++)
            {
                array2d[i] = new byte[Properties.Settings.Default.BLOCK_COLUMN_SIZE];
            }
            return array2d;
        } 

        public static byte[][] Transform1Dto2DArray(byte[] array)
        {
            byte[][] array2d = Initialize2DArray();

            for (int i = 0; i < array.Length; i++)
            {
                array2d[i % Properties.Settings.Default.BLOCK_COLUMN_SIZE][i / Properties.Settings.Default.BLOCK_ROW_SIZE] = array[i];
            }
            return array2d;
        }

        public static void Print2DHex(byte[][] result)
        {
            for(int i = 0; i < Properties.Settings.Default.BLOCK_ROW_SIZE; i++)
            {
                for(int j = 0; j < Properties.Settings.Default.BLOCK_COLUMN_SIZE; j++)
                {
                    Console.Write("{0:X2} ", result[i][j]);
                }
            }
            Console.WriteLine();
        }

        public static void Print1DHex(byte[] result)
        {
            for (int i = 0; i < result.Length; i++)
            {
                Console.Write("{0:X2} ", result[i]);
            }
            Console.WriteLine();
        }

        public static byte GetByte(string value)
        {
            int first = byteMap[value[0]] << 4;
            int second = byteMap[value[1]];
            int result = first ^ second;
            return Convert.ToByte(result);
        }

        public static byte[][] MatrixTranspose(byte[][] input)
        {
            byte[][] result = Initialize2DArray();
            
            for(int i = 0; i < Properties.Settings.Default.BLOCK_ROW_SIZE; i++)
            {
                for(int j = 0; j < Properties.Settings.Default.BLOCK_COLUMN_SIZE; j++)
                {
                    result[i][j] = input[j][i];
                }
            }
            return result;
        }

        // create 2d array column wise
        public static byte[] Transform2dTo1DArray(byte[][] input)
        {
            byte[] result = new byte[Properties.Settings.Default.BLOCK_ROW_SIZE * Properties.Settings.Default.BLOCK_COLUMN_SIZE];

            for (int i = 0; i < Properties.Settings.Default.BLOCK_ROW_SIZE; i++)
            {
                for (int j = 0; j < Properties.Settings.Default.BLOCK_COLUMN_SIZE; j++)
                {
                    result[Properties.Settings.Default.BLOCK_ROW_SIZE * i + j] = input[j][i];
                }
            }
            return result;
        }

    }
}
