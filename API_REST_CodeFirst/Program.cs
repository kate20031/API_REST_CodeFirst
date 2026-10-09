using API_REST_CodeFirst.Models.DataManager;
using API_REST_CodeFirst.Models.EntityFramework;
using API_REST_CodeFirst.Models.Repository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(
    AppDomain.CurrentDomain.GetAssemblies()
);


builder.Services.AddDbContext<CinemaContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("CinemaConnection")
    ));

builder.Services.AddDbContext<SeriesContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("SeriesConnection")
    ));


builder.Services.AddScoped<IDataRepository<User>, UserManager>();
builder.Services.AddScoped<ISerieManager, SerieManager>();

builder.Services.AddScoped<IMovieManager, MovieManager>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
