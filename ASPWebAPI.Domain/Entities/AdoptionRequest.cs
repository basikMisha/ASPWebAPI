namespace ASPWebAPI.Domain.Entities
{
    public class AdoptionRequest
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public Pet? Pet { get; set; }
        public int AdopterId { get; set; }
        public Adopter? Adopter { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
    }
}
