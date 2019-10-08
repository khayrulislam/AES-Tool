using AES.EncryptOrDecrypt.mode;
using AES.EncryptOrDecrypt.Mode;
using AES.Shared.Interface;
using AES.Shared.utility;
using System;
using System.Collections.Generic;
using System.IO;

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
            ChangeFileName();
            ModeInitialization();
            IEncryptDecryptMode operationMode = modeMap[param.Type + param.Mode];
            operationMode.InitializeMode(param);
            operationMode.ExecuteFileOperation();
            //mode.ExecuteTextOperation();
        }

        private void ChangeFileName()
        {
            if (param.Type == "e") param.OutputFolderPath += "/"+ Path.GetFileName(param.InputFilePath)+".aes";
            if (param.Type == "d") param.OutputFolderPath += "/" + Path.GetFileName(param.InputFilePath.Replace(".aes", "")) ;
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
