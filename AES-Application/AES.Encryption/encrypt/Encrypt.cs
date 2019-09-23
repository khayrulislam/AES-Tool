using AES.Encryption.Mode;
using AES.Shared.Interface;
using AES.Shared.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Encryption.encrypt
{
    public class Encrypt
    {

        private IEncryptDecryptMode encMode;

        private Parameter param;

        Dictionary<string, IEncryptDecryptMode> modeMap = new Dictionary<string, IEncryptDecryptMode>();

        public Encrypt(Parameter parameter)
        {
            this.param = parameter;
        }

        public void Execute()
        {
            ModeInitialization();
            IEncryptDecryptMode mode = modeMap[param.Type + param.Mode];
            mode.InitializeMode(param);
            //mode.ExecuteFileOperation();
            mode.ExecuteTextOperation();
        }

        private void ModeInitialization()
        {
            modeMap["eecb"] = new ECBMode(); 
            modeMap["ecbc"] = new CBCMode(); 
        }
    }
}
