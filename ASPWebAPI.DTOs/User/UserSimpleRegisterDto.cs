namespace ASPWebAPI.DTOs.User
{
    public record UserSimpleRegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
