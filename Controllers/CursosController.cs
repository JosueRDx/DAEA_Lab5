using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAB5_RodrigoApaza.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CursosController : ControllerBase
{
    private readonly ICursoService _cursoService;

    public CursosController(ICursoService cursoService)
    {
        _cursoService = cursoService;
    }

    [HttpGet]
    [Authorize(Policy = "ProfesorOnly")]
    public async Task<IActionResult> GetAll()
    {
        var cursos = await _cursoService.GetAllCursosAsync();
        return Ok(cursos);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "ProfesorOnly")]
    public async Task<IActionResult> GetById(int id)
    {
        var curso = await _cursoService.GetCursoByIdAsync(id);
        if (curso == null) return NotFound();
        return Ok(curso);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Create(CrearCursoDto dto)
    {
        var creado = await _cursoService.CreateCursoAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = creado.IdCurso }, creado);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Update(int id, CrearCursoDto dto)
    {
        var result = await _cursoService.UpdateCursoAsync(id, dto);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _cursoService.DeleteCursoAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}