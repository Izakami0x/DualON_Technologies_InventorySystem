using System;

namespace DualON_Technologies_InventorySystem.Models
{
    public enum MovementType
    {
        Receive = 1,
        Sale = 2,
        Returned = 3
    }

    public class StockMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public MovementType Type { get; set; }
        public int Quantity { get; set; }
        public string? ClientName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}