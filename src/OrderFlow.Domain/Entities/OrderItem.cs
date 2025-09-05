namespace OrderFlow.Domain.Entities;

/// <summary>
/// Represents a line item within an order.
/// </summary>
public class OrderItem
{
    /// <summary>
    /// Unique identifier of the order item.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Identifier of the product associated with this item.
    /// </summary>
    public Guid ProductId { get; private set; }

    /// <summary>
    /// Quantity of the product in this item.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Unit price of the product at the time the order was created.
    /// This value is a snapshot and should not be changed if the product price changes.
    /// </summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>
    /// Constructor to initialize a new instance of the <see cref="OrderItem"/> entity.
    /// </summary>
    /// <param name="id">The unique identifier of the item.</param>
    /// <param name="productId">The product ID.</param>
    /// <param name="quantity">The quantity of the product.</param>
    /// <param name="unitPrice">The unit price at the time of purchase.</param>
    public OrderItem(Guid id, Guid productId, int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        }

        if (unitPrice <= 0)
        {
            throw new ArgumentException("Unit price must be greater than zero.", nameof(unitPrice));
        }

        Id = id;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    /// <summary>
    /// Calculates the total amount for this order item.
    /// </summary>
    /// <returns>The total amount (Quantity * UnitPrice).</returns>
    public decimal GetTotal() => Quantity * UnitPrice;
}
