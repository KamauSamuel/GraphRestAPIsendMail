using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace MSGraph.SendEmail.Authentication
{
    public class AuthenticationProvider
    {
        public AuthenticationProvider(string tenantId, string clientId, string clientSecret)
        {
            yourtenantid = tenantId;
            yourclientid = clientId;
            yourclientsecret = clientSecret;
        }
        public string yourtenantid { get; set; }
        public string yourclientid { get; set; }
        public string yourclientsecret { get; set; }
        public GraphServiceClient getGraphClient()
        {
            ClientSecretCredential cred = new ClientSecretCredential(yourtenantid, yourclientid, yourclientsecret);
            return new GraphServiceClient(cred);
        }
    }
}
