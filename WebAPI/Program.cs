using Microsoft.EntityFrameworkCore;
using WebAPI.Contexts;
using WebAPI.Interfaces.Repository;
using WebAPI.Interfaces.Service;
using WebAPI.Models;
using WebAPI.Repository;
using WebAPI.Services;
using WebAPI.Utility;
using DotNetEnv;


DotNetEnv.Env.Load();


var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("SQL_SERVER_CONNECTION_STRING");


builder.Services.AddDbContext<AtmAppContext>(
    options => options.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IRepository<int, Account>, AccountRepository>();
builder.Services.AddScoped<IRepository<int, Transaction>, TransactionRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddControllers();

// CORS
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AllowAllCorsPolicy", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAllCorsPolicy");
//}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
