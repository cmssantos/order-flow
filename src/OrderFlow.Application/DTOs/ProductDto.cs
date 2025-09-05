namespace OrderFlow.Application.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Sku  { get; set; } = string.Empty;
    public decimal Price  { get; set; }
    public string Currency  { get; set; } = string.Empty;
}
