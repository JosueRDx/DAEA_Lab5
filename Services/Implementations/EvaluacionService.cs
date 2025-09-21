using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Infrastructure.Models;
using LAB5_RodrigoApaza.Services.Interfaces;

namespace LAB5_RodrigoApaza.Services.Implementations;

public class EvaluacionService : IEvaluacionService
{
    private readonly IUnitOfWork _unitOfWork;

    public EvaluacionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<EvaluacionDto>> GetAllAsync()
    {
        var evaluaciones = await _unitOfWork.Repository<Evaluacione>().GetAllAsync();
        return evaluaciones.Select(e => new EvaluacionDto
        {
            IdEvaluacion = e.IdEvaluacion,
            Calificacion = e.Calificacion,
            Fecha = e.Fecha,
            IdCurso = e.IdCurso,
            IdEstudiante = e.IdEstudiante
        }).ToList();
    }

    public async Task<EvaluacionDto?> GetByIdAsync(int id)
    {
        var e = await _unitOfWork.Repository<Evaluacione>().GetByIdAsync(id);
        if (e == null) return null;

        return new EvaluacionDto
        {
            IdEvaluacion = e.IdEvaluacion,
            Calificacion = e.Calificacion,
            Fecha = e.Fecha,
            IdCurso = e.IdCurso,
            IdEstudiante = e.IdEstudiante
        };
    }

    public async Task<EvaluacionDto> CreateAsync(CrearEvaluacionDto dto)
    {
        var evaluacion = new Evaluacione
        {
            Calificacion = dto.Calificacion,
            Fecha = dto.Fecha,
            IdCurso = dto.IdCurso,
            IdEstudiante = dto.IdEstudiante
        };

        await _unitOfWork.Repository<Evaluacione>().AddAsync(evaluacion);
        await _unitOfWork.Complete();

        return new EvaluacionDto
        {
            IdEvaluacion = evaluacion.IdEvaluacion,
            Calificacion = evaluacion.Calificacion,
            Fecha = evaluacion.Fecha,
            IdCurso = evaluacion.IdCurso,
            IdEstudiante = evaluacion.IdEstudiante
        };
    }

    public async Task<bool> UpdateAsync(int id, CrearEvaluacionDto dto)
    {
        var evaluacion = await _unitOfWork.Repository<Evaluacione>().GetByIdAsync(id);
        if (evaluacion == null) return false;

        evaluacion.Calificacion = dto.Calificacion;
        evaluacion.Fecha = dto.Fecha;
        evaluacion.IdCurso = dto.IdCurso;
        evaluacion.IdEstudiante = dto.IdEstudiante;

        _unitOfWork.Repository<Evaluacione>().Update(evaluacion);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var evaluacion = await _unitOfWork.Repository<Evaluacione>().GetByIdAsync(id);
        if (evaluacion == null) return false;

        _unitOfWork.Repository<Evaluacione>().Delete(evaluacion);
        await _unitOfWork.Complete();
        return true;
    }
}
