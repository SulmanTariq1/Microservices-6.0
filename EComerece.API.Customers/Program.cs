using EComerece.API.Customers.DB;
using EComerece.API.Customers.Interfaces;
using EComerece.API.Customers.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//My services

builder.Services.AddScoped<ICustomerProvider, CustomersProvider>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<CustomerDbContext>(options =>
{
    options.UseInMemoryDatabase("Customer");
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
