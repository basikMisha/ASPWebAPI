namespace ASPWebAPI.DTOs.AdoptionRequest
{
    public record UpdateAdoptionRequestDto
    {
        public int PetId { get; set; }
        public int AdopterId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime AdoptionDate { get; set; }
        public string Status { get; set; }
    }
}
