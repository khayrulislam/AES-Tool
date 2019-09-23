using AES.Shared.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Shared.Interface
{
    public interface IEncryptDecryptMode 
    {
        void InitializeMode(Parameter param);
        void ExecuteFileOperation();
        void ExecuteTextOperation();
    }
}
