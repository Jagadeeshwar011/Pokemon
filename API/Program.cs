using System.Text.Json.Serialization;
using PokeApi.Logic;
using PokeApi.Helpers;
using PokeApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
 
services.AddDbContext<PokeApi.Helpers.DataContext>();
services.AddCors();
services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // ignore omitted parameters on models to enable optional params (e.g. User update)
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// configure DI for application services
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IPokemanRepository, PokemanRepository>();
services.AddScoped<IUserFavoriteRepository, UserFavoriteRepository>();
services.AddScoped<IUserLogic, UserLogic>();
services.AddScoped<IPokemanLogic, PokemanLogic>();
services.AddScoped<IUserFavoriteLogic, UserFavoriteLogic>();


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
