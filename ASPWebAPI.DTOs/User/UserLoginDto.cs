﻿namespace ASPWebAPI.DTOs.User
{
    public record UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
