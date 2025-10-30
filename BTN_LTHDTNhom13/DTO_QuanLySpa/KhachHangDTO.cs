using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLySpa
{

    public class KhachHangDTO
    {
        private string maKH;
        private string tenKH;
        private string soDT;

        public KhachHangDTO(string maKH, string tenKH, string soDT)
        {
            MaKH = maKH;
            TenKH = tenKH;
            SoDT = soDT;
        }

        public KhachHangDTO() { }

        public string MaKH { get => maKH; set => maKH = value; }
        public string TenKH { get => tenKH; set => tenKH = value; }
        public string SoDT { get => soDT; set => soDT = value; }
    }
}