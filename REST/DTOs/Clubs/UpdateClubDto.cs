namespace REST.DTOs.Clubs
{
    public class UpdateClubDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}