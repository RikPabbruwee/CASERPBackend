using DAL.DataAcess;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(10);
//});
IConfigurationBuilder configbuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
IConfiguration configuration = configbuilder.Build();
string DatabaseConnectionString = configuration.GetConnectionString("database");
builder.Services.AddDbContext<CursusContext>(options =>
{
    options.UseSqlServer(DatabaseConnectionString);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
    {
        //policy.AllowAnyOrigin();
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyMethod().AllowCredentials().AllowAnyHeader();
    });
});
builder.Services.AddTransient<ICursusRepository, CursusDatabaseRepository>();
builder.Services.AddTransient<ICursusInstantieRepository, CursusInstantieDatabaseRepository>();
builder.Services.AddTransient<IFavoriteWeekRepository, FavoriteWeekDatabaseRepository>();
builder.Services.AddTransient<ICursistRepository, CursistDatabaseRepository>();
var app = builder.Build();
app.UseCors("frontend");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();
//app.UseSession();
app.Run();
