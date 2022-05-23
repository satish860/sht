namespace UrlShortner.Web.CreateShortUrl
{
    public class CreateEndpoint : Endpoint<CreateUrlRequest,CreateResponse>
    {
        private const string DEFAULT = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("/api/create");
            AllowAnonymous();
        }

        public override Task HandleAsync(CreateUrlRequest req, CancellationToken ct)
        {
            var shturl = Nanoid.Nanoid.Generate(DEFAULT,12);
            CreateResponse createResponse = new CreateResponse
            {
                ShortUrl = $"{BaseURL}{shturl}"
            };
            return SendOkAsync(createResponse, ct);
        }
    }
}
