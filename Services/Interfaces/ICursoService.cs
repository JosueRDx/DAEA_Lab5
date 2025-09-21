using LAB5_RodrigoApaza.DTOs;

namespace LAB5_RodrigoApaza.Services.Interfaces;

public interface ICursoService
{
    Task<IEnumerable<CursoDto>> GetAllCursosAsync();
    Task<CursoDto?> GetCursoByIdAsync(int id);
    Task<CursoDto> CreateCursoAsync(CrearCursoDto dto);
    Task<bool> UpdateCursoAsync(int id, CrearCursoDto dto);
    Task<bool> DeleteCursoAsync(int id);
}