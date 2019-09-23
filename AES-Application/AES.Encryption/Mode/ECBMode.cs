using AES.Encryption.steps;
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

        public void ExpandEncryptionKey(byte[] key)
        {
            keyInstance = Key.GetKeyInstance;
            keyInstance.InitializeKey(key);
        }

        public void ExecuteFileOperation()
        {
            int bufferSize=16;
            FileStream fileStram = new FileStream(parameter.InputFilePath, FileMode.Open, FileAccess.Read);
            using (fileStram)
            {
                byte[] buffer = new byte[bufferSize];
                fileStram.Seek(0, SeekOrigin.Begin);
                int bytesRead = fileStram.Read(buffer, 0, bufferSize);

                while (bytesRead > 0)
                {
                    byte[] cypher = EncryptBlock(buffer);
                    Array.Clear(buffer,0,16);
                    FileWrite(cypher);
                    Util.Print1DHex(cypher);
                    bytesRead = fileStram.Read(buffer, 0, bufferSize);
                }
            }
        }

        private void FileWrite(byte[] output)
        {
            FileStream fs;
            if (!File.Exists(@parameter.OutputFilePath))
            {
                fs = File.Create(@parameter.OutputFilePath);
                fs.Close();
            }
            fs = new FileStream(parameter.OutputFilePath, FileMode.Append);
            fs.Write(output, 0, output.Length);
            fs.Close();
        }

        public void ExecuteTextOperation()
        {
            byte[] textByteArray= Encoding.ASCII.GetBytes(parameter.Text);
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
            ExpandEncryptionKey(Encoding.ASCII.GetBytes(parameter.Key));
        }
    }
}
