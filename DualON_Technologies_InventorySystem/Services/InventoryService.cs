using DualON_Technologies_InventorySystem.Data;
using DualON_Technologies_InventorySystem.Models;
using DualON_Technologies_InventorySystem.Repositories.Interfaces;
using DualON_Technologies_InventorySystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DualON_Technologies_InventorySystem.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _context;

        public InventoryService(IProductRepository productRepository, AppDbContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }

        public async Task RegisterProductAsync(string code, string model, string brand, string description, decimal dealerPrice)
        {
            var existing = await _productRepository.GetByCodeAsync(code);

            if (existing != null)
                throw new Exception("Product code already exists.");

            var product = new Product(code, model, brand, description, dealerPrice);

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(int productId, string brand, string description, decimal dealerPrice)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
                throw new Exception("Product not found.");

            product.Brand = brand;
            product.Description = description;
            product.DealerPrice = dealerPrice;

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task ReceiveStockAsync(string code, int quantity)
        {
            if (quantity <= 0)
                throw new Exception("Quantity must be greater than zero.");

            var product = await _productRepository.GetByCodeAsync(code);

            if (product == null)
                throw new Exception("Product not registered.");

            _context.StockMovements.Add(new StockMovement
            {
                ProductId = product.Id,
                Quantity = quantity,
                Type = MovementType.Receive,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }

        public async Task SellStockAsync(string code, int quantity, string client)
        {
            if (quantity <= 0)
                throw new Exception("Quantity must be greater than zero.");

            using var transaction = await _context.Database.BeginTransactionAsync();

            var product = await _productRepository.GetByCodeAsync(code);

            if (product == null)
                throw new Exception("Product not found.");

            var stock = product.StockMovements.Sum(m =>
                m.Type == MovementType.Receive ? m.Quantity :
                m.Type == MovementType.Sale ? -m.Quantity :
                m.Quantity);

            if (stock < quantity)
                throw new Exception("Insufficient stock.");

            _context.StockMovements.Add(new StockMovement
            {
                ProductId = product.Id,
                Quantity = quantity,
                Type = MovementType.Sale,
                ClientName = client,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task<int> GetCurrentStockAsync(string code)
        {
            var product = await _productRepository.GetByCodeAsync(code);

            if (product == null)
                return 0;

            return product.StockMovements.Sum(m =>
                m.Type == MovementType.Receive ? m.Quantity :
                m.Type == MovementType.Sale ? -m.Quantity :
                m.Quantity);
        }
    }
}