namespace Doing.Common.Auth
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }

        public int ExpirationinMinutes { get; set; }

        public string IssuerOfToken { get; set; }
    }
}