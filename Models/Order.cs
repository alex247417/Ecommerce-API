namespace EcommerceAPI.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public decimal Total { get; set; }
    public string Status { get; set; } = "pendente";
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; set; } = new();
}