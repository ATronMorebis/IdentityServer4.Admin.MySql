using System.Net.Http;
using Trov.IdentityServer4.Admin.Constants;
using Trov.IdentityServer4.Admin.Middlewares;

namespace Trov.IdentityServer4.Admin.IntegrationTests.Common
{
    public static class HttpClientExtensions
    {
        public static void SetAdminClaimsViaHeaders(this HttpClient client)
        {
            client.DefaultRequestHeaders.Add($"{AuthenticatedTestRequestMiddleware.TestUserPrefixHeader}-{AuthenticatedTestRequestMiddleware.TestUserId}", "1");
            client.DefaultRequestHeaders.Add($"{AuthenticatedTestRequestMiddleware.TestUserPrefixHeader}-{AuthenticatedTestRequestMiddleware.TestUserName}", "test");
            client.DefaultRequestHeaders.Add($"{AuthenticatedTestRequestMiddleware.TestUserPrefixHeader}-{AuthenticatedTestRequestMiddleware.TestUserRoles}", AuthorizationConsts.AdministrationRole);
        }

    }
}
