using AES.Shared.mixColumn;
using AES.Shared.s_Box;
using AES.Shared.utility;
using AES.Shared.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Shared.steps
{
    public class EncryptDecryptRoundStep
    {
        private SBox sBoxInstance;

        private MixColumn mixColumnInstance;

        public bool isInverse;

        public bool fileCreate;

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

        public void FileWrite(byte[] output,string filePath)
        {
            if (fileCreate)
            {
                CreateFile(filePath);
                fileCreate = false;
            }
            using (FileStream fs = new FileStream(@filePath, FileMode.Append))
            {
                fs.Write(output, 0, output.Length);
                fs.Close();
            }
            
        }

        private void CreateFile(string filePath)
        {
            FileStream fs;
            if (File.Exists(@filePath))
            {
                File.Delete(@filePath);
            }
            fs = File.Create(@filePath);
            fs.Close();
        }
    }
}
