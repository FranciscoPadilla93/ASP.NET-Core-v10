using Business.Services;
using Data.Repositories;
using static Data.Context.Context;

var builder = WebApplication.CreateBuilder(args);

// 1. Dependencias
builder.Services.AddSingleton<DapperContext>();

// Repositorios (Capa Data)
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


// Servicios
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILoginService, LoginService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

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
