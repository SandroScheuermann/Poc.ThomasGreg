using Microsoft.IdentityModel.Tokens;
using Poc.ThomasGreg.Application.Services;
using Poc.ThomasGreg.Application.Services.Interfaces;
using Poc.ThomasGreg.Domain.Interfaces;
using Poc.ThomasGreg.Infra.Repositories; 
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "valid_issuer",
            ValidAudience = "valid_audience",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes("VascoDaGamaCampeaoEterno1998Libertadores32Caracteres"))
        };
    });

if (builder.Environment.IsDevelopment())
{
	builder.WebHost.ConfigureKestrel(options =>
	{
		options.ListenAnyIP(5000);
		options.ListenAnyIP(5001);
	});
}


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ILogradouroRepository, LogradouroRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
 
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ILogradouroService, LogradouroService>();
builder.Services.AddScoped<IAutenticacaoService, AutenticacaoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); 

app.Run();
