using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Shared.mixColumn
{
    public sealed class MixColumn
    {
        public static MixColumn mixColumnInstance = null;

        private int[][] matrix;
        private MixColumn()
        {
        }

        public void StoreMatrix(List<string[]> lines)
        {
            matrix = new int[4][];
            for (int i = 0; i< lines.Count; i++){
                for(int j = 0; j < lines[i].Length; j++)
                {
                    matrix[i][j] = Convert.ToInt32(lines[i][j]);
                }
            }
        }

        public static MixColumn GetMixColumnInstance
        {
            get
            {
                if (mixColumnInstance == null)
                {
                    mixColumnInstance = new MixColumn();
                }
                return mixColumnInstance;
            }
        }

/*        public byte[][] CalculateMixColumn(byte[][] input)
        {
            byte[][] result = new byte[4][];

            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    byte[] col = new byte[4];
                    for(int k = 0; k < 4; k++)
                    {
                        col[k][]
                    }
                    result[i][j] =  
                }
            }
        }*/

    }
}
