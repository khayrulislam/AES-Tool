﻿using AES.EncryptDecrypt.Interface;
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
    public class ECB_Decryption: EncryptDecryptRoundStep,IEncryptDecryptMode
    {
        private Parameter parameter;
        private Key keyInstance;

        public void ExecuteFileOperation()
        {
            long fileBlock = GetFileBlockSize(parameter.InputFilePath);
            byte[] inputBlock, plainText;
            this.isNotOutputFileExist = true;

            for (int i = 0; i < fileBlock; i++)
            {
                inputBlock = FileRead(parameter.InputFilePath, i * Properties.Settings.Default.INPUT_BLOCK_SIZE);
                plainText = DecryptBlock(inputBlock);
                if (i + 1 == fileBlock) plainText = RemovePadding(plainText);
                FileWrite(plainText, parameter.OutputFolderPath);
                Util.Print1DHex(plainText);
            }
        }

        private byte[] DecryptBlock(byte[] block)
        {
            byte[][] input = Util.Transform1Dto2DArray(block);
            byte[][] result = DecryptionRoundIteration(input);
            return Util.Transform2dTo1DArray(result);
        }

        private byte[][] DecryptionRoundIteration(byte[][] currentStage)
        {
            currentStage = AddRoundKey(currentStage, keyInstance.GetRoundKey(10));
            for (int i = 9; i >= 0; i--)
            {
                currentStage = this.ShiftRow(currentStage);
                currentStage = SubstituteByte(currentStage);
                currentStage = AddRoundKey(currentStage, keyInstance.GetRoundKey(i));
                if (i != 0) currentStage = MixColumnOperation(currentStage);
            }
            return currentStage;
        }

       /* public void ExecuteTextOperation()
        {
            byte[] textByteArray= Encoding.ASCII.GetBytes(parameter.Text);
            byte[] blockCypher = new byte[16];
            int length = textByteArray.Length;

            for (int i = 0; i < length; i += 16)
            {
                Array.Clear(blockCypher, 0, 16);
                if (i + 16 > length)
                {
                    Array.Copy(textByteArray, i, blockCypher, 0, length - i);
                    blockCypher = AddPadding(blockCypher, length - i);
                }
                else
                {
                    Array.Copy(textByteArray, i, blockCypher, 0, 16);
                }
                // encrypted cypher text byte
                blockCypher = DecryptBlock(blockCypher);
                Util.Print1DHex(blockCypher);
            }
        }*/

        public void InitializeMode(Parameter param)
        {
            this.parameter = param;
            this.isInverse = true;
            ExpandKey(Encoding.ASCII.GetBytes(parameter.Key));
        }
        private void ExpandKey(byte[] key)
        {
            keyInstance = Key.GetKeyInstance;
            keyInstance.InitializeKey(key);
        }
    }
}
