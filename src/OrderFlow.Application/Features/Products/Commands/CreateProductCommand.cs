namespace OrderFlow.Application.Features.Products.Commands;

public record CreateProductCommand(string Name, string Sku, decimal Price, string Currency);
