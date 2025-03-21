namespace Movie24h_API.DTOs {
    public class UserDTO {
        public  string UserName { get; set; }
        public string Password { get; set; }
        public int? TypeId { get; set; }
        public int? LevelId { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateById { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateById { get; set; }
    }
}
