using LAB5_RodrigoApaza.DTOs;

namespace LAB5_RodrigoApaza.Services.Interfaces;

public interface IEvaluacionService
{
    Task<IEnumerable<EvaluacionDto>> GetAllAsync();
    Task<EvaluacionDto?> GetByIdAsync(int id);
    Task<EvaluacionDto> CreateAsync(CrearEvaluacionDto dto);
    Task<bool> UpdateAsync(int id, CrearEvaluacionDto dto);
    Task<bool> DeleteAsync(int id);
}