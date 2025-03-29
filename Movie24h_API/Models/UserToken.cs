using System.ComponentModel.DataAnnotations;

namespace Movie24h_API.Models {
    public class UserToken {
        [Key]
        [MaxLength(36)]
        public required string Id { get; set; }
        [MaxLength(100)]
        public string? UserName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? Expiration { get; set; }
        public DateTime? RevokedDate { get; set; }
        [MaxLength(500)]
        public string? DeviceInfo { get; set; }
        [MaxLength(500)]
        public string? IP { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
