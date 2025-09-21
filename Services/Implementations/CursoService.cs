using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Infrastructure.Models;
using LAB5_RodrigoApaza.Services.Interfaces;

namespace LAB5_RodrigoApaza.Services.Implementations;

public class CursoService : ICursoService
{
    private readonly IUnitOfWork _unitOfWork;

    public CursoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CursoDto>> GetAllCursosAsync()
    {
        var cursos = await _unitOfWork.Repository<Curso>().GetAllAsync();
        return cursos.Select(c => new CursoDto
        {
            IdCurso = c.IdCurso,
            Nombre = c.Nombre,
            Descripcion = c.Descripcion,
            Creditos = c.Creditos
        }).ToList();
    }

    public async Task<CursoDto?> GetCursoByIdAsync(int id)
    {
        var curso = await _unitOfWork.Repository<Curso>().GetByIdAsync(id);
        if (curso == null) return null;

        return new CursoDto
        {
            IdCurso = curso.IdCurso,
            Nombre = curso.Nombre,
            Descripcion = curso.Descripcion,
            Creditos = curso.Creditos
        };
    }

    public async Task<CursoDto> CreateCursoAsync(CrearCursoDto dto)
    {
        var curso = new Curso
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Creditos = dto.Creditos
        };

        await _unitOfWork.Repository<Curso>().AddAsync(curso);
        await _unitOfWork.Complete();

        return new CursoDto
        {
            IdCurso = curso.IdCurso,
            Nombre = curso.Nombre,
            Descripcion = curso.Descripcion,
            Creditos = curso.Creditos
        };
    }

    public async Task<bool> UpdateCursoAsync(int id, CrearCursoDto dto)
    {
        var curso = await _unitOfWork.Repository<Curso>().GetByIdAsync(id);
        if (curso == null) return false;

        curso.Nombre = dto.Nombre;
        curso.Descripcion = dto.Descripcion;
        curso.Creditos = dto.Creditos;

        _unitOfWork.Repository<Curso>().Update(curso);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<bool> DeleteCursoAsync(int id)
    {
        var curso = await _unitOfWork.Repository<Curso>().GetByIdAsync(id);
        if (curso == null) return false;

        _unitOfWork.Repository<Curso>().Delete(curso);
        await _unitOfWork.Complete();
        return true;
    }
}
