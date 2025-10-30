using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLySpa; 
using DAL_QuanLySpa; 

namespace BLL_QuanLySpa
{

    public class SpaBLL
    {

        private List<DichVuDTO> DsDV;
        private List<KhachHangDTO> DsKH;
        private List<DonSuDungDVDTO> DsDon;

        private SpaDAL dal;
        private string duongDan;


        public SpaBLL(string tenfile)
        {
            duongDan = tenfile;
            dal = new SpaDAL();
            (DsDV, DsKH, DsDon) = dal.DocFileXml(duongDan);
        }

        public List<DichVuDTO> LayDsDichVu()
        {
            return DsDV;
        }

        public List<KhachHangDTO> LayDsKhachHang()
        {
            return DsKH;
        }


        public List<DonSuDungDVDTO> LayDsDonSuDung()
        {
            return DsDon;
        }

        public void ThemDichVu(DichVuDTO dv)
        {
            DsDV.Add(dv);

            dal.LuuFileXml(duongDan, DsDV, DsKH, DsDon);
        }


        public void CapNhatGiaCSSD()
        {
            List<ChamSocSacDepDTO> cssd = DsDV.OfType<ChamSocSacDepDTO>().ToList();
            cssd.ForEach(x => x.GiaDV *= 1.03);

            dal.LuuFileXml(duongDan, DsDV, DsKH, DsDon);
        }


        public List<DichVuDTO> LocDichVuTren500k()
        {
            return DsDV.Where(k => k.GiaDV > 500000).ToList();
        }


        public (KhachHangDTO, List<DonSuDungDVDTO>, List<DichVuDTO>) TimDichVuCuaKhachHang(string tenKH)
        {

            KhachHangDTO kh = DsKH.Where(t => t.TenKH == tenKH).FirstOrDefault();
            if (kh == null)
                return (null, null, null);


            List<DonSuDungDVDTO> donsCuaKH = DsDon.Where(t => t.MaKH == kh.MaKH).ToList();


            List<DichVuDTO> dvsCuaKH = new List<DichVuDTO>();
            foreach (var don in donsCuaKH)
            {
                DichVuDTO dv = DsDV.Where(t => t.MaDV == don.MaDV).FirstOrDefault();
                if (dv != null)
                    dvsCuaKH.Add(dv);
            }

            return (kh, donsCuaKH, dvsCuaKH);
        }


        public DichVuDTO TimDichVuTheoTen(string tenDV)
        {

            return DsDV.FirstOrDefault(d => d.TenDV == tenDV);
        }


        public List<DichVuDTO> LocDichVuTheoLoai(string loai)
        {
            return DsDV.Where(d => d.LoaiDV == loai).ToList();
        }

        public List<(KhachHangDTO, int)> LocKhachHangTren3DV()
        {
            List<KhachHangDTO> khTren3 = DsKH.Where(kh => DsDon.Count(d => d.MaKH == kh.MaKH) > 3).ToList();

            List<(KhachHangDTO, int)> ketQua = new List<(KhachHangDTO, int)>();

            if (khTren3.Count > 0)
            {
                foreach (KhachHangDTO kh in khTren3)
                {
                    int soLan = DsDon.Count(d => d.MaKH == kh.MaKH);
                    ketQua.Add((kh, soLan));
                }
            }

            return ketQua;
        }
    }
}