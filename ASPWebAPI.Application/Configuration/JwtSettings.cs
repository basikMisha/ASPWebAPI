namespace ASPWebAPI.BLL.Configuration
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int ExpireMinutes { get; set; }
        public string Key { get; set; } = default!;
    }
}
