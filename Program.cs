using System.Text;
using Microsoft.EntityFrameworkCore;
using LAB5_RodrigoApaza.Infrastructure.Data;
using LAB5_RodrigoApaza.Services.Implementations;
using LAB5_RodrigoApaza.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AcademicDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


//  Servicios
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IMatriculaService, MatriculaService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<IEvaluacionService, EvaluacionService>();
builder.Services.AddScoped<IAsistenciaService, AsistenciaService>();
builder.Services.AddScoped<IAuthService, AuthService>(); 



// Configuración de Autenticación con JWT
var jwtSection = builder.Configuration.GetSection("Jwt");
var issuer = jwtSection["Issuer"] ?? throw new InvalidOperationException("La configuración Jwt:Issuer es obligatoria.");
var audience = jwtSection["Audience"] ?? throw new InvalidOperationException("La configuración Jwt:Audience es obligatoria.");
var secretKey = jwtSection["SecretKey"] ?? throw new InvalidOperationException("La configuración Jwt:SecretKey es obligatoria.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// Configuración de Autorización (Políticas de Roles)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("ProfesorOnly", policy => policy.RequireRole("Profesor", "Admin"));
    options.AddPolicy("EstudianteOnly", policy => policy.RequireRole("Estudiante", "Admin"));
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();