using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Infrastructure.Models;
using LAB5_RodrigoApaza.Services.Interfaces;


namespace LAB5_RodrigoApaza.Services.Implementations;

public class MatriculaService : IMatriculaService
{
    private readonly IUnitOfWork _unitOfWork;

    public MatriculaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MatriculaDto>> GetAllMatriculasAsync()
    {
        var matriculas = await _unitOfWork.Repository<Matricula>().GetAllAsync();
        var estudiantes = await _unitOfWork.Repository<Estudiante>().GetAllAsync();
        var cursos = await _unitOfWork.Repository<Curso>().GetAllAsync();

        return matriculas.Select(m => new MatriculaDto
        {
            IdMatricula = m.IdMatricula,
            IdEstudiante = m.IdEstudiante,
            NombreEstudiante = estudiantes.FirstOrDefault(e => e.IdEstudiante == m.IdEstudiante)?.Nombre,
            IdCurso = m.IdCurso,
            NombreCurso = cursos.FirstOrDefault(c => c.IdCurso == m.IdCurso)?.Nombre,
            Semestre = m.Semestre
        }).ToList();
    }

    public async Task<MatriculaDto?> GetMatriculaByIdAsync(int id)
    {
        var matricula = await _unitOfWork.Repository<Matricula>().GetByIdAsync(id);
        if (matricula == null) return null;

        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(matricula.IdEstudiante);
        var curso = await _unitOfWork.Repository<Curso>().GetByIdAsync(matricula.IdCurso);

        return new MatriculaDto
        {
            IdMatricula = matricula.IdMatricula,
            IdEstudiante = matricula.IdEstudiante,
            NombreEstudiante = estudiante?.Nombre,
            IdCurso = matricula.IdCurso,
            NombreCurso = curso?.Nombre,
            Semestre = matricula.Semestre
        };
    }

    public async Task<MatriculaDto> CrearMatriculaAsync(CrearMatriculaDto dto)
    {
        var matricula = new Matricula
        {
            IdEstudiante = dto.IdEstudiante,
            IdCurso = dto.IdCurso,
            Semestre = dto.Semestre
        };

        await _unitOfWork.Repository<Matricula>().AddAsync(matricula);
        await _unitOfWork.Complete();

        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(dto.IdEstudiante);
        var curso = await _unitOfWork.Repository<Curso>().GetByIdAsync(dto.IdCurso);

        return new MatriculaDto
        {
            IdMatricula = matricula.IdMatricula,
            IdEstudiante = dto.IdEstudiante,
            NombreEstudiante = estudiante?.Nombre,
            IdCurso = dto.IdCurso,
            NombreCurso = curso?.Nombre,
            Semestre = dto.Semestre
        };
    }
    
    public async Task<bool> UpdateMatriculaAsync(int id, CrearMatriculaDto dto)
    {
        var matricula = await _unitOfWork.Repository<Matricula>().GetByIdAsync(id);
        if (matricula == null) return false;

        matricula.IdEstudiante = dto.IdEstudiante;
        matricula.IdCurso = dto.IdCurso;
        matricula.Semestre = dto.Semestre;

        _unitOfWork.Repository<Matricula>().Update(matricula);
        await _unitOfWork.Complete();
        return true;
    }


    public async Task<bool> DeleteMatriculaAsync(int id)
    {
        var matricula = await _unitOfWork.Repository<Matricula>().GetByIdAsync(id);
        if (matricula == null) return false;

        _unitOfWork.Repository<Matricula>().Delete(matricula);
        await _unitOfWork.Complete();
        return true;
    }
}
