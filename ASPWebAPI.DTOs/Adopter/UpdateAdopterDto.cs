﻿namespace ASPWebAPI.DTOs.Adopters
{
    public record UpdateAdopterDto
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
    }
}
