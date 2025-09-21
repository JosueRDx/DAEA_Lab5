using LAB5_RodrigoApaza.DTOs;

namespace LAB5_RodrigoApaza.Services.Interfaces;

public interface IProfesorService
{
    Task<IEnumerable<ProfesorDto>> GetAllProfesoresAsync();
    Task<ProfesorDto?> GetProfesorByIdAsync(int id);
    Task<ProfesorDto> CreateProfesorAsync(CrearProfesorDto dto);
    Task<bool> UpdateProfesorAsync(int id, CrearProfesorDto dto);
    Task<bool> DeleteProfesorAsync(int id);
}