using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LAB5_RodrigoApaza.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MateriasController : ControllerBase
{
    private readonly IMateriaService _materiaService;

    public MateriasController(IMateriaService materiaService)
    {
        _materiaService = materiaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var materias = await _materiaService.GetAllMateriasAsync();
        return Ok(materias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var materia = await _materiaService.GetMateriaByIdAsync(id);
        if (materia == null) return NotFound();
        return Ok(materia);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CrearMateriaDto dto)
    {
        var creada = await _materiaService.CreateMateriaAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = creada.IdMateria }, creada);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CrearMateriaDto dto)
    {
        var result = await _materiaService.UpdateMateriaAsync(id, dto);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _materiaService.DeleteMateriaAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}