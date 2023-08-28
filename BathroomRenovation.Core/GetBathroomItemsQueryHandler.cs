using BathroomRenovation.Contracts;
using BathroomRenovation.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BathroomRenovation.Core
{
    public class GetBathroomItemsQueryHandler : IRequestHandler<GetBathroomItemsQuery, GetBathroomItemsQueryResult>
    {
        private readonly BathroomRenovationDbContext _context;

        public GetBathroomItemsQueryHandler(BathroomRenovationDbContext context)
        {
            _context = context;
        }

        public async Task<GetBathroomItemsQueryResult> Handle(GetBathroomItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.BathroomItems.ToListAsync(cancellationToken);

            var result = items.Select(item => new BathroomItemDto(item.Id, item.Title, item.Description)).ToList();

            return new GetBathroomItemsQueryResult(result);
        }
    }
}
