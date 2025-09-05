namespace OrderFlow.Domain.Entities;

/// <summary>
/// Represents an order placed by a customer.
/// This is the Aggregate Root that manages the OrderItems.
/// </summary>
public class Order
{
    /// <summary>
    /// Unique identifier of the order.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Identifier of the customer who placed the order.
    /// </summary>
    public Guid CustomerId { get; private set; }

    /// <summary>
    /// Date and time when the order was created.
    /// </summary>
    public DateTime OrderDate { get; private set; }

    // The list of items is private to ensure control by the aggregate root.
    private readonly List<OrderItem> _orderItems = [];

    /// <summary>
    /// Read-only collection of order items.
    /// The list must be manipulated through methods of the <see cref="Order"/> class.
    /// </summary>
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    /// <summary>
    /// Constructor to initialize a new instance of the <see cref="Order"/> entity.
    /// </summary>
    /// <param name="id">The unique identifier of the order.</param>
    /// <param name="customerId">The customer ID.</param>
    public Order(Guid id, Guid customerId)
    {
        Id = id;
        CustomerId = customerId;
        OrderDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds a new item to the order or updates the quantity if the product already exists.
    /// </summary>
    /// <param name="product">The product to be added.</param>
    /// <param name="quantity">The quantity to be added.</param>
    public void AddItem(Product product, int quantity)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null.");
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        }

        // Checks if an item with the same product already exists in the order.
        var existingItem = _orderItems.FirstOrDefault(item => item.ProductId == product.Id);

        if (existingItem != null)
        {
            // If the item already exists, the business logic could be to update the quantity.
            // To keep it simple, this example prevents adding a duplicate.
            // A more complex implementation could include an UpdateItemQuantity method.
            throw new InvalidOperationException("The product already exists in the order. Use an appropriate method to update the quantity.");
        }
        else
        {
            // Creates a new order item with the current product price (snapshot).
            var newItem = new OrderItem(Guid.NewGuid(), product.Id, quantity, product.Price);
            _orderItems.Add(newItem);
        }
    }

    /// <summary>
    /// Calculates the total amount of the order by summing up all item totals.
    /// </summary>
    /// <returns>The total amount of the order.</returns>
    public decimal GetTotalAmount()
    {
        return _orderItems.Sum(item => item.GetTotal());
    }
}
