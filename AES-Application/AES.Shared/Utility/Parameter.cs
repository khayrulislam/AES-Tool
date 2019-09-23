using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Shared.utility
{
    public class Parameter
    {
        public string InputFilePath { get; set; }
        public string Mode { get; set; } // ECB or CBC
        public string Key { get; set; } // size 16 byte
        public string InitialVector { get; set; }
        public string OutputFilePath { get; set; }
        public string Text { get; set; }
        public string Type { get; set; } // encryption or decryption


    }
}
