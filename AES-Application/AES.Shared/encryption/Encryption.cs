using AES.Shared.KeyExpand;
using AES.Shared.mixColumn;
using AES.Shared.S_Box;
using AES.Shared.utility;
using AES.Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Shared.encryption
{
    public class Encryption
    {
        byte[][] plainText;

        private SBox sBoxInstance;

        private MixColumn mixColumnInstance;

        private Key keyInstance;

        public Encryption()
        {
            sBoxInstance = SBox.GetSBoxInstance;
            mixColumnInstance = MixColumn.GetMixColumnInstance;
            keyInstance = Key.GetKeyInstance;

        }

        public void Encrypt(byte[] textArray)
        {
            plainText = new byte[Constants.BLOCK_COLUMN_SIZE][];
            plainText = Util.Convert1Dto2DArray(textArray);
            
            // initial add round key execute using 0 round key
            StartEncryptionRound(AddRoundKey(plainText, keyInstance.GetRoundKey(0)));
        }

        private byte[][] StartEncryptionRound(byte[][] result)
        {
            for(int i = 1; i <= 10; i++)
            {
                result = SubstituteByte(result);
                result = ShiftRow(result);
                if(i != 10 )result = mixColumnInstance.CalculateMixColumn(result);
                result = AddRoundKey(result, keyInstance.GetRoundKey(i));
            }
            return result;
        }

        private byte[][] ShiftRow(byte[][] result)
        {
            for(int i = 1; i < Constants.BLOCK_ROW_SIZE; i++)
            {
                result[i] = Util.ShiftRow(result[i], i);
            }
            return result;
        }

        private byte[][] SubstituteByte(byte[][] result)
        {

            for(int i = 0; i < Constants.BLOCK_ROW_SIZE; i++)
            {
                for(int j = 0; j < Constants.BLOCK_COLUMN_SIZE; j++)
                {
                    result[i][j] = sBoxInstance.GetSBoxByte(result[i][j]);
                }
            }
            return result;
        }

        private byte[][] AddRoundKey(byte[][] plainText, byte[][] key)
        {
            byte[][] result = new byte[Constants.BLOCK_ROW_SIZE][];

            for(int i = 0; i < Constants.BLOCK_ROW_SIZE; i++)
            {
                result[i] = Util.WordXOR(plainText[i],key[i]);
            }
            return result;
        }
    }
}
