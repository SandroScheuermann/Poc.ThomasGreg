using Poc.ThomasGreg.Application.Services;
using Poc.ThomasGreg.Application.Services.Interfaces;
using Poc.ThomasGreg.Domain.Interfaces;
using Poc.ThomasGreg.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "sua_aplicacao",
            ValidAudience = "sua_aplicacao",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("sua_chave_super_secreta_32+caracteres"))
        };
    });


builder.Services.AddAuthorization();


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
