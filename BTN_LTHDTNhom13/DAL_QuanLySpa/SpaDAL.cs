using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DTO_QuanLySpa; 

namespace DAL_QuanLySpa
{

    public class SpaDAL
    {

        public (List<DichVuDTO>, List<KhachHangDTO>, List<DonSuDungDVDTO>) DocFileXml(string file)
        {
            List<DichVuDTO> DsDV = new List<DichVuDTO>();
            List<KhachHangDTO> DsKH = new List<KhachHangDTO>();
            List<DonSuDungDVDTO> DsDon = new List<DonSuDungDVDTO>();

            XmlDocument doc = new XmlDocument();
            doc.Load(file);


            XmlNodeList NodeListDV = doc.SelectNodes("/DuLieuSpa/DsDichVu/DichVu");
            foreach (XmlNode node in NodeListDV)
            {
                DichVuDTO dv = null;
                string maDv = node["MaDV"].InnerText;
                string tenDV = node["TenDV"].InnerText;
                double giaDV = double.Parse(node["GiaDV"].InnerText);
                string loaiDV = node["LoaiDV"].InnerText;

                if (loaiDV == "Chăm sóc sắc đẹp")
                    dv = new ChamSocSacDepDTO(maDv, tenDV, giaDV, loaiDV);
                else if (loaiDV == "Chăm sóc body")
                    dv = new ChamSocBodyDTO(maDv, tenDV, giaDV, loaiDV);
                else if (loaiDV == "Dưỡng sinh trị liệu")
                    dv = new DuongSinhTriLieuDTO(maDv, tenDV, giaDV, loaiDV);

                if (dv != null)
                    DsDV.Add(dv);
            }


            XmlNodeList NodeListKH = doc.SelectNodes("/DuLieuSpa/DSKH/KH");
            foreach (XmlNode node in NodeListKH)
            {
                string maKH = node["MaKH"].InnerText;
                string tenKH = node["TenKH"].InnerText;
                string sdt = node["SDT"].InnerText;


                if (DsKH.All(kh => kh.MaKH != maKH))
                {
                    DsKH.Add(new KhachHangDTO(maKH, tenKH, sdt));
                }


                XmlNodeList NodeListDVSuDung = node.SelectNodes("DsDVDaSuDung/DVSuDung");
                foreach (XmlNode node1 in NodeListDVSuDung)
                {
                    string maDV = node1["MaDV"].InnerText;
                    DateTime ngayThucHien = DateTime.Parse(node1["NgayThucHien"].InnerText);
                    DsDon.Add(new DonSuDungDVDTO(maKH, maDV, ngayThucHien));
                }
            }

            return (DsDV, DsKH, DsDon);
        }


        public void LuuFileXml(string file, List<DichVuDTO> dsDV, List<KhachHangDTO> dsKH, List<DonSuDungDVDTO> dsDon)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true; //cho thut dau dong
            settings.Encoding = Encoding.UTF8;

            using (XmlWriter writer = XmlWriter.Create(file, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("DuLieuSpa");
                writer.WriteStartElement("DsDichVu");

                foreach (var dv in dsDV)
                {
                    writer.WriteStartElement("DichVu");
                    writer.WriteElementString("MaDV", dv.MaDV);
                    writer.WriteElementString("TenDV", dv.TenDV);
                    writer.WriteElementString("GiaDV", dv.GiaDV.ToString());
                    writer.WriteElementString("LoaiDV", dv.LoaiDV);
                    writer.WriteEndElement(); // DichVu
                }
                writer.WriteEndElement(); // DsDichVu


                writer.WriteStartElement("DSKH");
                foreach (var kh in dsKH)
                {
                    writer.WriteStartElement("KH");
                    writer.WriteElementString("MaKH", kh.MaKH);
                    writer.WriteElementString("TenKH", kh.TenKH);
                    writer.WriteElementString("SDT", kh.SoDT);

                    writer.WriteStartElement("DsDVDaSuDung");
                    var donsCuaKH = dsDon.Where(d => d.MaKH == kh.MaKH);
                    foreach (var don in donsCuaKH)
                    {
                        writer.WriteStartElement("DVSuDung");
                        writer.WriteElementString("MaDV", don.MaDV);
                        writer.WriteElementString("NgayThucHien", don.NgayThucHien.ToString("yyyy/MM/dd"));
                        writer.WriteEndElement(); // DVSuDung
                    }
                    writer.WriteEndElement(); // DsDVDaSuDung
                    writer.WriteEndElement(); // KH
                }
                writer.WriteEndElement(); // DSKH

                writer.WriteEndElement(); // DuLieuSpa
                writer.WriteEndDocument();
            }
        }
    }
}