using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

builder.Services.AddApplicationServices(builder.Configuration)
                .AddInfrastructure(builder.Configuration)
                .AddApiServices(builder.Configuration);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiServices();
await app.InitialiseDatabaseAsync();

app.Run();