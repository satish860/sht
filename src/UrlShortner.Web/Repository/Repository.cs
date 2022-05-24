using CSharpFunctionalExtensions;
using Google.Cloud.Firestore;

namespace UrlShortner.Web.Repository
{
    public class Repository : IRepository
    {
        private readonly CollectionReference Collection;

        public Repository(FirestoreDb firestoreDb)
        {
            this.Collection = firestoreDb.Collection("Urls");
        }

        public async Task<Result> CreateShortUrl(ShortUrl shortUrl)
        {
            DocumentReference reference = this.Collection.Document(shortUrl.ShortCode);
            var result = await reference.CreateAsync(shortUrl);
            var condition = Precondition.LastUpdated(result.UpdateTime);
            return condition.LastUpdateTime.HasValue ? Result.Success() : Result.Failure("Database Save failed");
        }

        public async Task<Result<ShortUrl>> GetShortUrl(string shortCode)
        {
            DocumentReference doc = this.Collection.Document(shortCode);
            var snapshot = await doc.GetSnapshotAsync();
            if(snapshot.Exists)
            {
                var ShortUrl = snapshot.ConvertTo<ShortUrl>();
                return Result.Success<ShortUrl>(ShortUrl);
            }
            return Result.Failure<ShortUrl>("Short url doesnt exist");
        }
    }
}
