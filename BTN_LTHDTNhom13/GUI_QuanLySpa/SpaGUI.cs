using System;
using System.Collections.Generic;
using System.Text;
using BLL_QuanLySpa;
using DTO_QuanLySpa;
namespace GUI_QuanLySpa
{
	public class SpaGUI
	{
		static SpaBLL bll = new SpaBLL("../../../Data/QuanLySpa.xml");
		public void HienThiMenu()
		{
			int chon;
			do
			{
				Console.Clear();
				Console.WriteLine("================ MENU QUẢN LÝ SPA ================");
				Console.WriteLine("1.  (Đã chạy) Đọc dữ liệu từ file XML");
				Console.WriteLine("2.  Thêm mới một dịch vụ");
				Console.WriteLine("3.  Xuất danh sách tất cả dịch vụ");
				Console.WriteLine("4.  Tìm dịch vụ theo tên");
				Console.WriteLine("5.  Xuất danh sách dịch vụ của một khách hàng");
				Console.WriteLine("6.  Cập nhật kinh phí dịch vụ 'Chăm sóc sắc đẹp' tăng 3%");
				Console.WriteLine("7.  Xuất danh sách các dịch vụ có giá trên 500,000");
				Console.WriteLine("8.  Xuất danh sách dịch vụ thuộc loại 'Chăm sóc sắc đẹp'");
				Console.WriteLine("9.  In danh sách khách hàng đã sử dụng trên 3 dịch vụ");
				Console.WriteLine("10. In danh sách dịch vụ thuộc loại 'Dưỡng sinh trị liệu'");
				Console.WriteLine("0.  Thoát chương trình");
				Console.WriteLine("=================================================");
				Console.Write("Vui lòng chọn chức năng: ");

				try
				{
					chon = int.Parse(Console.ReadLine());
				}
				catch (Exception)
				{
					chon = -1;
				}

				Console.Clear();
				switch (chon)
				{
					case 1:
						Console.WriteLine("Chức năng 1: Đã đọc dữ liệu từ file XML khi khởi động.");
						XuatDanhSachDichVu();
						XuatDanhSachKhachHang();
						break;
					case 2:
						ThemMoiDichVu();
						break;
					case 3:
						Console.WriteLine("Chức năng 3: Xuất danh sách tất cả dịch vụ");
						XuatDanhSachDichVu();
						break;
					case 4:
						TimDichVuTheoTen();
						break;
					case 5:
						XuatDichVuCuaKhachHang();
						break;
					case 6:
						Console.WriteLine("Chức năng 6: Cập nhật giá DV 'Chăm sóc sắc đẹp' + 3%");
						bll.CapNhatGiaCSSD();
						Console.WriteLine("Đã cập nhật giá thành công! Dưới đây là danh sách sau khi cập nhật:");
						XuatDanhSachDichVu();
						break;
					case 7:
						Console.WriteLine("Chức năng 7: Xuất danh sách dịch vụ giá > 500,000");
						List<DichVuDTO> dv500k = bll.LocDichVuTren500k();
						HienThiDanhSachDV(dv500k);
						break;
					case 8:

						Console.WriteLine("Chức năng 8: Xuất danh sách dịch vụ 'Chăm sóc sắc đẹp'");
						List<DichVuDTO> dvCSSD = bll.LocDichVuTheoLoai("Chăm sóc sắc đẹp");
						HienThiDanhSachDV(dvCSSD);
						break;
					case 9:
						Console.WriteLine("Chức năng 9: Khách hàng đã sử dụng > 3 dịch vụ");
						var dsKHTren3 = bll.LocKhachHangTren3DV();
						if (dsKHTren3.Count == 0)
						{
							Console.WriteLine("Không có khách hàng nào sử dụng trên 3 dịch vụ.");
						}
						else
						{
							foreach (var kh in dsKHTren3)
							{
								XuatKhachHang(kh.Item1);
								Console.WriteLine($"\t-> Số lần sử dụng: {kh.Item2}");
							}
						}
						break;
					case 10:
						Console.WriteLine("Chức năng 10: Xuất danh sách dịch vụ 'Dưỡng sinh trị liệu'");
						List<DichVuDTO> dvDS = bll.LocDichVuTheoLoai("Dưỡng sinh trị liệu");
						HienThiDanhSachDV(dvDS);
						break;
					case 0:
						break;
					default:
						Console.WriteLine("Chức năng không hợp lệ, vui lòng chọn lại.");
						break;
				}

				if (chon != 0)
				{
					Console.WriteLine("\nBấm phím bất kỳ để quay về Menu...");
					Console.ReadKey();
				}

			} while (chon != 0);
		}


