using Microsoft.EntityFrameworkCore;
using WebAPI.Contexts;
using WebAPI.Interfaces.Repository;
using WebAPI.Interfaces.Service;
using WebAPI.Models;
using WebAPI.Repository;
using WebAPI.Services;
using WebAPI.Utility;
using Azure.Storage.Blobs;
using DotNetEnv;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = Environment.GetEnvironmentVariable("SQL_SERVER_CONNECTION_STRING");

builder.Services.AddDbContext<AtmAppContext>(
    options => options.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IRepository<int, Account>, AccountRepository>();
builder.Services.AddScoped<IRepository<int, Transaction>, TransactionRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register BlobServiceClient with the connection string
string blobConnectionString = Environment.GetEnvironmentVariable("AZURE_BLOB_CONNECTION_STRING");
builder.Services.AddSingleton(new BlobServiceClient(blobConnectionString));

// Add controllers
builder.Services.AddControllers();

// CORS
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AllowAllCorsPolicy", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAllCorsPolicy");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();