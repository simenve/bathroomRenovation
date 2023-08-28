using BathroomRenovation.Contracts;
using BathroomRenovation.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BathroomRenovation.Core
{
    public class GetBathroomItemDetailsForApartmentQueryHandler : IRequestHandler<GetBathroomItemDetailsForApartmentQuery, GetBathroomItemDetailsForApartmentQueryResult>
    {
        private readonly BathroomRenovationDbContext _dbContext;

        public GetBathroomItemDetailsForApartmentQueryHandler(BathroomRenovationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetBathroomItemDetailsForApartmentQueryResult> Handle(GetBathroomItemDetailsForApartmentQuery request, CancellationToken cancellationToken)
        {
            var bathroomItemOrder = await _dbContext.BathroomItemOrders
                .Include(b => b.BathroomItemBrand)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.BathroomItemId == request.bathroomItemId && o.ApartmentId == request.apartmentId, cancellationToken);

            var bathroomItem = await _dbContext.BathroomItems
                .Include(b => b.AvailableBrands)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == request.bathroomItemId, cancellationToken);

            if (bathroomItemOrder is null || bathroomItem is null)
            {
                throw new Exception("Some better exception...");
            }

            var result = new GetBathroomItemDetailsForApartmentQueryResult()
            {
                ApartmentId = request.apartmentId,
                BathroomItemId = bathroomItem.Id,
                BathroomItemTitle = bathroomItem.Title,
                BathroomItemAvailableBrandsDtos = bathroomItem.AvailableBrands.Select(b => new BathroomItemAvailableBrandsDto(b.Id, b.Title)).ToList(),
                BathroomItemOrderDto = new BathroomItemOrderDto(bathroomItemOrder.Id, bathroomItemOrder.BathroomItemBrand.Id, bathroomItemOrder.BathroomItemBrand.Title)
            };

            return result;
        }
    }
}
