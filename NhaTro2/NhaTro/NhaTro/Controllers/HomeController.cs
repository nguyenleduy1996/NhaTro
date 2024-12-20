using Microsoft.AspNetCore.Mvc;
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
            var a = GetPhongTroPreviousMonth().ToList();
            return View(a);
        }
        public IQueryable<PhongTro> GetPhongTroPreviousMonth()
        {
            var now = DateTime.Now;
            int prevMonth = now.Month - 1;
            int year = now.Year;

            if (prevMonth == 0)
            {
                prevMonth = 12; // Quay về tháng 12
                year -= 1;      // Giảm năm
            }

            return _context.PhongTros
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
