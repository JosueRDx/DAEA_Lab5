using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Infrastructure.Models;
using LAB5_RodrigoApaza.Services.Interfaces;

namespace LAB5_RodrigoApaza.Services.Implementations;

public class MateriaService : IMateriaService
{
    private readonly IUnitOfWork _unitOfWork;

    public MateriaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MateriaDto>> GetAllMateriasAsync()
    {
        var materias = await _unitOfWork.Repository<Materia>().GetAllAsync();
        return materias.Select(m => new MateriaDto
        {
            IdMateria = m.IdMateria,
            Nombre = m.Nombre,
            Descripcion = m.Descripcion,
            IdCurso = m.IdCurso
        }).ToList();
    }

    public async Task<MateriaDto?> GetMateriaByIdAsync(int id)
    {
        var materia = await _unitOfWork.Repository<Materia>().GetByIdAsync(id);
        if (materia == null) return null;

        return new MateriaDto
        {
            IdMateria = materia.IdMateria,
            Nombre = materia.Nombre,
            Descripcion = materia.Descripcion,
            IdCurso = materia.IdCurso
        };
    }

    public async Task<MateriaDto> CreateMateriaAsync(CrearMateriaDto dto)
    {
        var materia = new Materia
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            IdCurso = dto.IdCurso
        };

        await _unitOfWork.Repository<Materia>().AddAsync(materia);
        await _unitOfWork.Complete();

        return new MateriaDto
        {
            IdMateria = materia.IdMateria,
            Nombre = materia.Nombre,
            Descripcion = materia.Descripcion,
            IdCurso = materia.IdCurso
        };
    }

    public async Task<bool> UpdateMateriaAsync(int id, CrearMateriaDto dto)
    {
        var materia = await _unitOfWork.Repository<Materia>().GetByIdAsync(id);
        if (materia == null) return false;

        materia.Nombre = dto.Nombre;
        materia.Descripcion = dto.Descripcion;
        materia.IdCurso = dto.IdCurso;

        _unitOfWork.Repository<Materia>().Update(materia);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<bool> DeleteMateriaAsync(int id)
    {
        var materia = await _unitOfWork.Repository<Materia>().GetByIdAsync(id);
        if (materia == null) return false;

        _unitOfWork.Repository<Materia>().Delete(materia);
        await _unitOfWork.Complete();
        return true;
    }
}
