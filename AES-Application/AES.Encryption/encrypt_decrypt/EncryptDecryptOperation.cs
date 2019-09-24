using AES.EncryptOrDecrypt.mode;
using AES.EncryptOrDecrypt.Mode;
using AES.Shared.Interface;
using AES.Shared.utility;
using System.Collections.Generic;

namespace AES.EncryptOrDecrypt.encrypt_decrypt
{
    public class EncryptDecryptOperation
    {
        private Parameter param;

        Dictionary<string, IEncryptDecryptMode> modeMap = new Dictionary<string, IEncryptDecryptMode>();

        public EncryptDecryptOperation(Parameter parameter)
        {
            this.param = parameter;
        }

        public void Execute()
        {
            ModeInitialization();
            IEncryptDecryptMode mode = modeMap[param.Type + param.Mode];
            mode.InitializeMode(param);
            mode.ExecuteFileOperation();
            //mode.ExecuteTextOperation();
        }

        private void ModeInitialization()
        {
            modeMap["eecb"] = new ECBModeEncrypt(); 
            modeMap["ecbc"] = new CBCModeEecrypt();
            modeMap["decb"] = new ECBModeDecrypt();
            modeMap["dcbc"] = new CBCModeDecrypt();
        }
    }
}
