using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Repository;
using Helpers;
using Models;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5244");

var connectionString = builder.Configuration["ConnectionString"];

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if(string.IsNullOrEmpty(connectionString)){
    throw new InvalidOperationException("Connection string not found.");
}

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<CustomerServices>();
builder.Services.AddScoped<ISellerRepository, SellerRepository>();
builder.Services.AddScoped<SellerServices>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<OrderServices>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ItemServices>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductServices>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CategoryServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserServices>();

builder.Services.AddCors();

var app = builder.Build();

// Error Handling
app.UseMiddleware<ErrorHandlerMiddleware>();

using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<DataContext>();
        db.Database.Migrate();
    }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options =>
    options.AllowAnyHeader()
    .SetIsOriginAllowed(hostName => true)
    .AllowAnyMethod()
    .AllowCredentials()
);

app.Run();
