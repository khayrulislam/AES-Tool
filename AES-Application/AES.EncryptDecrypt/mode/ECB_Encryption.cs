using AES.EncryptDecrypt.steps;
using AES.EncryptDecrypt.KeyExpand;
using AES.EncryptDecrypt.Interface;
using AES.EncryptDecrypt.utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.EncryptDecrypt.mode
{
    public class ECB_Encryption : EncryptDecryptRoundStep,IEncryptDecryptMode
    {
        private Key keyInstance;
        private Parameter parameter;
        public ECB_Encryption(): base()
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
            return currentStage;
        }

        public void ExpandKey(byte[] key)
        {
            keyInstance = Key.GetKeyInstance;
            keyInstance.InitializeKey(key);
        }

        public void ExecuteFileOperation()
        {
            long fileBlock = GetFileBlockSize(@parameter.InputFilePath);
            byte[] inputBlock, cypher;
            this.isNotOutputFileExist = true;

            for (int i = 0; i < fileBlock; i++)
            {
                inputBlock = FileRead(@parameter.InputFilePath,i * Properties.Settings.Default.INPUT_BLOCK_SIZE);
                cypher = EncryptBlock(inputBlock);
                FileWrite(cypher, @parameter.OutputFolderPath);
                Util.Print1DHex(cypher);
            }
        }

       /* public void ExecuteTextOperation()
        {
            byte[] textByteArray= Encoding.ASCII.GetBytes(parameter.Text);
            //byte[] textByteArray= new byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff };
            byte[] blockCypher = new byte[16];
            int length = textByteArray.Length;

            for(int i = 0; i < length; i += 16)
            {
                Array.Clear(blockCypher, 0, 16);
                if (i+16 > length)
                {
                    Array.Copy(textByteArray,i,blockCypher,0,length-i);
                    blockCypher = AddPadding(blockCypher, length-i);
                }
                else
                {
                    Array.Copy(textByteArray,i,blockCypher,0,16);
                }
                // encrypted cypher text byte
                blockCypher = EncryptBlock(blockCypher);
                Util.Print1DHex(blockCypher);
            }
        }*/

        private byte[] EncryptBlock(byte[] block)
        {
            byte[][] input = Util.Transform1Dto2DArray(block);
            byte[][] result = EncryptionRoundIteration(input);
            return Util.Transform2dTo1DArray(result);
        }

        public void InitializeMode(Parameter param)
        {
            this.parameter = param;
            this.isInverse = false;
            ExpandKey(Encoding.ASCII.GetBytes(parameter.Key));
        }
    }
}
