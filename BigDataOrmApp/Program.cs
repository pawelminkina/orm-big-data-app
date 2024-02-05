using BigDataOrmApp.Application.Services.BigData;
using BigDataOrmApp.Application.Services.Data;
using BigDataOrmApp.Infrastructure;
using BigDataOrmApp.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBigDataService, BigDataService>();
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddDbContext<DataDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!);
});

builder.Services.Configure<ElasticSearchConfig>(builder.Configuration.GetSection("ElasticSearch"));
builder.Services.AddLogging(s=>s.AddConsole());
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
