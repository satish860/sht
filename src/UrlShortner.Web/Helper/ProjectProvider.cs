using Google.Apis.Auth.OAuth2;

namespace UrlShortner.Web.Helper
{
    public class ProjectProvider : IProjectProvider
    {
        public string GetProjectId()
        {
            GoogleCredential googleCredential = GoogleCredential.GetApplicationDefault();
            if (googleCredential != null)
            {
                ICredential credential = googleCredential.UnderlyingCredential;
                ServiceAccountCredential? serviceAccountCredential =
                    credential as ServiceAccountCredential;
                if (serviceAccountCredential != null)
                {
                    return serviceAccountCredential.ProjectId;
                }
            }
            return Google.Api.Gax.Platform.Instance().ProjectId;
        }
    }
}
