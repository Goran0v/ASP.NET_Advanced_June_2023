using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Models.Ad;
using SoftUniBazar.Services.Interfaces;

namespace SoftUniBazar.Services
{
    public class AdService : IAdService
    {
        private readonly BazarDbContext dbContext;

        public AdService(BazarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<HashSet<AdAllViewModel>> AllAsync(HashSet<AdAllViewModel> models)
        {
            if (models.Count < 1)
            {
                models = new HashSet<AdAllViewModel>();
            }

            var ads = await this.dbContext
                .Ads
                .ToArrayAsync();

            foreach (Ad ad in ads)
            {
                IdentityUser owner = await dbContext.Users.FirstAsync(o => o.Id == ad.OwnerId);
                Category category = await dbContext.Categories.FirstAsync(c => c.Id == ad.CategoryId);

                AdAllViewModel model = new AdAllViewModel()
                {
                    Id = ad.Id,
                    Name = ad.Name,
                    Description = ad.Description,
                    Price = ad.Price,
                    OwnerId = ad.OwnerId,
                    Owner = owner.UserName,
                    ImageUrl = ad.ImageUrl,
                    CreatedOn = ad.CreatedOn,
                    CategoryId = ad.CategoryId,
                    Category = category.Name
                };

                if (!models.Contains(model))
                {
                    models.Add(model);
                }
            }

            return models;
        }

        public async Task CreateAnAdAsync(AddingAdViewModel model, string ownerId)
        {
            IdentityUser owner = await this.dbContext.Users.FirstAsync(u => u.Id == ownerId);
            Category category = await this.dbContext.Categories.FirstAsync(c => c.Id == model.CategoryId);

            Ad ad = new Ad()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                OwnerId = ownerId,
                Owner = owner,
                ImageUrl = model.ImageUrl,
                CreatedOn = DateTime.Now,
                CategoryId = model.CategoryId,
                Category = category
            };

            await this.dbContext.Ads.AddAsync(ad);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task GenerateAdCategoriesAsync(AddingAdViewModel model)
        {
            var categories = await this.dbContext
                .Categories
                .ToArrayAsync();

            foreach (var category in categories)
            {
                model.Categories.Add(category);
            }
        }

        public async Task<bool> AdExistsByIdAsync(int id)
        {
            Ad? ad = await this.dbContext
                .Ads
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ad == null)
            {
                return false;
            }

            return true;
        }

        public async Task<AddingAdViewModel> GetAdForEditByIdAsync(int id)
        {
            Ad ad = await this.dbContext
                .Ads
                .FirstAsync(a => a.Id == id);

            AddingAdViewModel viewModel = new AddingAdViewModel()
            {
                Id = ad.Id,
                Name = ad.Name,
                Description = ad.Description,
                Price = ad.Price,
                OwnerId = ad.OwnerId,
                ImageUrl = ad.ImageUrl,
                CategoryId = ad.CategoryId
            };

            return viewModel;
        }

        public async Task EditAdByIdAndModelAsync(int id, AddingAdViewModel model)
        {
            Ad ad = await this.dbContext
                .Ads
                .FirstAsync(a => a.Id == id);

            ad.Name = model.Name;
            ad.Description = model.Description;
            ad.Price = model.Price;
            ad.ImageUrl = model.ImageUrl;
            ad.CategoryId = model.CategoryId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task AddToCartAsync(int id, string buyerId)
        {
            Ad ad = await this.dbContext
                .Ads
                .FirstAsync(a => a.Id == id);

            IdentityUser buyer = await this.dbContext
                .Users
                .FirstAsync(u => u.Id == buyerId);

            AdBuyer? adBuyer = await this.dbContext
                .AdsBuyers
                .FirstOrDefaultAsync(ab => ab.AdId == id && ab.BuyerId == buyerId);

            if (adBuyer == null)
            {
                adBuyer = new AdBuyer()
                {
                    Ad = ad,
                    AdId = ad.Id,
                    Buyer = buyer,
                    BuyerId = buyer.Id
                };

                await this.dbContext.AdsBuyers.AddAsync(adBuyer);
            }

            await this.dbContext.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(int id, string buyerId)
        {
            AdBuyer? adBuyer = await this.dbContext
                .AdsBuyers
                .FirstOrDefaultAsync(ab => ab.AdId == id && ab.BuyerId == buyerId);

            if (adBuyer != null)
            {
                this.dbContext.AdsBuyers.Remove(adBuyer);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task<HashSet<AdAllViewModel>> AllAdsByBuyerId(HashSet<AdAllViewModel> models, string id)
        {
            if (models.Count < 1)
            {
                models = new HashSet<AdAllViewModel>();
            }

            var adsBuyers = await this.dbContext
                .AdsBuyers
                .Where(ab => ab.BuyerId == id)
                .ToArrayAsync();

            foreach (AdBuyer ab in adsBuyers)
            {
                Ad ad = await this.dbContext.Ads.FirstAsync(a => a.Id == ab.AdId);
                IdentityUser owner = await this.dbContext.Users.FirstAsync(o => o.Id == ad.OwnerId);
                Category category = await this.dbContext.Categories.FirstAsync(c => c.Id == ad.CategoryId);

                AdAllViewModel model = new AdAllViewModel()
                {
                    Id = ad.Id,
                    Name = ad.Name,
                    Description = ad.Description,
                    Price = ad.Price,
                    OwnerId = ad.OwnerId,
                    Owner = owner.UserName,
                    ImageUrl = ad.ImageUrl,
                    CreatedOn = ad.CreatedOn,
                    CategoryId = ad.CategoryId,
                    Category = category.Name
                };

                if (!models.Contains(model))
                {
                    models.Add(model);
                }
            }

            return models;
        }
    }
}