using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace _2FA_Calculator.ClientSide
{
    public class GoogleAuthenticator
    {
        // Return username
        public static string AuthenticateUser()
        {
            UserCredential userCredential;

            // Load or request credentials through OAuth 2.0
            using (var stream = new System.IO.FileStream("client_secrets.json", System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                string credentialsStoragePath = "savedUserTokens.json";

                userCredential = GoogleWebAuthorizationBroker.AuthorizeAsync
                (
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    new[] { PeopleServiceService.Scope.UserinfoProfile },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credentialsStoragePath, true)
                ).Result;
            }

            // Create People API service
            var peopleService = new PeopleServiceService(new BaseClientService.Initializer
            {
                HttpClientInitializer = userCredential,
                ApplicationName = "2FA Calculator"
            });

            // Request and get username of user
            var peopleRequest = peopleService.People.Get("people/me");
            peopleRequest.PersonFields = "names";
            Person userProfile = peopleRequest.Execute();
            string username = userProfile.Names?[0]?.DisplayName;

            return username;
        }
    }
}
