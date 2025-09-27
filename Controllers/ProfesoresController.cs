using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAB5_RodrigoApaza.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "AdminOnly")]
public class ProfesoresController : ControllerBase
{
    private readonly IProfesorService _profesorService;

    public ProfesoresController(IProfesorService profesorService)
    {
        _profesorService = profesorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var profesores = await _profesorService.GetAllProfesoresAsync();
        return Ok(profesores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var profesor = await _profesorService.GetProfesorByIdAsync(id);
        if (profesor == null) return NotFound();
        return Ok(profesor);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CrearProfesorDto dto)
    {
        var creado = await _profesorService.CreateProfesorAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = creado.IdProfesor }, creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CrearProfesorDto dto)
    {
        var result = await _profesorService.UpdateProfesorAsync(id, dto);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _profesorService.DeleteProfesorAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}