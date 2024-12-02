namespace Infrastructure.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetOrSetAsync<T>(string key, Func<CancellationToken, Task<T>> getItemCallback, int expirationInSeconds = 10, CancellationToken cancellationToken = default);
    }
}
