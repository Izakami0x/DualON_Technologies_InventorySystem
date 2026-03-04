using DualON_Technologies_InventorySystem.Services.Interfaces;
using DualON_Technologies_InventorySystem.ViewModels;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                await _inventoryService.RegisterProductAsync(
                    vm.Code,
                    vm.Model,
                    vm.Brand,
                    vm.Description,
                    vm.DealerPrice
                );

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }
    }
}