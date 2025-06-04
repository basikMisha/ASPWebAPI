namespace ASPWebAPI.DTOs.Pet
{
    public record CreatePetDto
    {
        public string Name { get; set; } = default!;
        public string Species { get; set; } = default!;
        public int Age { get; set; } = default!;
        public bool IsAdopted { get; set; } = false;
        public string PhotoUrl { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int VolunteerId { get; set; } = default!;
    }
}
