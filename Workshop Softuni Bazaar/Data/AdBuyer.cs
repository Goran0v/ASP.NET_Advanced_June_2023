using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftUniBazar.Data
{
    public class AdBuyer
    {
        [ForeignKey(nameof(Buyer))]
        public string BuyerId { get; set; } = null!;

        public virtual IdentityUser Buyer { get; set; } = null!;

        [ForeignKey(nameof(Ad))]
        public int AdId { get; set; }

        public virtual Ad Ad { get; set; } = null!;
    }
}
