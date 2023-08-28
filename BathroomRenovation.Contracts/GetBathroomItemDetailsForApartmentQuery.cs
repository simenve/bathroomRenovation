using MediatR;

namespace BathroomRenovation.Contracts
{
    public record GetBathroomItemDetailsForApartmentQuery(int bathroomItemId, int apartmentId) : IRequest<GetBathroomItemDetailsForApartmentQueryResult>
    {
    }
}
