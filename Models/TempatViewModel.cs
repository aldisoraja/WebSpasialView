using System.ComponentModel.DataAnnotations;

namespace WebSpasialView.Models
{
    public class TempatViewModel
    {
        public int id { get; set; }
        [Required]
        public string nama_pemilik { get; set; } = string.Empty;
        [Required]
        public string nama_tempat { get; set; } = string.Empty;
        [Required]
        public string alamat { get; set; } = string.Empty;
        [Required]
        public string jenis_tempat { get; set; } = string.Empty;
        [Required]
        public string latitude { get; set; } = string.Empty;
        public string longitude { get; set; } = string.Empty;
    }
}
