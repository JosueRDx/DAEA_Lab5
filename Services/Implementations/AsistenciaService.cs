using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Infrastructure.Models;
using LAB5_RodrigoApaza.Services.Interfaces;

namespace LAB5_RodrigoApaza.Services.Implementations;

public class AsistenciaService : IAsistenciaService
{
    private readonly IUnitOfWork _unitOfWork;

    public AsistenciaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<AsistenciaDto>> GetAllAsync()
    {
        var asistencias = await _unitOfWork.Repository<Asistencia>().GetAllAsync();
        return asistencias.Select(a => new AsistenciaDto
        {
            IdAsistencia = a.IdAsistencia,
            Fecha = a.Fecha,
            Estado = a.Estado,
            IdCurso = a.IdCurso,
            IdEstudiante = a.IdEstudiante
        });
    }

    public async Task<AsistenciaDto?> GetByIdAsync(int id)
    {
        var asistencia = await _unitOfWork.Repository<Asistencia>().GetByIdAsync(id);
        if (asistencia == null) return null;

        return new AsistenciaDto
        {
            IdAsistencia = asistencia.IdAsistencia,
            Fecha = asistencia.Fecha,
            Estado = asistencia.Estado,
            IdCurso = asistencia.IdCurso,
            IdEstudiante = asistencia.IdEstudiante
        };
    }

    public async Task<AsistenciaDto> CreateAsync(CreateAsistenciaDto dto)
    {
        var asistencia = new Asistencia
        {
            Fecha = dto.Fecha,
            Estado = dto.Estado,
            IdCurso = dto.IdCurso,
            IdEstudiante = dto.IdEstudiante
        };

        await _unitOfWork.Repository<Asistencia>().AddAsync(asistencia);
        await _unitOfWork.Complete();

        return new AsistenciaDto
        {
            IdAsistencia = asistencia.IdAsistencia,
            Fecha = asistencia.Fecha,
            Estado = asistencia.Estado,
            IdCurso = asistencia.IdCurso,
            IdEstudiante = asistencia.IdEstudiante
        };
    }

    public async Task<bool> UpdateAsync(int id, CreateAsistenciaDto dto)
    {
        var asistencia = await _unitOfWork.Repository<Asistencia>().GetByIdAsync(id);
        if (asistencia == null) return false;

        asistencia.Fecha = dto.Fecha;
        asistencia.Estado = dto.Estado;
        asistencia.IdCurso = dto.IdCurso;
        asistencia.IdEstudiante = dto.IdEstudiante;

        _unitOfWork.Repository<Asistencia>().Update(asistencia);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var asistencia = await _unitOfWork.Repository<Asistencia>().GetByIdAsync(id);
        if (asistencia == null) return false;

        _unitOfWork.Repository<Asistencia>().Delete(asistencia);
        await _unitOfWork.Complete();
        return true;
    }
}
