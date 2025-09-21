using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Infrastructure.Models;

namespace LAB5_RodrigoApaza.Services.Interfaces;

public interface IEstudianteService
{
    Task<IEnumerable<EstudianteDto>> GetAllEstudiantesAsync();
    Task<EstudianteDto?> GetEstudianteByIdAsync(int id);
    Task<EstudianteDto> CreateEstudianteAsync(CreateEstudianteDto createEstudianteDto);
    Task<bool> UpdateEstudianteAsync(int id, CreateEstudianteDto updateEstudianteDto);
    Task<bool> DeleteEstudianteAsync(int id);
}