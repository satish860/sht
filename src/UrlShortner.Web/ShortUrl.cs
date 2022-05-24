using Google.Cloud.Firestore;

namespace UrlShortner.Web
{
    [FirestoreData]
    public class ShortUrl
    {
        [FirestoreProperty]
        public string Url { get; set; } = string.Empty;

        [FirestoreProperty]
        public string ShortCode { get; set; } = string.Empty;
    }
}
