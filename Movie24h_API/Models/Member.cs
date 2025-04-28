using System.ComponentModel.DataAnnotations;

namespace Movie24h_API.Models {
    public class Member {
        [Key]
        [MaxLength(36)]
        public required string Id { get; set; }
        [MaxLength(100)]
        public string? FullName { get; set; }
        [MaxLength(200)]
        public string? Address { get; set; }
        public DateOnly? Birthday { get; set; }
        [MaxLength(10)]
        public string? Gender { get; set; }
        [MaxLength(200)]
        public string? Avatar { get; set; }
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }
        [MaxLength(200)]
        public string? Email { get; set; }
        [MaxLength(20)]
        public string? Fax { get; set; }
        [MaxLength(12)]
        public string? CCCD { get; set; }
        public int? ProvineId { get; set; }
        public int? DistrictId { get; set; }
        public int? WardId { get; set; }
        public DateTime? CreateDate { get; set; }
        [MaxLength(36)]
        public string? CreateById { get; set; }
        public DateTime? UpdateDate { get; set; }
        [MaxLength(36)]
        public string? UpdateById { get; set; }
    }
}
