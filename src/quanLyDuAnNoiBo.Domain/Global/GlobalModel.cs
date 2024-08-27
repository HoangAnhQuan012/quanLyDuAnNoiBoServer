using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyDuAnNoiBo.Global
{
    public class GlobalModel
    {
        // Trạng thái Dự án
        public static SortedList<int, string> SortedTrangThaiDuAn = new SortedList<int, string>
        {
            { 1, "Khởi tạo" },
            { 2, "Đang thực hiện"},
            { 3, "Đang tạm dừng"},
            { 4, "Kết thúc"},
        };

        // Loại hình dự án
        public static SortedList<int, string> SortedLoaiHinhDuAn = new SortedList<int, string>
        {
            { 1, "Bình thường" },
            { 2, "OT"},
            { 3, "Outsource"},
        };
    }
}
