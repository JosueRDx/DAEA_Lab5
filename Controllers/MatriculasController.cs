using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAB5_RodrigoApaza.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MatriculasController : ControllerBase
{
    private readonly IMatriculaService _matriculaService;

    public MatriculasController(IMatriculaService matriculaService)
    {
        _matriculaService = matriculaService;
    }

    [HttpGet]
    [Authorize(Policy = "ProfesorOnly")]
    public async Task<IActionResult> GetAll()
    {
        var lista = await _matriculaService.GetAllMatriculasAsync();
        return Ok(lista);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "ProfesorOnly")]
    public async Task<IActionResult> GetById(int id)
    {
        var matricula = await _matriculaService.GetMatriculaByIdAsync(id);
        if (matricula == null) return NotFound();
        return Ok(matricula);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Create(CrearMatriculaDto dto)
    {
        var creada = await _matriculaService.CrearMatriculaAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = creada.IdMatricula }, creada);
    }
    
    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Update(int id, CrearMatriculaDto dto)
    {
        var result = await _matriculaService.UpdateMatriculaAsync(id, dto);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }


    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _matriculaService.DeleteMatriculaAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}