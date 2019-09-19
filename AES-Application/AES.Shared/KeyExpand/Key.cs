using AES.Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Shared.KeyExpand
{
    
    public class Key
    {
        private byte[][] KeyWords;

        private int NumberOfKeyWords = 44;
        private byte KeyConstant = 0x40;
        private static Key KeyInstance = null;
        private Key()
        {
            KeyWords = new byte[NumberOfKeyWords][];
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
            byte[] PreviousWord = new byte[4];

            for(int i = 4; i < NumberOfKeyWords; i++)
            {
                PreviousWord = KeyWords[i-1];
                if(i%4 == 0)
                {
                    PreviousWord = GetGResult(PreviousWord);
                }
                //KeyWords[i] = 
            }
        }

        private byte[] GetGResult(byte[] previousWord)
        {
            // one byte left shift;
            byte[] rowShift = Util.ShiftRow(previousWord,1);

            return null;
        }
    }
}
