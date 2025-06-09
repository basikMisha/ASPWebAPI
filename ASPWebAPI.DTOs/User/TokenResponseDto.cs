namespace ASPWebAPI.DTOs.User
{
    public record TokenResponseDto
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
