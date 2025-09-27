using LAB5_RodrigoApaza.DTOs;

namespace LAB5_RodrigoApaza.Services.Interfaces;

public interface IAuthService
{
    Task<UsuarioDto?> LoginAsync(LoginDto loginDto);
    Task<UsuarioDto?> RegisterAsync(RegisterUsuarioDto registerDto);
}