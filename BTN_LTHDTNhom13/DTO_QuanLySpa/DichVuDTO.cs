using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLySpa
{
    public abstract class DichVuDTO
    {
        protected string maDV;
        protected string tenDV;
        protected double giaDV;
        protected string loaiDV;


        public DichVuDTO(string maDV, string tenDV, double giaDV, string loaiDV)
        {
            MaDV = maDV;
            TenDV = tenDV;
            GiaDV = giaDV;
            LoaiDV = loaiDV;
        }
        public DichVuDTO() { }

        public string MaDV { get => maDV; set => maDV = value; }
        public string TenDV { get => tenDV; set => tenDV = value; }
        public double GiaDV { get => giaDV; set => giaDV = value; }
        public string LoaiDV { get => loaiDV; set => loaiDV = value; }


        public abstract double TinhTien();
    }
}