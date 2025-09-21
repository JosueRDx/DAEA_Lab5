using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EstudiantesController : ControllerBase
{
    private readonly IEstudianteService _estudianteService;

    public EstudiantesController(IEstudianteService estudianteService)
    {
        _estudianteService = estudianteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var estudiantes = await _estudianteService.GetAllEstudiantesAsync();
        return Ok(estudiantes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var estudiante = await _estudianteService.GetEstudianteByIdAsync(id);
        if (estudiante == null)
        {
            return NotFound();
        }
        return Ok(estudiante);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEstudianteDto createEstudianteDto)
    {
        var estudiante = await _estudianteService.CreateEstudianteAsync(createEstudianteDto);
        return CreatedAtAction(nameof(GetById), new { id = estudiante.IdEstudiante }, estudiante);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateEstudianteDto updateEstudianteDto)
    {
        var result = await _estudianteService.UpdateEstudianteAsync(id, updateEstudianteDto);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _estudianteService.DeleteEstudianteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}