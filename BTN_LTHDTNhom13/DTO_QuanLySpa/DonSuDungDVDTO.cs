using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLySpa
{

    public class DonSuDungDVDTO
    {
        private string maDV;
        private string maKH;
        private DateTime ngayThucHien;

        public DonSuDungDVDTO(string makh, string madv, DateTime ngayThucHien)
        {
            maKH = makh;
            MaDV = madv;
            NgayThucHien = ngayThucHien;
        }

        public string MaDV { get => maDV; set => maDV = value; }
        public string MaKH { get => maKH; set => maKH = value; }
        public DateTime NgayThucHien { get => ngayThucHien; set => ngayThucHien = value; }
    }
}