namespace ASPWebAPI.DTOs.User
{
    public record RefreshTokenRequestDto
    {
        public string RefreshToken { get; set; } = default!;
    }
}
