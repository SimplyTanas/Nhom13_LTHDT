using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLySpa
{
 
    public class ChamSocBodyDTO : DichVuDTO
    {
        public ChamSocBodyDTO()
        {
        }

        public ChamSocBodyDTO(string maDV, string tenDV, double giaDV, string loaiDV) : base(maDV, tenDV, giaDV, loaiDV)
        {
        }

        public override double TinhTien()
        {
            return GiaDV - GiaDV * 0.07;
        }
    }
}