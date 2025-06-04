namespace ASPWebAPI.Domain.Entities
{
    public class Adopter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email {  get; set; }
        public string? Phone { get; set; }
        public ICollection<AdoptionRequest>? AdoptionRequests { get; set; }
    }
}
