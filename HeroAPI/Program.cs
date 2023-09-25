using HeroAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddCors(x =>
//{ 
//    x.AddPolicy("AllowAll", z => z.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
//});

builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HeroDbContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("HeroDbContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(z => z.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.MapControllers();

app.Run();
