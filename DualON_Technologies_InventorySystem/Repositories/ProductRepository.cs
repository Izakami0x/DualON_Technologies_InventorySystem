using DualON_Technologies_InventorySystem.Data;
using DualON_Technologies_InventorySystem.Models;
using DualON_Technologies_InventorySystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DualON_Technologies_InventorySystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByCodeAsync(string code)
        {
            return await _context.Products
                .Include(p => p.StockMovements)
                .FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.StockMovements)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}