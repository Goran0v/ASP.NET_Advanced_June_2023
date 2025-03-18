using HouseRentingSystem.Services.Data.Models.House;
using HouseRentingSystem.Services.Data.Models.Statistics;
using HouseRentingSystem.Web.ViewModels.Home;
using HouseRentingSystem.Web.ViewModels.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Services.Data.Interfaces
{
    public interface IHouseService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeHousesAsync();

        Task<string> CreateAndReturnIdAsync(HouseFormModel formModel, string agentId);

        Task<AllHousesFilteredAndPagedServiceModel> AllAsync(AllHousesQueryModel queryModel);

        Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId);

        Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userId);

        Task<HouseDetailsViewModel> GetDetailsByIdAsync(string houseId);

        Task<bool> ExistsByIdAsync(string houseId);

        Task<HouseFormModel> GetHouseForEditByIdAsync(string houseId);

        Task<bool> IsAgentWithIdOwnerOfHouseWithIdAsync(string houseId, string agentId);

        Task EditHouseByIdAndFormModelAsync(string houseId, HouseFormModel model);

        Task<HousePreDeleteDetailsViewModel> GetHouseForDeleteByIdAsync(string houseId);

        Task DeleteHouseByIdAsync(string houseId);

        Task<bool> IsRentedAsync(string houseId);

        Task RentHouseAsync(string houseId, string userId);

        Task<bool> IsRentedByUserWithIdAsync(string houseId, string userId);

        Task LeaveHouseAsync(string houseId);

        Task<StatisticsServiceModel> GetStatisticsAsync();
    }
}
