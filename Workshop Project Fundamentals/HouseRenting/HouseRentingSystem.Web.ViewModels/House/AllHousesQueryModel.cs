﻿using HouseRentingSystem.Web.ViewModels.House.Enums;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Common.GeneralApplicationsConstants;

namespace HouseRentingSystem.Web.ViewModels.House
{
    public class AllHousesQueryModel
    {
        public AllHousesQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.HousesPerPage = EntitiesPerPage;
            this.Categories = new HashSet<string>();
            this.Houses = new HashSet<HouseAllViewModel>();
        }

        public string? Category { get; set; }

        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Houses By")]
        public HouseSorting HouseSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Show Houses On Page")]
        public int HousesPerPage { get; set; }

        public int TotalHouses { get; set; }

        public IEnumerable<string> Categories { get; set; } = null!;

        public IEnumerable<HouseAllViewModel> Houses { get; set; }
    }
}
