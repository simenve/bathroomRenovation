namespace BathroomRenovation.Domain.Models
{
    public class BathroomItem
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public List<BathroomItemBrand> AvailableBrands { get; set; } = new();

        public List<BathroomItemOrder> BathroomItemOrders { get; set; } = new();
    }
}
