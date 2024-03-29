﻿using AES.EncryptDecrypt.steps;
using AES.EncryptDecrypt.KeyExpand;
using AES.EncryptDecrypt.Interface;
using AES.EncryptDecrypt.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AES.EncryptDecrypt.mode
{
    public class CBC_Encryption : EncryptDecryptRoundStep, IEncryptDecryptMode
    {
        private Key keyInstance;
        private Parameter parameter;
        public CBC_Encryption() : base()
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
            return currentStage;
        }

        public void ExecuteFileOperation()
        {
            long fileBlock = GetFileBlockSize(parameter.InputFilePath);
            byte[][] initialVector = Util.Transform1Dto2DArray(Encoding.ASCII.GetBytes(parameter.InitialVector));
            byte[] inputBlock;
            this.isNotOutputFileExist = true;

            for (int i = 0; i < fileBlock; i++)
            {
                inputBlock = FileRead(parameter.InputFilePath, i * Properties.Settings.Default.INPUT_BLOCK_SIZE);
                initialVector = EncryptBlock(inputBlock, initialVector);
                FileWrite(Util.Transform2dTo1DArray(initialVector), parameter.OutputFolderPath);
                Util.Print1DHex(Util.Transform2dTo1DArray(initialVector));
                //initialVector = Util.MatrixTranspose(initialVector);
            }
        }

/*        public void ExecuteTextOperation()
        {
            // initial vector convert to 2d array from 1d array
            byte[][] iv = Util.Transform1Dto2DArray(Encoding.ASCII.GetBytes(parameter.InitialVector));

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
                Util.Print1DHex(Util.Transform2dTo1DArray(iv));
            }
        }
*/
        private byte[][] EncryptBlock(byte[] textBlock, byte[][] iv)
        {
            byte[][] input = Util.Transform1Dto2DArray(textBlock);
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
