using System.Collections.Generic;

namespace DualON_Technologies_InventorySystem.Models
{
    public class Product
    {
        public int Id { get; private set; }
        public string Code { get; private set; } = null!;
        public string Model { get; private set; } = null!;
        public string Brand { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal DealerPrice { get; set; }

        public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

        private Product() { } // For EF Core

        public Product(string code, string model, string brand, string description, decimal dealerPrice)
        {
            Code = code;
            Model = model;
            Brand = brand;
            Description = description;
            DealerPrice = dealerPrice;
        }
    }
}