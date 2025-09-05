using OrderFlow.Domain.Entities;

namespace OrderFlow.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for the <see cref="Order"/> entity, which is an aggregate root.
/// Inherits CRUD operations from <see cref="IRepository{T}"/> and adds order-specific query methods.
/// The implementation of this interface must ensure that order items (<see cref="OrderItem"/>) are loaded together with the order.
/// </summary>
public interface IOrderRepository : IRepository<Order>
{
    /// <summary>
    /// Retrieves all orders for a specific customer asynchronously.
    /// </summary>
    /// <param name="customerId">The customer identifier.</param>
    /// <returns>A collection of orders for the specified customer.</returns>
    Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId);

    /// <summary>
    /// Retrieves orders created within a specific date range.
    /// </summary>
    /// <param name="startDate">The start date of the period.</param>
    /// <param name="endDate">The end date of the period.</param>
    /// <returns>A collection of orders that match the date criteria.</returns>
    Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
}
