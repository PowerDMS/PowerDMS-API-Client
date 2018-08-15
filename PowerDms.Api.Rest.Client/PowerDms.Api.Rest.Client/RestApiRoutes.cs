namespace PowerDms.Api.Rest.Client
{
    public class RestApiRoutes
    {
        public const string PowerDmsRestApiDomain = "api.powerdms.com";

        public const string Groups = "principal/groups";

        public const string Users = "principal/users";

        // with the slash, it ignores the version
        public const string OAuthAccessToken = "/auth/connect/token";
    }
}