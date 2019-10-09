using AES.EncryptDecrypt.FileReader;
using AES.EncryptDecrypt.s_Box;
using AES.EncryptDecrypt.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.EncryptDecrypt.KeyExpand
{
    
    public sealed class Key
    {
        private byte[][] KeyWords;

        private byte[][] KeyConstant;
        private static Key KeyInstance = null;
        private SBox sBoxInstance = null;
        private Key()
        {
            KeyWords = new byte[Properties.Settings.Default.NUMBER_OF_KEY_WORDS][];
            InitializeKeyConstant();
            sBoxInstance = SBox.GetSBoxInstance;
        }

        private void InitializeKeyConstant()
        {
            File_Reader dReader = new File_Reader();
            List<string[]> lines = dReader.GetWordList(Properties.Resources.key);

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
            byte [][] initialKeyWords = Util.MatrixTranspose(Util.Transform1Dto2DArray(initialKey)) ;
            for (int i = 0; i < initialKeyWords.Length; i++)
            {
                KeyWords[i] = initialKeyWords[i];
            }
            ExpandKey();
        }

        public byte[][] GetRoundKey(int roundNumber)
        {
            byte[][] key = Util.Initialize2DArray();
            for(int i = 0; i < Properties.Settings.Default.BLOCK_COLUMN_SIZE; i++)
            {
                key[i] = KeyWords[Properties.Settings.Default.BLOCK_COLUMN_SIZE * roundNumber + i];
            }
            return Util.MatrixTranspose(key);
        }

        private void ExpandKey()
        {
            for(int i = 4; i < Properties.Settings.Default.NUMBER_OF_KEY_WORDS; i++)
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
            byte[] result = Util.ShiftRow(word,1, false);

            // substitute byte
            for(int i = 0; i < result.Length; i++)
            {
                result[i] = sBoxInstance.GetSubstituteByte(result[i],false); // use sbox substitution
            }
            // xor with constant
            result = Util.WordXOR(result, this.KeyConstant[keyConstantNumber]);

            return result;
        }
    }
}
