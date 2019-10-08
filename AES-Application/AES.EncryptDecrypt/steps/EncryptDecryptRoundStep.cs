using AES.EncryptDecrypt.FileReader;
using AES.EncryptDecrypt.mixColumn;
using AES.EncryptDecrypt.s_Box;
using AES.EncryptDecrypt.utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.EncryptDecrypt.steps
{
    public class EncryptDecryptRoundStep : File_Reader
    {
        private SBox sBoxInstance;

        private MixColumn mixColumnInstance;

        public bool isInverse;


        // create sbox and mixcolumn instance for next use
        public EncryptDecryptRoundStep()
        {
            sBoxInstance = SBox.GetSBoxInstance;
            mixColumnInstance = MixColumn.GetMixColumnInstance;
        }

        // substitute byte using sbox
        public byte[][] SubstituteByte(byte[][] currentStage)
        {
            for (int i = 0; i < Constants.BLOCK_ROW_SIZE; i++)
            {
                for (int j = 0; j < Constants.BLOCK_COLUMN_SIZE; j++)
                {
                    currentStage[i][j] = sBoxInstance.GetSubstituteByte(currentStage[i][j],isInverse);
                }
            }
            return currentStage;
        }

        // shift row circular base on the row number
        public byte[][] ShiftRow(byte[][] currentStage)
        {
            for (int i = 1; i < Constants.BLOCK_ROW_SIZE; i++)
            {
                currentStage[i] = Util.ShiftRow(currentStage[i], i, isInverse);
            }
            return currentStage;
        }

        // multiply with a fix matrix
        public byte[][] MixColumnOperation(byte[][] currentStage)
        {
            return mixColumnInstance.CalculateMixColumn(currentStage, isInverse);
        }

        // Add round key general xor operation on text byte and key
        public byte[][] AddRoundKey(byte[][] currentStage, byte[][] key)
        {
            byte[][] result = new byte[Constants.BLOCK_ROW_SIZE][];

            // bit wise xor operation with currentStage and key
            for (int i = 0; i < Constants.BLOCK_ROW_SIZE; i++)
            {
                result[i] = Util.WordXOR(currentStage[i], key[i]);
            }
            return result;
        }

        public byte[] AddPadding(byte[] input, int length)
        {
            byte[] paddingResult = new byte[16];

            Array.Copy(input, 0, paddingResult, 0, length);

            for (int i = length; i < 16; i++)
            {
                paddingResult[i] = 0;
            }
            paddingResult[15] = (byte)(16 - length);

            return paddingResult;
        }


    }
}
