using OrderFlow.Domain.Entities;

namespace OrderFlow.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for the <see cref="Customer"/> entity.
/// Inherits CRUD operations from <see cref="IRepository{T}"/> and adds customer-specific query methods.
/// </summary>
public interface ICustomerRepository : IRepository<Customer>
{
    /// <summary>
    /// Retrieves a customer by their email address asynchronously.
    /// </summary>
    /// <param name="email">The customer's email address.</param>
    /// <returns>The customer if found, or null if it does not exist.</returns>
    Task<Customer?> GetByEmailAsync(string email);
}
