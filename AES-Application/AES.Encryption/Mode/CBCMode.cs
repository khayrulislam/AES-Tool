using AES.Shared.steps;
using AES.Shared.KeyExpand;
using AES.Shared.Interface;
using AES.Shared.utility;
using AES.Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AES.EncryptOrDecrypt.Mode
{
    public class CBCModeEecrypt : EncryptDecryptRoundStep, IEncryptDecryptMode
    {
        private Key keyInstance;
        private Parameter parameter;
        public CBCModeEecrypt() : base()
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

        public void ExecuteFileOperation()
        {
            using (FileStream fileStram = new FileStream(@parameter.InputFilePath, FileMode.Open, FileAccess.Read))
            {
                byte[] inputBufferByte = new byte[Constants.INPUT_BLOCK_SIZE];
                fileStram.Seek(0, SeekOrigin.Begin);
                int bytesRead = fileStram.Read(inputBufferByte, 0, Constants.INPUT_BLOCK_SIZE);
                byte[][] iv = Util.MatrixTranspose(Util.Convert1Dto2DArray(Encoding.ASCII.GetBytes(parameter.InitialVector)));
                this.fileCreate = true;

                while (bytesRead > 0)
                {
                    // encrypted cypher byte array in 2d foramte;
                    iv = EncryptBlock(inputBufferByte,iv);
                    Array.Clear(inputBufferByte, 0, 16);
                    FileWrite(Util.Convert2dTo1DArray(iv), parameter.OutputFilePath);
                    Util.Print2DHex(iv);
                    bytesRead = fileStram.Read(inputBufferByte, 0, Constants.INPUT_BLOCK_SIZE);
                    iv = Util.MatrixTranspose(iv);
                }
            }
        }


        public void ExecuteTextOperation()
        {
            // initial vector convert to 2d array from 1d array
            byte[][] iv = Util.MatrixTranspose(Util.Convert1Dto2DArray(Encoding.ASCII.GetBytes(parameter.InitialVector)));

            byte[] textByteArray = Encoding.ASCII.GetBytes(parameter.Text);
            byte[] textBlock = new byte[16];
            int length = textByteArray.Length;

            // extract block from the text byte array
            for (int i = 0; i < length; i += 16)
            {
                Array.Clear(textBlock, 0, 16);
                if (i + 16 > length)
                {
                    Array.Copy(textByteArray, i, textBlock, 0, length - i);
                    textBlock = AddPadding(textBlock, length - i);
                }
                else
                {
                    Array.Copy(textByteArray, i, textBlock, 0, 16);
                }
                // encrypted cypher 2d byte
                iv = EncryptBlock(textBlock,iv);
                Util.Print2DHex(iv);
                iv = Util.MatrixTranspose(iv);
                
            }
        }

        private byte[][] EncryptBlock(byte[] textBlock, byte[][] iv)
        {
            byte[][] input = Util.MatrixTranspose(Util.Convert1Dto2DArray(textBlock));
            byte[][] currentStage = AddRoundKey(input, iv);
            return EncryptionRoundIteration(currentStage);
        }

        private void ExpandEncryptionKey(byte[] key)
        {
            keyInstance = Key.GetKeyInstance;
            keyInstance.InitializeKey(key);
        }

        public void InitializeMode(Parameter param)
        {
            this.parameter = param;
            this.isInverse = false;
            ExpandEncryptionKey(Encoding.ASCII.GetBytes(parameter.Key));
        }
    }
}
