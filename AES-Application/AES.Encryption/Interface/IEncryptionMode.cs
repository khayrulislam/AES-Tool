using AES.Encryption.encrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Encryption.Interface
{
    public interface IEncryptionMode 
    {
        void InitializeEncryption(Parameter param);
        void EncryptFile();
        void EncryptText();
    }
}
