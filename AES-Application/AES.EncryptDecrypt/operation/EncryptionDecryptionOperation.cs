using AES.EncryptDecrypt.mode;
using AES.EncryptDecrypt.Interface;
using AES.EncryptDecrypt.utility;
using System.Collections.Generic;
using System.IO;

namespace AES.EncryptDecrypt.operation
{
    public class EncryptionDecryptionOperation
    {
        private Parameter param;

        Dictionary<string, IEncryptDecryptMode> modeMap = new Dictionary<string, IEncryptDecryptMode>();

        public EncryptionDecryptionOperation(Parameter parameter)
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
            modeMap["eecb"] = new ECB_Encryption(); 
            modeMap["ecbc"] = new CBC_Encryption();
            modeMap["decb"] = new ECB_Decryption();
            modeMap["dcbc"] = new CBC_Decryption();
        }
    }
}
