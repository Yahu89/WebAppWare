namespace WebAppWareApi.Authentication;

public class AuthenticationSettings
{
    public string JwtKey { get; set; }
    public int JwtExpireMinutes { get; set; }
    public string JwtIssuer { get; set; }
}
