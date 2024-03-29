﻿using AES.EncryptDecrypt.mode;
using AES.EncryptDecrypt.Interface;
using AES.EncryptDecrypt.utility;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AES.EncryptDecrypt.algorithm
{
    public class AESAlgorithm
    {
        private Parameter param;

        Dictionary<string, IEncryptDecryptMode> modeMap = new Dictionary<string, IEncryptDecryptMode>();

        public AESAlgorithm(Parameter parameter)
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
            var str = param.Type == "e" ? "Encryption " : "Decryption ";
            str += "complete !!!";
            System.Windows.Forms.MessageBox.Show(str);
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
