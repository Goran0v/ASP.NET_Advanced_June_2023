using SoftUniBazar.Models.Ad;

namespace SoftUniBazar.Services.Interfaces
{
    public interface IAdService
    {
        Task<HashSet<AdAllViewModel>> AllAsync(HashSet<AdAllViewModel> models);

        Task CreateAnAdAsync(AddingAdViewModel model, string ownerId);

        Task GenerateAdCategoriesAsync(AddingAdViewModel model);

        Task<bool> AdExistsByIdAsync(int id);

        Task<AddingAdViewModel> GetAdForEditByIdAsync(int id);

        Task EditAdByIdAndModelAsync(int id, AddingAdViewModel model);

        Task<HashSet<AdAllViewModel>> AllAdsByBuyerId(HashSet<AdAllViewModel> models, string id);

        Task AddToCartAsync(int id, string buyerId);

        Task RemoveFromCartAsync(int id, string buyerId);
    }
}
