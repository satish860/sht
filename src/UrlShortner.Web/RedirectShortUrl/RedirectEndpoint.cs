using UrlShortner.Web.Repository;

namespace UrlShortner.Web.RedirectShortUrl
{
    public class RedirectEndpoint : Endpoint<RedirectRequest>
    {
        private readonly IRepository repository;

        public RedirectEndpoint(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("{ShortCode}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RedirectRequest req, CancellationToken ct)
        {
            var result = await repository.GetShortUrl(req.ShortCode);
            if (result.IsFailure)
            {
                await SendNotFoundAsync(ct);
            }
            await SendRedirectAsync(result.Value.Url, isPermanant: true);
        }
    }
}
