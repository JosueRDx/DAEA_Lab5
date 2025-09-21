using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LAB5_RodrigoApaza.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EvaluacionesController : ControllerBase
{
    private readonly IEvaluacionService _service;

    public EvaluacionesController(IEvaluacionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CrearEvaluacionDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.IdEvaluacion }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CrearEvaluacionDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}