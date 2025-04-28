using System.ComponentModel.DataAnnotations;

namespace Movie24h_API.Models {
    public class User {
        [Key]
        [MaxLength(100)]
        public required string UserName { get; set; }
        public string? Password { get; set; }
        [MaxLength(36)]
        public string? MemberId { get; set; }
        public int? TypeId { get; set; }
        public int? LevelId { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreateDate { get; set; }
        [MaxLength(36)]
        public string? CreateById { get; set; }
        public DateTime? UpdateDate { get; set; }
        [MaxLength(36)]
        public string? UpdateById { get; set; }
    }
}
