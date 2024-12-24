using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NhaTro.Models
{
    [Table("DanhSachPhongTro")]
    public class DanhSachPhongTro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Vì bạn dùng GUID
        [MaxLength(36)]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TenPhong { get; set; }

        [Required]
        public double Gia { get; set; } = 0; // Default value như trong DB
    }
}
