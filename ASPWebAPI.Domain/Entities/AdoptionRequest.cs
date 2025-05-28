namespace ASPWebAPI.Models
{
    public class AdoptionRequest
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public int AdopterId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
    }
}
