namespace BathroomRenovation.Domain.Models
{
    public class Apartment
    {
        public int Id { get; set; }

        public required int ApartmentNumber { get; set; }

        public List<User> Owners { get; set; } = new();

        public List<BathroomItemOrder> BathroomItemOrders { get; set; } = new();
    }
}
