namespace Api1
{
    public static class Configurations
    {
        public static string JwtKey = "MIIEpQIBAAKCAQEA1zsXvF42tQEm9sWbvtUNfFlp1vRJ";
        public static string ApiKeyName = "api_key";
        public static string ApiKey = "_api_IlTevUM/z0ey3NwCV/unWg==";
        public static SmtpConfiguration Smtp = new ();
        public class SmtpConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; } = 25;
            public string UserName { get; set; }
            public string Password { get; set; }
        }

    }

}
