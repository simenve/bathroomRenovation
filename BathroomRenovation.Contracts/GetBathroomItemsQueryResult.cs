namespace BathroomRenovation.Contracts
{
    public record GetBathroomItemsQueryResult(List<BathroomItemDto> BathroomItems);

    public record BathroomItemDto(int Id, string Title, string Description);
}
