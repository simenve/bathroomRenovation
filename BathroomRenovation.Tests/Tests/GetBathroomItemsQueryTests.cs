using BathroomRenovation.Contracts;
using BathroomRenovation.Domain.Models;
using FluentAssertions;
using System.Net;
using Xunit;

namespace BathroomRenovation.Tests.Tests
{
    public class GetBathroomItemsQueryTests
    {
        [Fact]
        public async Task GetBathroomItems_Ok()
        {
            await using var factory = await TestHelper.WebAppFactoryWithInMemoryDb(seedfunction: context =>
            {
                var bathroomItems = new List<BathroomItem>()
                {
                    new()
                    {
                        Id = 1,
                        Title = "Item1",
                        Description = "Description1",
                        AvailableBrands = new List<BathroomItemBrand>()
                        {
                            new BathroomItemBrand() { Id = 1, Title = "Brand 1" },
                            new BathroomItemBrand() { Id = 2, Title = "Brand 2" }
                        },
                    },
                    new()
                    {
                        Id = 2,
                        Title = "Item2",
                        Description = "Description2"
                    },
                    new()
                    {
                        Id = 3,
                        Title = "Item3",
                        Description = "Description3"
                    }
                };

                context.AddRange(bathroomItems);
            });

            using var client = factory.CreateClient();

            var response = await client.GetAsync("/bathroomItems");
            var result = await response.DeserializeBody<GetBathroomItemsQueryResult>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.BathroomItems.Count.Should().Be(3);
            result.BathroomItems.First().Title.Should().Be("Item1");
        }
    }
}
