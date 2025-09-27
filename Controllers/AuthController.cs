using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAB5_RodrigoApaza.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var usuarioDto = await _authService.LoginAsync(loginDto);

        if (usuarioDto == null)
        {
            return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
        }

        return Ok(usuarioDto);
    }

    [HttpPost("register")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Register(RegisterUsuarioDto registerDto)
    {
        var usuarioDto = await _authService.RegisterAsync(registerDto);

        if (usuarioDto == null)
        {
            return Conflict(new { message = "El nombre de usuario ya está registrado" });
        }

        return Ok(usuarioDto);
    }

    [HttpGet("perfil")]
    [Authorize]
    public IActionResult GetProfile()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("id");
        var nombreClaim = User.FindFirst(ClaimTypes.Name) ?? User.FindFirst(JwtRegisteredClaimNames.Sub);
        var rolClaim = User.FindFirst(ClaimTypes.Role);

        if (idClaim == null || nombreClaim == null || rolClaim == null)
        {
            return Unauthorized(new { message = "No se pudo determinar la identidad del usuario." });
        }

        _ = int.TryParse(idClaim.Value, out var idUsuario);

        return Ok(new
        {
            IdUsuario = idUsuario,
            NombreUsuario = nombreClaim.Value,
            Rol = rolClaim.Value
        });
    }

    [HttpGet("admin/reportes")]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult GetAdminReports()
    {
        return Ok(new
        {
            message = "Información confidencial disponible únicamente para administradores"
        });
    }

    [HttpGet("profesores/herramientas")]
    [Authorize(Policy = "ProfesorOnly")]
    public IActionResult GetProfesorTools()
    {
        return Ok(new
        {
            message = "Recursos exclusivos para el personal docente"
        });
    }

    [HttpGet("estudiantes/seguimiento")]
    [Authorize(Policy = "EstudianteOnly")]
    public IActionResult GetStudentProgress()
    {
        return Ok(new
        {
            message = "Panel de seguimiento disponible para estudiantes registrados"
        });
    }
}