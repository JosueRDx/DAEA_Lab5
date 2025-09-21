using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Infrastructure.Models;
using LAB5_RodrigoApaza.Services.Interfaces;

namespace LAB5_RodrigoApaza.Services.Implementations;

public class ProfesorService : IProfesorService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProfesorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProfesorDto>> GetAllProfesoresAsync()
    {
        var profesores = await _unitOfWork.Repository<Profesore>().GetAllAsync();
        return profesores.Select(p => new ProfesorDto
        {
            IdProfesor = p.IdProfesor,
            Nombre = p.Nombre,
            Especialidad = p.Especialidad,
            Correo = p.Correo
        }).ToList();
    }

    public async Task<ProfesorDto?> GetProfesorByIdAsync(int id)
    {
        var profesor = await _unitOfWork.Repository<Profesore>().GetByIdAsync(id);
        if (profesor == null) return null;

        return new ProfesorDto
        {
            IdProfesor = profesor.IdProfesor,
            Nombre = profesor.Nombre,
            Especialidad = profesor.Especialidad,
            Correo = profesor.Correo
        };
    }

    public async Task<ProfesorDto> CreateProfesorAsync(CrearProfesorDto dto)
    {
        var profesor = new Profesore
        {
            Nombre = dto.Nombre,
            Especialidad = dto.Especialidad,
            Correo = dto.Correo
        };

        await _unitOfWork.Repository<Profesore>().AddAsync(profesor);
        await _unitOfWork.Complete();

        return new ProfesorDto
        {
            IdProfesor = profesor.IdProfesor,
            Nombre = profesor.Nombre,
            Especialidad = profesor.Especialidad,
            Correo = profesor.Correo
        };
    }

    public async Task<bool> UpdateProfesorAsync(int id, CrearProfesorDto dto)
    {
        var profesor = await _unitOfWork.Repository<Profesore>().GetByIdAsync(id);
        if (profesor == null) return false;

        profesor.Nombre = dto.Nombre;
        profesor.Especialidad = dto.Especialidad;
        profesor.Correo = dto.Correo;

        _unitOfWork.Repository<Profesore>().Update(profesor);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<bool> DeleteProfesorAsync(int id)
    {
        var profesor = await _unitOfWork.Repository<Profesore>().GetByIdAsync(id);
        if (profesor == null) return false;

        _unitOfWork.Repository<Profesore>().Delete(profesor);
        await _unitOfWork.Complete();
        return true;
    }
}
