namespace AES.EncryptDecrypt.utility
{
    public static class Constants
    {
        public static int BLOCK_ROW_SIZE = 4;
        public static int BLOCK_COLUMN_SIZE = 4;
        public static int BOX_SIZE = 16;
        public static int MIX_COLUMN_CONSTANT = 27;
        public static int INPUT_BLOCK_SIZE = 16;


        public static string S_BOX_FILE_PATH = "../../../AES.EncryptDecrypt/s-Box/s-box.txt";
        public static string INVERSE_S_BOX_FILE_PATH = "../../../AES.EncryptDecrypt/s-Box/inverse-s-box.txt";
        public static string MIX_COLUMN_FILE_PATH = "../../../AES.EncryptDecrypt/mixColumn/mix-column-constant.txt";
        public static string INVERSE_MIX_COLUMN_FILE_PATH = "../../../AES.EncryptDecrypt/mixColumn/inverse-mix-column-constant.txt";
        public static string KEY_CONSTANT_FILE_PATH = "../../../AES.EncryptDecrypt/KeyExpand/key-constant.txt";
        public static string INPUT_FILE_PATH = "../../../AES.EncryptDecrypt/fileReader/input.txt";
        public static string INPUT_FILE_PATH2 = "../../../AES.EncryptDecrypt/fileReader/input.pdf";
        public static string OUTPUT_FILE_PATH = "../../../AES.EncryptDecrypt/fileReader";
        public static string OUTPUT_FILE_PATH2 = "../../../AES.EncryptDecrypt/fileReader";

    }
}
