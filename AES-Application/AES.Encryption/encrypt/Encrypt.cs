﻿using AES.Encryption.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Encryption.encrypt
{
    public class Encrypt
    {

        private IEncryptionMode encMode;

        private Parameter encryptionParameter;

        public Encrypt(Parameter parameter)
        {
            this.encryptionParameter = parameter;
        }

        public void SetEncryptionMode(IEncryptionMode encryptionMode)
        {
            this.encMode = encryptionMode;
        }

        public void Execute()
        {

            encMode.InitializeEncryption(encryptionParameter);
            encMode.EncryptText();
            //encMode.EncryptFile();
        }
    }
}
