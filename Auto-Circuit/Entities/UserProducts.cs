namespace Auto_Circuit.Entities;

public class UserProducts
{
    public string UserId { get; set; }
    public string ProductId { get; set; }
    public virtual User User { get; set; }
    public virtual Product Product { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
