using Microsoft.EntityFrameworkCore;
using NhaTro.Models;

namespace NhaTro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PhongTroTheoThang> PhongTroTheoThangs { get; set; }
        public DbSet<DanhSachPhongTro> DanhSachPhongTro { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DanhSachPhongTro>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.TenPhong).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Gia).HasDefaultValue(0);
            });

            modelBuilder.Entity<PhongTroTheoThang>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.TenPhong).HasMaxLength(50);
                entity.Property(e => e.TienPhong).HasColumnType("float");
                entity.Property(e => e.TienDien).HasColumnType("float");
                entity.Property(e => e.TienNuoc).HasColumnType("float");
                entity.Property(e => e.TienRac).HasColumnType("float");
                entity.Property(e => e.SoDienCu).HasColumnType("int");
                entity.Property(e => e.SoDienMoi).HasColumnType("int");
                entity.Property(e => e.SoNuocCu).HasColumnType("int");
                entity.Property(e => e.SoNuocMoi).HasColumnType("int");
                entity.Property(e => e.Thang).HasColumnType("int");
                entity.Property(e => e.Nam).HasColumnType("int");
            });
        }
    }
}
