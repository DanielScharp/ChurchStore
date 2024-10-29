using ChurchStore.Api;
using ChurchStore.Api.Hubs;
using ChurchStore.Api.Mail;
using ChurchStore.Api.Services;
using ChurchStore.App;
using ChurchStore.Database.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = builder.Configuration;

string connectionString = configuration.GetConnectionString("MySqlConnection");

// Adicionando configuração de EmailSettings para injeção de dependência
builder.Services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
builder.Services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));

builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebAdmin",
        policy => policy
            .WithOrigins("https://localhost:7038") // Altere para a URL correta do WebAdmin
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

// Criando instâncias de UsuarioRepositorio      
var usuariosRepositorio = new UsuariosRepositorio(connectionString);
var produtosRepositorio = new ProdutosRepositorio(connectionString);
var pedidosRepositorio = new PedidosRepositorio(connectionString);

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

// Adicionando as instâncias ao contêiner de serviços
builder.Services.AddSingleton(usuariosRepositorio);
builder.Services.AddSingleton(produtosRepositorio);
builder.Services.AddSingleton(pedidosRepositorio);

builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<MailSender>();

builder.Services.AddScoped<UsuariosApplication>();
builder.Services.AddScoped<ProdutosApplication>();
builder.Services.AddScoped<PedidosApplication>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddControllers();
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
app.UseCors("AllowWebAdmin");  // Utilize o CORS antes do MapHub
app.MapHub<PedidoHub>("/pedidoHub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();