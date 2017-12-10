namespace AMS.Models
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public T Data { get; set; }
    }
}