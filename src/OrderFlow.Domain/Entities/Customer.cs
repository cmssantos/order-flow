namespace OrderFlow.Domain.Entities;

/// <summary>
/// Represents a customer in the system.
/// </summary>
public class Customer
{
    /// <summary>
    /// Unique identifier of the customer.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Full name of the customer.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Customer's email address, used for login and communication.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Constructor to initialize a new instance of the <see cref="Customer"/> entity.
    /// </summary>
    /// <param name="id">The unique identifier of the customer.</param>
    /// <param name="name">The customer's full name.</param>
    /// <param name="email">The customer's email address.</param>
    public Customer(Guid id, string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
        }

        Id = id;
        Name = name;
        Email = email;
    }
}
