using BathroomRenovation.Contracts;
using BathroomRenovation.Core;
using BathroomRenovation.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(GetBathroomItemsQueryHandler).Assembly));

if (!builder.Environment.IsEnvironment("Test"))
{
    var db = new SqliteInMemoryDb<BathroomRenovationDbContext>(opt => new BathroomRenovationDbContext(opt));

    // Inmemory Sqlite is transient by default, it ceases to exist as soon as the database connection is closed
    // However EF Core's DbContext opens/closes connections automatically, unless an already open connection is provided
    // This is not a thread safe solution
    builder.Services.AddDbContext<BathroomRenovationDbContext>((provider, options) =>
    {
        options.UseSqlite(db.Connection);
    });
}

var app = builder.Build();

if (!builder.Environment.IsEnvironment("Test"))
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;
        using var context = services.GetRequiredService<BathroomRenovationDbContext>();

        context.Database.EnsureCreated();
        BathroomRenovationSeeder.SeedDb(context);
        await context.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/bathroomItems", async ([FromServices] IMediator mediator) => 
    await mediator.Send(new GetBathroomItemsQuery()));
app.MapGet("/bathroomItem/details/{apartmentId}/{bathroomItemId}", async ([FromServices] IMediator mediator, [FromRoute] int apartmentId, [FromRoute] int bathroomItemId) =>
    await mediator.Send(new GetBathroomItemDetailsForApartmentQuery(bathroomItemId, apartmentId)));

app.MapControllers();

app.Run();

public partial class Program { }