﻿using AES.Shared.Interface;
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
    public class ECBModeDecrypt: EncryptDecryptRoundStep,IEncryptDecryptMode
    {
        private Parameter parameter;
        private Key keyInstance;

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
                    byte[] cypher = DecryptBlock(inputBufferByte);
                    Array.Clear(inputBufferByte, 0, 16);
                    FileWrite(cypher, parameter.OutputFilePath);
                    Util.Print1DHex(cypher);
                    bytesRead = fileStram.Read(inputBufferByte, 0, Constants.INPUT_BLOCK_SIZE);
                }
            }
        }

        private byte[] DecryptBlock(byte[] block)
        {
            byte[][] input = Util.MatrixTranspose(Util.Convert1Dto2DArray(block));
            byte[][] result = DecryptionRoundIteration(input);
            return Util.Convert2dTo1DArray(result);
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
            return Util.MatrixTranspose(currentStage);
        }

        public void ExecuteTextOperation()
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
        }

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
