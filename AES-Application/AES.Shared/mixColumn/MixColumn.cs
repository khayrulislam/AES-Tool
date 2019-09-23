﻿using AES.Shared.FileReader;
using AES.Shared.utility;
using AES.Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Shared.mixColumn
{
    public sealed class MixColumn
    {
        public static MixColumn mixColumnInstance = null;

        private int[][] matrix;
        private int[][] inverseMatrix;
        private MixColumn()
        {
            InitializeMixColumnMatrix();
        }

        private void InitializeMixColumnMatrix()
        {
            DataReader dReader = new DataReader();
            List<string[]> lines = dReader.GetLinesOfWordsFromFile(Constants.MIX_COLUMN_FILE_PATH);
            List<string[]> inverseLines = dReader.GetLinesOfWordsFromFile(Constants.INVERSE_MIX_COLUMN_FILE_PATH);

            matrix = new int[Constants.BLOCK_ROW_SIZE][];
            inverseMatrix = new int[Constants.BLOCK_ROW_SIZE][];

            for (int i = 0; i < lines.Count; i++)
            {
                matrix[i] = new int[lines[i].Length];
                inverseMatrix[i] = new int[inverseLines[i].Length];
                for (int j = 0; j < lines[i].Length; j++)
                {
                    matrix[i][j] = Convert.ToInt32(lines[i][j]);
                    inverseMatrix[i][j] = Convert.ToInt32(inverseLines[i][j]);
                }
            }
        }

        public static MixColumn GetMixColumnInstance
        {
            get
            {
                if (mixColumnInstance == null)
                {
                    mixColumnInstance = new MixColumn();
                }
                return mixColumnInstance;
            }
        }

        // matrix multiplication with defined matrix
        public byte[][] CalculateMixColumn(byte[][] input, bool isInverse)
        {
            byte[][] result = Util.Initialize2DArray();
            
            for (int i = 0; i < Constants.BLOCK_ROW_SIZE; i++)
            {
                for (int j = 0; j < Constants.BLOCK_COLUMN_SIZE; j++)
                {
                    byte[] col = new byte[Constants.BLOCK_COLUMN_SIZE];
                    for (int k = 0; k < Constants.BLOCK_COLUMN_SIZE; k++)
                    {
                        col[k] = input[k][j];
                    }
                    result[i][j] = isInverse? Multiplication(inverseMatrix[i], col) : Multiplication(matrix[i], col);
                }
            }
            return result;
        }

        private byte Multiplication(int[] matrixRow, byte[] col)
        {
            int result = 0;
            int iterativeValue;
            for(int i = 0; i < Constants.BLOCK_COLUMN_SIZE; i++)
            {
                iterativeValue = col[i];
                int tempResult = 0;
                int num = matrixRow[i];
                while (num!=0)
                {
                    if (num % 2 != 0)
                    {
                        tempResult ^= iterativeValue;
                    }
                    num /= 2;
                    iterativeValue = Multiply2(iterativeValue);
                }
                result ^= tempResult;
            }
            return Convert.ToByte(result); 
        }

        private int Multiply2(int value)
        {
            int constant = Constants.MIX_COLUMN_CONSTANT;
            // check 8th bit is 0 or not
            // if not multiply with two and xor with constant
            if (value >> 7 == 0)
            {
                constant = 0;
            }
            else
            {
                value ^= 1 << 7; 
            }
            return value << 1 ^ constant;
        }

    }
}
