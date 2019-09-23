using AES.Shared.FileReader;
using AES.Shared.s_Box;
using AES.Shared.utility;
using AES.Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Shared.KeyExpand
{
    
    public sealed class Key
    {
        private byte[][] KeyWords;

        private int NumberOfKeyWords = 44;
        private byte[][] KeyConstant;
        private static Key KeyInstance = null;
        private SBox sBoxInstance = null;
        private Key()
        {
            KeyWords = new byte[NumberOfKeyWords][];
            InitializeKeyConstant();
            sBoxInstance = SBox.GetSBoxInstance;
        }

        private void InitializeKeyConstant()
        {
            DataReader dReader = new DataReader();
            List<string[]> lines = dReader.GetLinesOfWordsFromFile(Constants.KEY_CONSTANT_FILE_PATH);

            this.KeyConstant = new byte[lines.Count][];

            for (int i = 0; i < lines.Count; i++)
            {
                this.KeyConstant[i] = new byte[lines[i].Length];
                for (int j = 0; j < lines[i].Length; j++)
                {
                    this.KeyConstant[i][j] = Util.GetByte(lines[i][j]);
                }
            }

        }

        public static Key GetKeyInstance
        {
            get
            {
                if (KeyInstance == null)
                {
                    KeyInstance = new Key();
                }
                return KeyInstance;
            }
        }

        // initialize the key in four row as word
        // input key in one dimension array
        public void InitializeKey(byte []initialKey)
        {
            byte [][] initialKeyWords = Util.Convert1Dto2DArray(initialKey);

            for (int i = 0; i < initialKeyWords.Length; i++)
            {
                KeyWords[i] = initialKeyWords[i];
            }
            ExpandKey();
        }

        public byte[][] GetRoundKey(int roundNumber)
        {
            byte[][] key = Util.Initialize2DArray();
            for(int i = 0; i < Constants.BLOCK_COLUMN_SIZE; i++)
            {
                key[i] = KeyWords[Constants.BLOCK_COLUMN_SIZE * roundNumber + i];
            }
            return Util.MatrixTranspose(key);
        }

        private void ExpandKey()
        {
            for(int i = 4; i < NumberOfKeyWords; i++)
            {
                byte[] previousWord = new byte[4];
                previousWord = KeyWords[i-1];

                if(i%4 == 0)
                {
                    previousWord = GetGResult(previousWord,i/4-1);
                }
                KeyWords[i] = Util.WordXOR(previousWord, KeyWords[i-4]);
            }
        }

        private byte[] GetGResult(byte[] word, int keyConstantNumber)
        {
            // one byte left shift;
            byte[] result = Util.ShiftRow(word,1);

            // substitute byte
            for(int i = 0; i < result.Length; i++)
            {
                result[i] = sBoxInstance.GetSubstituteByte(result[i],false);
            }
            // xor with constant
            result = Util.WordXOR(result, this.KeyConstant[keyConstantNumber]);

            return result;
        }
    }
}
