using AES.Shared.steps;
using AES.Shared.KeyExpand;
using AES.Shared.Interface;
using AES.Shared.utility;
using AES.Shared.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Encryption.Mode
{
    public class ECBMode : EncryptDecryptRoundStep,IEncryptDecryptMode
    {
        private Key keyInstance;
        private Parameter parameter;
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

        public void ExpandKey(byte[] key)
        {
            keyInstance = Key.GetKeyInstance;
            keyInstance.InitializeKey(key);
        }

        public void ExecuteFileOperation()
        {

            using (FileStream fileStram = new FileStream(@parameter.InputFilePath, FileMode.Open, FileAccess.Read))
            {
                byte[] inputBufferByte = new byte[Constants.INPUT_BUFFER_SIZE];
                fileStram.Seek(0, SeekOrigin.Begin);
                int bytesRead = fileStram.Read(inputBufferByte, 0, Constants.INPUT_BUFFER_SIZE);
                byte[][] iv = Util.MatrixTranspose(Util.Convert1Dto2DArray(Encoding.ASCII.GetBytes(parameter.InitialVector)));
                this.fileCreate = true;

                while (bytesRead > 0)
                {
                    byte[] cypher = EncryptBlock(inputBufferByte);
                    Array.Clear(inputBufferByte, 0, 16);
                    FileWrite(cypher, parameter.OutputFilePath);
                    Util.Print1DHex(cypher);
                    bytesRead = fileStram.Read(inputBufferByte, 0, Constants.INPUT_BUFFER_SIZE);
                }
            }

        }

        public void ExecuteTextOperation()
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
        }

        private byte[] EncryptBlock(byte[] block)
        {
            byte[][] input = Util.MatrixTranspose(Util.Convert1Dto2DArray(block));
            byte[][] result = EncryptionRoundIteration(input);
            return Util.Convert2dTo1DArray(result);
        }

        public void InitializeMode(Parameter param)
        {
            this.parameter = param;
            this.isInverse = false;
            ExpandKey(Encoding.ASCII.GetBytes(parameter.Key));
        }
    }
}
