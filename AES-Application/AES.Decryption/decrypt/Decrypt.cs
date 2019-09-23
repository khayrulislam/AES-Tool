using AES.Shared.Interface;
using AES.Shared.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Decryption.decrypt
{
    public class Decrypt
    {
        private IEncryptDecryptMode encMode;

        private Parameter encryptionParameter;

        public Decrypt(Parameter parameter)
        {
            this.encryptionParameter = parameter;
        }

        public void SetEncryptionMode(IEncryptDecryptMode encryptionMode)
        {
            this.encMode = encryptionMode;
        }

        public void Execute()
        {

            encMode.InitializeMode(encryptionParameter);
            encMode.ExecuteTextOperation();
            //encMode.EncryptFile();
        }
    }
}
