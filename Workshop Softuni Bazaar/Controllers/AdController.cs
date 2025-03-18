using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Extensions;
using SoftUniBazar.Models.Ad;
using SoftUniBazar.Services.Interfaces;

namespace SoftUniBazar.Controllers
{
    [Authorize]
    public class AdController : Controller
    {
        private readonly IAdService adService;

        public AdController(IAdService adService)
        {
            this.adService = adService;
        }

        [HttpGet]
        public async Task<IActionResult> All(HashSet<AdAllViewModel> models)
        {
            return this.View(await this.adService.AllAsync(models));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddingAdViewModel model = new AddingAdViewModel();
            await this.adService.GenerateAdCategoriesAsync(model);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddingAdViewModel model)
        {
            await this.adService.CreateAnAdAsync(model, this.User.GetId()!);

            return this.RedirectToAction("All", "Ad");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            bool adExists = await this.adService
                .AdExistsByIdAsync(id);

            if (!adExists)
            {
                return this.RedirectToAction("All", "Ad");
            }

            AddingAdViewModel model = await this.adService
                .GetAdForEditByIdAsync(id);
            await this.adService.GenerateAdCategoriesAsync(model);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddingAdViewModel model)
        {
            bool adExists = await this.adService
                .AdExistsByIdAsync(id);

            if (!adExists)
            {
                return this.RedirectToAction("Edit", "Ad");
            }

            await this.adService.EditAdByIdAndModelAsync(id, model);

            return this.RedirectToAction("All", "Ad");
        }

        [HttpGet]
        public async Task<IActionResult> Cart(HashSet<AdAllViewModel> models)
        {
            return this.View(await this.adService.AllAdsByBuyerId(models, this.User.GetId()!));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            bool adExists = await this.adService
                .AdExistsByIdAsync(id);

            if (!adExists)
            {
                return this.RedirectToAction("All", "Ad");
            }

            await this.adService.AddToCartAsync(id, this.User.GetId()!);

            return this.RedirectToAction("Cart", "Ad");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            bool adExists = await this.adService
                .AdExistsByIdAsync(id);

            if (!adExists)
            {
                return this.RedirectToAction("Cart", "Ad");
            }

            await this.adService.RemoveFromCartAsync(id, this.User.GetId()!);

            return RedirectToAction("All", "Ad");
        }
    }
}