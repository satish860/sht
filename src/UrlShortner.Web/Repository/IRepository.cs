using CSharpFunctionalExtensions;

namespace UrlShortner.Web.Repository
{
    public interface IRepository
    {
        Task<Result> CreateShortUrl(ShortUrl shortUrl);

        Task<Result<ShortUrl>> GetShortUrl(string shortCode);
    }
}
