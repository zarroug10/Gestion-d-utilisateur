namespace Auto_Circuit.Entities;

public class Rating
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public long UserId { get; set; }
    public int RatingValue { get; set; }
    public DateTime CreatedAt { get; set; }
}
