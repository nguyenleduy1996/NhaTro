﻿using Microsoft.AspNetCore.Mvc;
using NhaTro.Data;
using NhaTro.Models;
using System.Diagnostics;

namespace NhaTro.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
          //  var a = GetPhongTroPreviousMonth().ToList();
            return View();
        }
        public IActionResult RenderListRoom()
        {
            var model = _context.DanhSachPhongTro.ToList();
            return View(model);
        }
        public IActionResult ViewDetail(string TenPhong)
        {
            var model = _context.PhongTroTheoThangs.Where(x => x.TenPhong.ToUpper().Trim() == TenPhong.ToUpper().Trim()).ToList();
            return View(model);
        }
        public IActionResult TinhTien(string TenPhong, int soDienThangNay, int soNuocThangNay)
        {

            var Phong = _context.DanhSachPhongTro.Where(x=>x.TenPhong == TenPhong).FirstOrDefault();
            var now = DateTime.Now;
            int prevMonth = now.Month - 1;
            int year = now.Year;

            var latestPhongTro = _context.PhongTroTheoThangs
             .Where(p => p.DaNhan && p.TenPhong == TenPhong)  // Lọc các phòng trọ đã nhận
             .OrderByDescending(p => p.Nam)  // Sắp xếp giảm dần theo năm
             .ThenByDescending(p => p.Thang)  // Sắp xếp giảm dần theo tháng
             .FirstOrDefault();  // Lấy bản ghi đầu tiên (mới nhất)
            var phongTro = new PhongTroTheoThang
            {
                Id = Guid.NewGuid().ToString(), // Sinh ID ngẫu nhiên (UUID)
                TenPhong = Phong.TenPhong,
                TienPhong = Phong.Gia,
                TienDien = 3500,
                TienNuoc = 14000,
                TienRac = 10000,
                SoDienCu = latestPhongTro.SoDienMoi,
                SoDienMoi = soDienThangNay,
                SoNuocCu = latestPhongTro.SoNuocMoi,
                SoNuocMoi = soNuocThangNay,
                Thang = prevMonth,
                Nam = year,
                DaNhan = false,
            };

            // Thêm bản ghi vào DbSet
            _context.PhongTroTheoThangs.Add(phongTro);

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            return Json(new { success = true, message = "OK" });
        }
        public IActionResult deleteItem(string Id)
        {
            var item =  _context.PhongTroTheoThangs.Where(x=>x.Id == Id).FirstOrDefault();

            if (item == null)
            {
                // Nếu không tìm thấy đối tượng, trả về lỗi
                return Json(new { success = false, message = "Không tìm thấy phòng trọ" });
            }
            if (item.DaNhan == true)
            {
                return Json(new { success = false, message = "Da Nhan Tien Roi" });
            }

            // Xóa đối tượng
            _context.PhongTroTheoThangs.Remove(item);

            // Lưu thay đổi vào cơ sở dữ liệu
             _context.SaveChanges();

            // Trả về kết quả
            return Json(new { success = true, message = "Xóa thành công" });
        }
        public IQueryable<PhongTroTheoThang> GetPhongTroPreviousMonth()
        {
            var now = DateTime.Now;
            int prevMonth = now.Month - 1;
            int year = now.Year;

            if (prevMonth == 0)
            {
                prevMonth = 12; // Quay về tháng 12
                year -= 1;      // Giảm năm
            }

            return _context.PhongTroTheoThangs
                .Where(p => p.Thang == prevMonth && p.Nam == year);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
