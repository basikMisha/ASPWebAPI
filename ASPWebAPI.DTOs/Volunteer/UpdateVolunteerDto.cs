namespace ASPWebAPI.DTOs.Volunteer
{
    public record UpdateVolunteerDto
    {
        public string Name { get; set; } = default!;
        public string Role { get; set; }
        public DateTime StartDate { get; set; } = default!;
        public string Email { get; set; }
    }
}
