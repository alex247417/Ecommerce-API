namespace EcommerceAPI.DTOs;

public class ProductDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public string ImagemUrl { get; set; } = string.Empty;
}