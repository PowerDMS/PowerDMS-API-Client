namespace PowerDms.Api.Rest.Client
{
    public class RestApiRoutes
    {
        public const string PowerDmsRestApiDomain = "https://api.powerdms.com";

        // TODO: this will change to point to Auth0 host address
        public const string PowerDmsOAuthDomain = "https://api.powerdms.com";

        public const string Groups = "principal/groups";

        public const string Users = "principal/users";

        // with the slash, it ignores the version
        public const string OAuthAccessToken = "/auth/connect/token";
    }
}