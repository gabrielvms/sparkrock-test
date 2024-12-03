namespace Common.Models
{
    public class BaseApiOptions
    {
        public string BaseUrl { get; set; } = null!;
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(1);
    }
}
