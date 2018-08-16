namespace PowerDms.Api.Rest.Client
{
    /// <summary>
    /// ref: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Authorization
    /// </summary>
    public class HttpAuthorization
    {
        public string Type { get; set; }

        public string Credentials { get; set; }
    }
}