		static void ThemMoiDichVu()
		{
			Console.WriteLine("Chức năng 2: Thêm mới một dịch vụ");
			try
			{
				DichVuDTO dv = null;
				Console.Write("Nhập mã dịch vụ (VD: DV010): ");
				string maDv = Console.ReadLine();
				Console.Write("Nhập tên dịch vụ: ");
				string tenDV = Console.ReadLine();
				Console.Write("Nhập giá dịch vụ: ");
				double giaDV = double.Parse(Console.ReadLine());
				Console.WriteLine("Nhập loại dịch vụ:");
				Console.WriteLine("   1. Chăm sóc sắc đẹp");
				Console.WriteLine("   2. Chăm sóc body");
				Console.WriteLine("   3. Dưỡng sinh trị liệu");
				Console.Write("Chọn loại: ");
				int loai = int.Parse(Console.ReadLine());
				string loaiDV = "";

				switch (loai)
				{
					case 1:
						loaiDV = "Chăm sóc sắc đẹp";
						dv = new ChamSocSacDepDTO(maDv, tenDV, giaDV, loaiDV);
						break;
					case 2:
						loaiDV = "Chăm sóc body";
						dv = new ChamSocBodyDTO(maDv, tenDV, giaDV, loaiDV);
						break;
					case 3:
						loaiDV = "Dưỡng sinh trị liệu";
						dv = new DuongSinhTriLieuDTO(maDv, tenDV, giaDV, loaiDV);
						break;
					default:
						Console.WriteLine("Loại không hợp lệ.");
						return;
				}


				bll.ThemDichVu(dv);
				Console.WriteLine("Đã thêm dịch vụ thành công và lưu vào file XML!");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Lỗi nhập liệu: " + ex.Message);
			}
		}

		static void TimDichVuTheoTen()
		{
			Console.WriteLine("Chức năng 4: Tìm dịch vụ theo tên");
			Console.Write("Nhập tên dịch vụ cần tìm: ");
			string tenDV = Console.ReadLine();

			DichVuDTO dv = bll.TimDichVuTheoTen(tenDV);
			if (dv != null)
			{
				Console.WriteLine("Tìm thấy dịch vụ:");
				XuatDichVu(dv);
			}
			else
			{
				Console.WriteLine("Không tìm thấy dịch vụ nào có tên: " + tenDV);
			}
		}

		static void XuatDichVuCuaKhachHang()
		{
			Console.WriteLine("Chức năng 5: Xuất danh sách dịch vụ của một khách hàng");
			Console.Write("Nhập tên khách hàng cần tìm: ");
			string tenKH = Console.ReadLine();

			(var kh, var dons, var dvs) = bll.TimDichVuCuaKhachHang(tenKH);

			if (kh == null)
			{
				Console.WriteLine($"Không tìm thấy khách hàng: {tenKH}");
			}
			else
			{
				Console.WriteLine("Thông tin khách hàng:");
				XuatKhachHang(kh);
				Console.WriteLine("\nCác dịch vụ đã sử dụng:");
				for (int i = 0; i < dons.Count; i++)
				{
					XuatDichVu(dvs[i]);
					Console.WriteLine($"\t\t -> Ngày thực hiện: {dons[i].NgayThucHien.ToString("dd/MM/yyyy")}");
				}
			}
		}


		static void XuatDanhSachDichVu()
		{
			Console.WriteLine("\n--- Danh Sách Dịch Vụ ---");
			List<DichVuDTO> ds = bll.LayDsDichVu();
			HienThiDanhSachDV(ds);
		}

		static void XuatDanhSachKhachHang()
		{
			Console.WriteLine("\n--- Danh Sách Khách Hàng ---");
			List<KhachHangDTO> ds = bll.LayDsKhachHang();
			if (ds.Count == 0)
			{
				Console.WriteLine("Không có khách hàng.");
			}
			else
			{
				foreach (var kh in ds)
				{
					XuatKhachHang(kh);
					Console.WriteLine();
				}
			}
		}

		static void HienThiDanhSachDV(List<DichVuDTO> ds)
		{
			if (ds.Count == 0)
			{
				Console.WriteLine("Không có dịch vụ nào trong danh sách.");
			}
			else
			{
				foreach (DichVuDTO dv in ds)
				{
					XuatDichVu(dv);
				}
			}
		}


		static void XuatDichVu(DichVuDTO dv)
		{
			Console.Write($"Mã: {dv.MaDV}\t| Tên: {dv.TenDV,-40}\t| Giá: {dv.GiaDV:N0} VNĐ\t| Loại: {dv.LoaiDV}");

			double giaDaGiam = dv.TinhTien();
			if (giaDaGiam != dv.GiaDV)
			{
				Console.Write($"\t| Giá sau giảm: {giaDaGiam:N0} VNĐ");
			}
			Console.WriteLine();
		}

		static void XuatKhachHang(KhachHangDTO kh)
		{
			Console.Write($"Mã: {kh.MaKH}\t| Tên: {kh.TenKH,-25}\t| SĐT: {kh.SoDT}");
		}

	}
}