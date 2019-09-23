using AES.Shared.Interface;
using AES.Encryption.steps;
using AES.Shared.KeyExpand;
using AES.Shared.utility;
using AES.Shared.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Decryption.mode
{
    public class CBCModeDecrypt : EncryptDecryptRoundStep, IEncryptDecryptMode
    {
        private Parameter parameter;
        private Key keyInstance;
        public void ExecuteFileOperation()
        {
            int bufferSize = 16;
            FileStream fileStram = new FileStream(parameter.InputFilePath, FileMode.Open, FileAccess.Read);
            using (fileStram)
            {
                byte[] buffer = new byte[bufferSize];
                fileStram.Seek(0, SeekOrigin.Begin);
                int bytesRead = fileStram.Read(buffer, 0, bufferSize);
                while (bytesRead > 0)
                {
                    byte[] cypher = DecryptBlock(buffer);
                    Array.Clear(buffer, 0, 16);
                    FileWrite(cypher);

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

        private byte[] DecryptBlock(byte[] block)
        {
            byte[][] input = Util.MatrixTranspose(Util.Convert1Dto2DArray(block));
            byte[][] result = DecryptRoundIteration(input);
            return Util.Convert2dTo1DArray(result);
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
            return Util.MatrixTranspose(currentStage);
        }
        public void ExecuteTextOperation()
        {

        }

        public void InitializeMode(Parameter param)
        {
            this.parameter = param;
            ExpandDecryptKey(Encoding.ASCII.GetBytes(parameter.Key));
        }
        private void ExpandDecryptKey(byte[] key)
        {
            keyInstance = Key.GetKeyInstance;
            keyInstance.InitializeKey(key);
        }
    }
}
