using SimpleApiCase.Database;
using SimpleApiCase.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<ISimpleDatabase, SimpleDatabase>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
