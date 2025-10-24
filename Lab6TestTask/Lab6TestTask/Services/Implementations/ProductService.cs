    using Lab6TestTask.Data;
using Lab6TestTask.Models;
using Lab6TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab6TestTask.Services.Implementations;

/// <summary>
/// ProductService.
/// Implement methods here.
/// </summary>
public class ProductService : IProductService
{
    private readonly ApplicationDbContext _dbContext;

    public ProductService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> GetProductAsync()
    {
        var product = await _dbContext.Products
            .Where(p => p.Status == Enums.ProductStatus.Reserved)
            .OrderByDescending(p => p.Price)
            .FirstOrDefaultAsync();

        if (product == null)
            throw new InvalidOperationException("Продукт не найден");

        return product;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var products = await _dbContext.Products
            .Where(p => p.ReceivedDate.Year == 2025 && p.Quantity > 1000)
            .ToListAsync();
        return products;
    }
}
