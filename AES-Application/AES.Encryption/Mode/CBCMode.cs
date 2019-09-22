﻿using AES.Encryption.encrypt;
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
            // initial vector convert to 2d array from 1d array
            byte[][] iv = Util.MatrixTranspose(Util.Convert1Dto2DArray(Encoding.ASCII.GetBytes(initialVector)));

            byte[] textByteArray = Encoding.ASCII.GetBytes(text);
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
                // encrypted cypher 1d byte
                textBlock = Util.Convert2dTo1DArray(iv);
                Util.Print1DHex(textBlock);
            }
        }

        private byte[][] EncryptBlock(byte[] textBlock, byte[][] iv)
        {
            byte[][] input = Util.MatrixTranspose(Util.Convert1Dto2DArray(textBlock));
            byte[][] currentStage = AddRoundKey(input, iv);
            return EncryptionRoundIteration(currentStage);
        }

        public void ExpandEncryptionKey(byte[] key)
        {
            keyInstance = Key.GetKeyInstance;
            keyInstance.InitializeKey(key);
        }
    }
}
