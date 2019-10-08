using AES.EncryptDecrypt.Interface;
using AES.EncryptDecrypt.steps;
using AES.EncryptDecrypt.KeyExpand;
using AES.EncryptDecrypt.utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.EncryptDecrypt.mode
{
    public class CBC_Decryption : EncryptDecryptRoundStep, IEncryptDecryptMode
    {
        private Parameter parameter;
        private Key keyInstance;
        public void ExecuteFileOperation()
        {
            long fileBlock = GetFileBlockSize(@parameter.InputFilePath);
            byte[][] initialVector = Util.Transform1Dto2DArray(Encoding.ASCII.GetBytes(parameter.InitialVector));
            byte[] inputBlock, plainTextArray;
            this.isNotOutputFileExist = true;
            byte[][] decypher,plainText;

            for (int i = 0; i < fileBlock; i++)
            {
                inputBlock = FileRead(@parameter.InputFilePath, i * Constants.INPUT_BLOCK_SIZE);
                decypher = DecryptBlock(inputBlock);
                plainText = AddRoundKey(decypher, initialVector);
                initialVector = Util.Transform1Dto2DArray(inputBlock);
                plainTextArray = Util.Transform2dTo1DArray(plainText);
                if (i + 1 == fileBlock) plainTextArray = RemovePadding( plainTextArray );
                FileWrite(plainTextArray, @parameter.OutputFolderPath);
                Util.Print2DHex(initialVector);
            }
        }

        private byte[][] DecryptBlock(byte[] block)
        {
            byte[][] input = Util.Transform1Dto2DArray(block);
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
      /*  public void ExecuteTextOperation()
        {

        }*/

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
