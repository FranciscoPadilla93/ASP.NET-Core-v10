using Api.Middlewares;
using Business.Services;
using Business.Validators;
using Data.Repositories;
using FluentValidation;
using static Data.Context.Context;

var builder = WebApplication.CreateBuilder(args);

// Configuración CORS
var misReglasCors = "ReglasCorsAngular";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: misReglasCors,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

// 1. Dependencias
builder.Services.AddSingleton<DapperContext>();

// Repositorios (Capa Data)
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

// Servicios
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILoginService, LoginService>();

// Validador
builder.Services.AddValidatorsFromAssemblyContaining<UserSetValidator>();

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

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
//CORS
app.UseCors(misReglasCors);
// AUTHORIZACION POR JWT
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
