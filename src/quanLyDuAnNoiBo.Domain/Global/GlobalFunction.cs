using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyDuAnNoiBo.Global
{
    public class GlobalFunction
    {

        public static double LamTronTien(double? soTien, int soLamTron = 2)
        {
            return soTien != null ? Math.Round((double)soTien, soLamTron, MidpointRounding.AwayFromZero) : 0;
        }
    }
}
