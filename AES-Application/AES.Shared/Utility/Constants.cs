namespace AES.Shared.utility
{
    public static class Constants
    {
        public static int BLOCK_ROW_SIZE = 4;
        public static int BLOCK_COLUMN_SIZE = 4;
        public static int BOX_SIZE = 16;
        public static int MIX_COLUMN_CONSTANT = 27;
        public static int INPUT_BUFFER_SIZE = 16;


        public static string S_BOX_FILE_PATH = "../../../AES.shared/s-Box/s-box.txt";
        public static string INVERSE_S_BOX_FILE_PATH = "../../../AES.shared/s-Box/inverse-s-box.txt";
        public static string MIX_COLUMN_FILE_PATH = "../../../AES.shared/mixColumn/mix-column-constant.txt";
        public static string INVERSE_MIX_COLUMN_FILE_PATH = "../../../AES.shared/mixColumn/inverse-mix-column-constant.txt";
        public static string KEY_CONSTANT_FILE_PATH = "../../../AES.shared/KeyExpand/key-constant.txt";
        public static string INPUT_FILE_PATH = "../../../AES.shared/fileReader/input.txt";
        public static string OUTPUT_FILE_PATH = "../../../AES.shared/fileReader/output.txt";
        public static string OUTPUT_FILE_PATH2 = "../../../AES.shared/fileReader/output2.txt";

    }
}
