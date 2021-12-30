namespace Appeals.Domain
{
    public class Appeal
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}