using MediatR;

namespace BathroomRenovation.Contracts
{
    public record GetBathroomItemsQuery() : IRequest<GetBathroomItemsQueryResult>
    {
    }
}
