using BathroomRenovation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BathroomRenovation.Data
{
    public static class BathroomRenovationSeeder
    {
        public static void SeedDb(BathroomRenovationDbContext context)
        {
            var apartments = new List<Apartment>()
            {
                new()
                {
                    Id = 1,
                    ApartmentNumber = 1,
                    Owners = new List<User>
                    {
                        new()
                        {
                            Id = 1,
                            Name = "Testersen1",
                        },
                    }
                },
                new()
                {
                    Id = 2,
                    ApartmentNumber = 2,
                    Owners = new List<User>
                    {
                        new()
                        {
                            Id = 2,
                            Name = "Testersen2",
                        },
                    }
                }
            };

            context.AddRange(apartments);

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

            var bathroomItemOrder = new BathroomItemOrder(bathroomItems.First(), bathroomItems.First().AvailableBrands.First())
            {
                Id = 1,
                ApartmentId = 1,
            };

            context.Add(bathroomItemOrder);

            foreach (var bathroomItem in bathroomItems)
            {
                if (context.Entry(bathroomItem).State is EntityState.Detached)
                {
                    context.Entry(bathroomItem).State = EntityState.Added;
                }
            }
        }
    }
}
