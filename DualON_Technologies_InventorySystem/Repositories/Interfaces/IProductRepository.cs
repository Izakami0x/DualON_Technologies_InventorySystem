using DualON_Technologies_InventorySystem.Models;

namespace DualON_Technologies_InventorySystem.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByCodeAsync(string code);
        Task<Product?> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();

        Task AddAsync(Product product);
        void Update(Product product);

        Task SaveChangesAsync();
    }
}