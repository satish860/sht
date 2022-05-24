using UrlShortner.Web.Repository;

namespace UrlShortner.Web.CreateShortUrl
{
    public class CreateEndpoint : Endpoint<CreateUrlRequest, CreateResponse>
    {
        private const string DEFAULT = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly IRepository repository;

        public CreateEndpoint(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/api/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateUrlRequest req, CancellationToken ct)
        {
            var shturl = Nanoid.Nanoid.Generate(DEFAULT, 12);
            var shortcode = $"{BaseURL}{shturl}";
            CreateResponse createResponse = new CreateResponse
            {
                ShortUrl = shortcode
            };
            var result = await this.repository.CreateShortUrl(new ShortUrl
            {
                ShortCode = shturl,
                Url = req.Url,
            });
            if (result.IsFailure)
            {
                throw new Exception(result.Error);
            }
            else
                await SendOkAsync(createResponse, ct);
        }
    }
}
