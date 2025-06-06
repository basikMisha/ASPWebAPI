﻿namespace ASPWebAPI.DTOs.Adopters
{
    public record AdopterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone {  get; set; } = default!;
    }
}
