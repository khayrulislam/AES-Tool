using AES.EncryptDecrypt.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.EncryptDecrypt.Interface
{
    public interface IEncryptDecryptMode 
    {
        void InitializeMode(Parameter param);
        void ExecuteFileOperation();
        ///void ExecuteTextOperation();
    }
}
