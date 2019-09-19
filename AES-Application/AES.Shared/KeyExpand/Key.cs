﻿using AES.Shared.S_Box;
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
        private byte[] KeyConstant;
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
            this.KeyConstant = new byte[4];
            this.KeyConstant[0] = 0x1;
            this.KeyConstant[1] = 0x0;
            this.KeyConstant[2] = 0x0;
            this.KeyConstant[3] = 0x0;
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
        public void InitializeKey(byte [][]initialKey)
        {
            for(int i=0;i<initialKey.Length;i++)
            {
                KeyWords[i] = initialKey[i];
            }
            ExpandKey();
        }

        private void ExpandKey()
        {
            byte[] previousWord = new byte[4];

            for(int i = 4; i < NumberOfKeyWords; i++)
            {
                previousWord = KeyWords[i-1];

                if(i%4 == 0)
                {
                    previousWord = GetGResult(previousWord);
                }
                KeyWords[i] = Util.WordXOR(previousWord, KeyWords[i-4]);
            }
        }

        private byte[] GetGResult(byte[] word)
        {
            // one byte left shift;
            byte[] result = Util.ShiftRow(word,1);

            // substitute byte
            for(int i = 0; i < result.Length; i++)
            {
                int row = Util.GetRowNumber(result[i]);
                int col = Util.GetColumnNumber(result[i]);
                result[i] = sBoxInstance.GetSBoxByte(row,col);
            }

            // xor with constant
            result = Util.WordXOR(result, this.KeyConstant);

            return result;
        }
    }
}
