using AES.Encryption.encrypt;
using AES.Encryption.Interface;
using AES.Encryption.steps;
using AES.Shared.KeyExpand;
using AES.Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Encryption.Mode
{
    public class ECBMode : EncryptionRoundStep,IEncryptionMode
    {
        private Key keyInstance;
        Parameter param;
        public ECBMode(): base()
        {
                
        }

        private byte[][] EncryptionRoundIteration(byte[][] currentStage)
        {
            currentStage = AddRoundKey(currentStage, keyInstance.GetRoundKey(0));
            for (int i = 1; i <= 10; i++)
            {
                currentStage = SubstituteByte(currentStage);
                currentStage = ShiftRow(currentStage);
                if (i != 10) currentStage = MixColumnOperation(currentStage);
                currentStage = AddRoundKey(currentStage, keyInstance.GetRoundKey(i));
            }
            return Util.MatrixTranspose(currentStage);
        }

        public void ExpandEncryptionKey(byte[] key)
        {
            keyInstance = Key.GetKeyInstance;
            keyInstance.InitializeKey(key);
        }

        public void EncryptFile(string inputFilePath, string outputFilePath, string initialVector)
        {

        }

        public void EncryptText(string text, string initialVector)
        {
            byte[] textByteArray= Encoding.ASCII.GetBytes(text);
            byte[] block = new byte[16];
            int length = textByteArray.Length;

            for(int i = 0; i < length; i += 16)
            {
                if(i+16 > length)
                {
                    Array.Clear(block,0,16);
                    Array.Copy(textByteArray,i,block,0,length-i);
                    block = AddPadding(block, length-i);
                }
                else
                {
                    Array.Copy(textByteArray,i,block,0,16);
                }
                byte[][] input = Util.MatrixTranspose(Util.Convert1Dto2DArray(block));
                Util.PrintHex(EncryptionRoundIteration(input));
            }
        }

        
    }
}
