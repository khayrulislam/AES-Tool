using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Shared.Utility
{
    public static class Util
    {
        public static byte[] ShiftRow(byte[] row, int shiftCount)
        {
            byte[] rowShiftResult = new byte[row.Length];
            int length = row.Length;

            for (int i = 0; i < length; i++)
            {
                rowShiftResult[i] = row[(i + shiftCount) % length];
            }

            return rowShiftResult; ;
        }
    }
}
