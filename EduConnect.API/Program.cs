using EduConnect.API.Hubs;
using EduConnect.BLL.Interfaces;
using EduConnect.BLL.Services;
using EduConnect.DAL.DataContext;
using EduConnect.DAL.Interface;
using EduConnect.DAL.Repositories;
using EduConnect.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Cors;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost","localhost", "http://localhost:8080")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // Permite enviar las credenciales (tokens) en las solicitudes
    });
});
builder.Services.AddDbContext<EduConnectPruebasContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDB"));
});



builder.Services.AddScoped<IGenericRepository<User>, UserRepository>();
builder.Services.AddScoped<IGenericRepository<College>, CollegeRepository>();
builder.Services.AddScoped<IGenericRepository<History>, HistoryRepository>();
builder.Services.AddScoped<IGenericRepository<Request>, RequestRepository>();
builder.Services.AddScoped<IGenericRepository<Role>, RoleRepository>();
builder.Services.AddScoped<IGenericRepository<Chat>, ChatRepository>();
builder.Services.AddScoped<IGenericRepository<Department>, DepartmentRepository>();
builder.Services.AddScoped<IGenericRepository<City>, CityRepository>();
builder.Services.AddScoped<IGenericRepository<ChatMessage>, ChatMessageRepository>();
builder.Services.AddScoped<IGenericRepository<Connection>, ConnectionRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICollegeService, CollegeService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IConnectionService, ConnectionService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseWebSockets();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("Hubs/ChatHub");

app.Run();
