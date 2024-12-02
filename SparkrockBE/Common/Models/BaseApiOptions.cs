namespace Common.Models
{
    public class BaseApiOptions
    {
        public string BaseUrl { get; set; } = null!;
        public int RetryCount { get; set; } = 2;
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(1);
    }
}
