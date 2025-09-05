using OrderFlow.Domain.Entities;

namespace OrderFlow.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for the <see cref="Product"/> entity.
/// Inherits CRUD operations from <see cref="IRepository{T}"/> and adds product-specific query methods.
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    /// <summary>
    /// Retrieves a product by its SKU (Stock Keeping Unit) asynchronously.
    /// </summary>
    /// <param name="sku">The product SKU.</param>
    /// <returns>The product if found, or null if it does not exist.</returns>
    Task<Product?> GetBySkuAsync(string sku);
}
