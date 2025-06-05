namespace ASPWebAPI.Domain.Entities
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public bool IsAdopted {  get; set; }
        public string? PhotoUrl { get; set; }
        public string? Description { get; set; }
        public int? VolunteerId { get; set; }
        public Volunteer? Volunteer { get; set; }
        public ICollection<AdoptionRequest>? AdoptionRequests { get; set; }
    }
}
