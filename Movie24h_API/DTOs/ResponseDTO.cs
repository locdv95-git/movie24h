namespace Movie24h_API.DTOs {
    public class ResponseDTO<T> {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
