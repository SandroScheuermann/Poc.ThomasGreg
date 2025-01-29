using Poc.ThomasGreg.Application.Services.Interfaces;
using Poc.ThomasGreg.Application.Services;
using Poc.ThomasGreg.Domain.Interfaces;
using Poc.ThomasGreg.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ILogradouroRepository, LogradouroRepository>(); 

// Register services
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ILogradouroService, LogradouroService>();

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
