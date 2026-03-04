using System.ComponentModel.DataAnnotations;

namespace DualON_Technologies_InventorySystem.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        public string Code { get; set; } = null!;

        [Required]
        public string Model { get; set; } = null!;

        [Required]
        public string Brand { get; set; } = null!;

        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal DealerPrice { get; set; }
    }
}