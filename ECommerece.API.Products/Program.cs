using ECommerece.Api.Products.DB.Interfaces;
using ECommerece.Api.Products.DB.Providers;
using ECommerece.Api.Products.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//My services

builder.Services.AddScoped<IProductProvider, ProductProvider>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseInMemoryDatabase("Products");
});

//End of My services


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
