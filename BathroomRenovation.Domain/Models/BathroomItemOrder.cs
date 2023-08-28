using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BathroomRenovation.Domain.Models
{
    // Only one order of a certain BathroomItem can exist per Apartment
    [Index(nameof(ApartmentId), nameof(BathroomItemId), IsUnique = true)]
    public class BathroomItemOrder
    {
        // Enforce which brands can be selected for BathroomItem
        public BathroomItemOrder(BathroomItem bathroomItem, BathroomItemBrand bathroomItemBrand)
        {
            if (bathroomItem.AvailableBrands.Any(b => b.Title == bathroomItemBrand.Title))
            {
                BathroomItem = bathroomItem;
                BathroomItemBrand = bathroomItemBrand;
            }
            else
            {
                throw new ArgumentException("Brand not available for bathroom item");
            }
        }

        public BathroomItemOrder() { }

        public int Id { get; set; }

        public int ApartmentId { get; set; }

        [ForeignKey(nameof(ApartmentId))]
        public Apartment Apartment { get; set; } = null!;

        public int BathroomItemId { get; private set; }

        [ForeignKey(nameof(BathroomItemId))]
        public BathroomItem BathroomItem { get; private set; } = null!;

        public int BathroomItemBrandId { get; private set; }

        [ForeignKey(nameof(BathroomItemBrandId))]
        public BathroomItemBrand BathroomItemBrand { get; private set; } = null!;
    }
}
