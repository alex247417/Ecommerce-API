namespace EcommerceAPI.DTOs;

public class CreateOrderDTO
{
    public List<OrderItemDTO> Items { get; set; } = new();
}

public class OrderItemDTO
{
    public int ProductId { get; set; }
    public int Quantidade { get; set; }
}