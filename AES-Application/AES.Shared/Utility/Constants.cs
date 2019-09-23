﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Shared.utility
{
    public static class Constants
    {
        public static int BLOCK_ROW_SIZE = 4;
        public static int BLOCK_COLUMN_SIZE = 4;
        public static int BOX_SIZE = 16;
        public static int MIX_COLUMN_CONSTANT = 27;
        public static string S_BOX_FILE_PATH = "../../../AES.shared/S-Box/s-box.txt";
        public static string INVERSE_S_BOX_FILE_PATH = "../../../AES.shared/S-Box/inverse-s-box.txt";
        public static string MIX_COLUMN_MATRIX_FILE_PATH = "../../../AES.shared/MixColumn/Matrixconstant.txt";
        public static string KEY_CONSTANT_FILE_PATH = "../../../AES.shared/KeyExpand/keyConstant.txt";
        public static string INPUT_FILE_PATH = "../../../AES.shared/KeyExpand/Input3.txt";
        public static string OUTPUT_FILE_PATH = "../../../AES.shared/KeyExpand/Input2.dat";

    }
}
