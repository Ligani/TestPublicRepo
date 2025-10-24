using Lab6TestTask.Data;
using Lab6TestTask.Models;
using Lab6TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab6TestTask.Services.Implementations;

/// <summary>
/// WarehouseService.
/// Implement methods here.
/// </summary>
public class WarehouseService : IWarehouseService
{
    private readonly ApplicationDbContext _dbContext;

    public WarehouseService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Warehouse> GetWarehouseAsync()
    {
        var warehouse = await _dbContext.Warehouses
            .OrderByDescending(wh => wh.Products.Sum(p => p.Price))
            .FirstOrDefaultAsync();
        if (warehouse == null)
        {
            throw new InvalidOperationException("Склад не найден");
        }
        return warehouse;
    }

    public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
    {
        var start = new DateTime(2025, 4, 1); 
        var end = new DateTime(2025, 7, 1);

        var warehouses = await _dbContext.Warehouses
            .Where(wh => wh.Products.Any(p => p.ReceivedDate >= start && p.ReceivedDate < end))
            .ToListAsync();
        return warehouses;
    }
}
