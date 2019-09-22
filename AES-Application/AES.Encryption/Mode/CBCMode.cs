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
    public class CBCMode : EncryptionRoundStep, IEncryptionMode
    {
        private Key keyInstance;

        public CBCMode() : base()
        {

        }

        public byte[][] EncryptionRoundIteration(byte[][] currentStage)
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

        public void EncryptFile(string inputFilePath, string outputFilePath, string initialVector)
        {
            
        }

        public void EncryptText(string text, string initialVector)
        {
            byte[][] input = Util.MatrixTranspose(Util.Convert1Dto2DArray(Encoding.ASCII.GetBytes(text)));
            byte[][] iv = Util.MatrixTranspose(Util.Convert1Dto2DArray(Encoding.ASCII.GetBytes(initialVector)));
            byte[][] currentStage = AddRoundKey(input,iv);
            Util.PrintHex(EncryptionRoundIteration(currentStage));
        }

        public void ExpandEncryptionKey(byte[] key)
        {
            keyInstance = Key.GetKeyInstance;
            keyInstance.InitializeKey(key);
        }
    }
}
