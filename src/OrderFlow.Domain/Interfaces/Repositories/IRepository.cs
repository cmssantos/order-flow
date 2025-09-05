namespace OrderFlow.Domain.Interfaces.Repositories;

/// <summary>
/// Generic repository interface defining the basic CRUD operations.
/// Following this convention ensures consistency in the data access layer.
/// </summary>
/// <typeparam name="T">The domain entity (aggregate root) managed by the repository.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Retrieves an entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    /// <returns>The entity if found, or null if it does not exist.</returns>
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    /// Retrieves all entities asynchronously.
    /// </summary>
    /// <returns>A collection of all entities.</returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Adds a new entity to the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    Task AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity in the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity with updated data.</param>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Removes an entity from the repository by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the entity to remove.</param>
    Task DeleteAsync(Guid id);
}
