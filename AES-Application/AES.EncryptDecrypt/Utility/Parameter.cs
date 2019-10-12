namespace AES.EncryptDecrypt.utility
{
    public class Parameter
    {
        public string InputFilePath { get; set; }
        public string Mode { get; set; } // ECB or CBC
        public string Key { get; set; } // size 16 byte
        public string InitialVector { get; set; }
        public string OutputFolderPath { get; set; }
        public string Type { get; set; } // encryption or decryption
    }
}
