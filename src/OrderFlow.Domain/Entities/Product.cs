namespace OrderFlow.Domain.Entities;

/// <summary>
/// Represents a product in the e-commerce catalog.
/// </summary>
public class Product
{
    /// <summary>
    /// Unique identifier of the product.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// SKU (Stock Keeping Unit) of the product, a unique code for inventory control.
    /// </summary>
    public string Sku { get; private set; }

    /// <summary>
    /// Name of the product.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Unit price of the product.
    /// </summary>
    public decimal Price { get; private set; }

    /// <summary>
    /// Constructor to initialize a new instance of the <see cref="Product"/> entity.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <param name="sku">The product SKU.</param>
    /// <param name="name">The product name.</param>
    /// <param name="price">The product price.</param>
    public Product(Guid id, string sku, string name, decimal price)
    {
        // Validations to ensure object consistency (Domain Invariants)
        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new ArgumentException("SKU cannot be null or empty.", nameof(sku));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }

        if (price <= 0)
        {
            throw new ArgumentException("Price must be greater than zero.", nameof(price));
        }

        Id = id;
        Sku = sku;
        Name = name;
        Price = price;
    }
}
