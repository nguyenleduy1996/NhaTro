using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NhaTro.Models
{
    [Table("PhongTro")]
    public class PhongTro
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; }

        [StringLength(50)]
        public string TenPhong { get; set; }

        public double? TienPhong { get; set; }
        public double? TienDien { get; set; }
        public double? TienNuoc { get; set; }
        public double? TienRac { get; set; }

        public int? SoDienCu { get; set; }
        public int? SoDienMoi { get; set; }
        public int? SoNuocCu { get; set; }
        public int? SoNuocMoi { get; set; }

        public int? Thang { get; set; }
        public int? Nam { get; set; }
    }
}
