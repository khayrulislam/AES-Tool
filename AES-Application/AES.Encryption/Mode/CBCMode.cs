using AES.Encryption.encrypt;
using AES.Encryption.Interface;
using AES.Encryption.steps;
using AES.Shared.KeyExpand;
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

        public void EncryptFile(string inputFilePath, string outputFilePath)
        {
            throw new NotImplementedException();
        }

        public void EncryptText(string text)
        {
            
        }

        public void ExpandEncryptionKey(byte[] key)
        {
            keyInstance = Key.GetKeyInstance;
            keyInstance.InitializeKey(key);
        }
    }
}
