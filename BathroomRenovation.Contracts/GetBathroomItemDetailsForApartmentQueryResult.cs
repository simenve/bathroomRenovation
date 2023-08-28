namespace BathroomRenovation.Contracts
{
    public record GetBathroomItemDetailsForApartmentQueryResult()
    {
        public int BathroomItemId { get; set; }

        public int ApartmentId { get; set; }

        public required string BathroomItemTitle { get; set; }

        public List<BathroomItemAvailableBrandsDto> BathroomItemAvailableBrandsDtos { get; set; } = new();

        public BathroomItemOrderDto? BathroomItemOrderDto { get; set; }

    }

    public record BathroomItemAvailableBrandsDto(int BathroomItemBrandId, string BathroomItemBrandTitle);

    public record BathroomItemOrderDto(int BathroomItemOrderId, int BathroomItemBrandId, string BathroomItemBrandTitle);
}
