using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Infrastructure.Models;
using LAB5_RodrigoApaza.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LAB5_RodrigoApaza.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<UsuarioDto?> LoginAsync(LoginDto loginDto)
    {
        var usuarios = await _unitOfWork.Repository<Usuario>().GetAllAsync();
        var usuario = usuarios.FirstOrDefault(u => u.NombreUsuario.Equals(loginDto.NombreUsuario, StringComparison.OrdinalIgnoreCase));

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.PasswordHash))
        {
            return null;
        }

        var token = GenerateJwtToken(usuario);

        return new UsuarioDto
        {
            IdUsuario = usuario.IdUsuario,
            NombreUsuario = usuario.NombreUsuario,
            Rol = usuario.Rol,
            Token = token
        };
    }
    
    public async Task<UsuarioDto?> RegisterAsync(RegisterUsuarioDto registerDto)
    {
        var usuarioRepository = _unitOfWork.Repository<Usuario>();
        var usuarios = await usuarioRepository.GetAllAsync();
        var nombreUsuarioExiste = usuarios.Any(u => u.NombreUsuario.Equals(registerDto.NombreUsuario, StringComparison.OrdinalIgnoreCase));

        if (nombreUsuarioExiste)
        {
            return null;
        }

        var nuevoUsuario = new Usuario
        {
            NombreUsuario = registerDto.NombreUsuario,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            Rol = registerDto.Rol
        };

        var usuarioCreado = await usuarioRepository.AddAndSaveAsync(nuevoUsuario);
        var token = GenerateJwtToken(usuarioCreado);

        return new UsuarioDto
        {
            IdUsuario = usuarioCreado.IdUsuario,
            NombreUsuario = usuarioCreado.NombreUsuario,
            Rol = usuarioCreado.Rol,
            Token = token
        };
    }

    private string GenerateJwtToken(Usuario usuario)
    {
        var secretKey = _configuration["Jwt:SecretKey"]
                        ?? throw new InvalidOperationException("La clave secreta para JWT no está configurada.");
        var issuer = _configuration["Jwt:Issuer"]
                     ?? throw new InvalidOperationException("El emisor para JWT no está configurado.");
        var audience = _configuration["Jwt:Audience"]
                       ?? throw new InvalidOperationException("La audiencia para JWT no está configurada.");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.NombreUsuario),
            new Claim(ClaimTypes.Name, usuario.NombreUsuario),
            new Claim("id", usuario.IdUsuario.ToString()),
            new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
            new Claim(ClaimTypes.Role, usuario.Rol),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}