using SoftUniBazar.Data;
using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Common.ApplicationConstants.Ad;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace SoftUniBazar.Models.Ad
{
    public class AddingAdViewModel
    {
        public AddingAdViewModel()
        {
            this.Categories = new HashSet<Category>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string OwnerId { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public int CategoryId { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = null!;
    }
}
