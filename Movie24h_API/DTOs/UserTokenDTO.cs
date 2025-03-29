namespace Movie24h_API.DTOs {
    public class UserTokenDTO {
        public string? UserName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? Expiration { get; set; }
        public string? DeviceInfo { get; set; }
        public string? IP { get; set; }
    }
}
