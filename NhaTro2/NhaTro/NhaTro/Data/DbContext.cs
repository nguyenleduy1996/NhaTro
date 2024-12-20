using Microsoft.EntityFrameworkCore;
using NhaTro.Models;

namespace NhaTro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PhongTro> PhongTros { get; set; }
    }
}
