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
        void ExpandEncryptionKey(byte[] key);
        void EncryptFile(string inputFilePath,string outputFilePath);
        void EncryptText(string text);
    }
}
