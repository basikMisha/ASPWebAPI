namespace ASPWebAPI.Domain.Entities
{
    public class Volunteer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime StartDate { get; set; }
        public string? Email { get; set; }  
        public ICollection<Pet>? Pets { get; set; }
    }
}
