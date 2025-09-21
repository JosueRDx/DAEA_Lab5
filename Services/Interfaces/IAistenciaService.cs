using LAB5_RodrigoApaza.DTOs;

namespace LAB5_RodrigoApaza.Services.Interfaces;

public interface IAsistenciaService
{
    Task<IEnumerable<AsistenciaDto>> GetAllAsync();
    Task<AsistenciaDto?> GetByIdAsync(int id);
    Task<AsistenciaDto> CreateAsync(CreateAsistenciaDto dto);
    Task<bool> UpdateAsync(int id, CreateAsistenciaDto dto);
    Task<bool> DeleteAsync(int id);
}