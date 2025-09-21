using LAB5_RodrigoApaza.DTOs;

namespace LAB5_RodrigoApaza.Services.Interfaces;

public interface IMateriaService
{
    Task<IEnumerable<MateriaDto>> GetAllMateriasAsync();
    Task<MateriaDto?> GetMateriaByIdAsync(int id);
    Task<MateriaDto> CreateMateriaAsync(CrearMateriaDto dto);
    Task<bool> UpdateMateriaAsync(int id, CrearMateriaDto dto);
    Task<bool> DeleteMateriaAsync(int id);
}