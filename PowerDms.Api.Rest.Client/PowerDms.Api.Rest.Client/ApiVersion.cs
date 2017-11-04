namespace PowerDms.Api.Rest.Client
{
    public sealed class ApiVersion
    {
        private readonly string _Route;

        private readonly int _Value;

        private ApiVersion(int value, string route)
        {
            _Value = value;
            _Route = route;
        }

        public static readonly ApiVersion Version1 = new ApiVersion(1, "v1");

        public override string ToString()
        {
            return _Route;
        }
    }
}