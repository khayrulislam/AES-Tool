using AES.Shared.Interface;
using AES.Shared.steps;
using AES.Shared.KeyExpand;
using AES.Shared.utility;
using AES.Shared.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.EncryptOrDecrypt.mode
{
    public class CBCModeDecrypt : EncryptDecryptRoundStep, IEncryptDecryptMode
    {
        private Parameter parameter;
        private Key keyInstance;
        public void ExecuteFileOperation()
        {
            long fileBlock = GetFileBlockSize(@parameter.InputFilePath);
            byte[][] initialVector = Util.Convert1Dto2DArrayColumnWise(Encoding.ASCII.GetBytes(parameter.InitialVector));
            byte[] inputBlock;
            this.fileCreate = true;
            byte[][] decypher,plainText;

            for (int i = 0; i < fileBlock; i++)
            {
                inputBlock = FileRead(@parameter.InputFilePath, i * Constants.INPUT_BLOCK_SIZE);
                decypher = DecryptBlock(inputBlock);
                plainText = AddRoundKey(decypher, initialVector);
                FileWrite(Util.Convert2dTo1DArrayColumnWise(plainText), @parameter.OutputFilePath);
                Util.Print2DHex(initialVector);
                initialVector = Util.Convert1Dto2DArrayColumnWise(inputBlock);
            }
        }

        private byte[][] DecryptBlock(byte[] block)
        {
            byte[][] input = Util.Convert1Dto2DArrayColumnWise(block);
            return DecryptRoundIteration(input);
        }

        private byte[][] DecryptRoundIteration(byte[][] currentStage)
        {
            currentStage = AddRoundKey(currentStage, keyInstance.GetRoundKey(10));
            for (int i = 9; i >= 0; i--)
            {
                currentStage = ShiftRow(currentStage);
                currentStage = SubstituteByte(currentStage);                
                currentStage = AddRoundKey(currentStage, keyInstance.GetRoundKey(i));
                if (i != 0) currentStage = MixColumnOperation(currentStage);
            }
            return currentStage;
        }
        public void ExecuteTextOperation()
        {

        }

        public void InitializeMode(Parameter param)
        {
            this.parameter = param;
            this.isInverse = true;
            ExpandDecryptKey(Encoding.ASCII.GetBytes(parameter.Key));
        }
        private void ExpandDecryptKey(byte[] key)
        {
            keyInstance = Key.GetKeyInstance;
            keyInstance.InitializeKey(key);
        }
    }
}
