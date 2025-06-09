namespace ASPWebAPI.DTOs.User
{
    public record UserRegisterDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Role { get; set; } = "User";
    }
}
