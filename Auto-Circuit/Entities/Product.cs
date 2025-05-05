namespace Auto_Circuit.Entities;

public class Product
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public int Price { get; set; }
    public int Stu { get; set; }
    public ICollection<UserProducts> UserProducts { get; set; }
    public DateTime CreatedAt { get; set; }
}
