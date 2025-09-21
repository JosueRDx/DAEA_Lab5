using LAB5_RodrigoApaza.DTOs;

namespace LAB5_RodrigoApaza.Services.Interfaces;

public interface IMatriculaService
{
    Task<IEnumerable<MatriculaDto>> GetAllMatriculasAsync();
    Task<MatriculaDto?> GetMatriculaByIdAsync(int id);
    Task<MatriculaDto> CrearMatriculaAsync(CrearMatriculaDto dto);
    Task<bool> UpdateMatriculaAsync(int id, CrearMatriculaDto dto);
    Task<bool> DeleteMatriculaAsync(int id);
}