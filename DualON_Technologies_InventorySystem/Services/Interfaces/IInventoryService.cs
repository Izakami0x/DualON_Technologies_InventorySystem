using DualON_Technologies_InventorySystem.Models;

namespace DualON_Technologies_InventorySystem.Services.Interfaces
{
    public interface IInventoryService
    {
        Task RegisterProductAsync(string code, string model, string brand, string description, decimal dealerPrice);

        Task UpdateProductAsync(int productId, string brand, string description, decimal dealerPrice);

        Task ReceiveStockAsync(string code, int quantity);

        Task SellStockAsync(string code, int quantity, string client);

        Task<int> GetCurrentStockAsync(string code);
    }
}