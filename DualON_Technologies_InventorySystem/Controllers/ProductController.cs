using DualON_Technologies_InventorySystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DualON_Technologies_InventorySystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public ProductController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}