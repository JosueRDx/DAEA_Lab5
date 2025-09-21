using LAB5_RodrigoApaza.DTOs;
using LAB5_RodrigoApaza.Infrastructure.Models;
using LAB5_RodrigoApaza.Services.Interfaces;

public class EstudianteService : IEstudianteService
{
    private readonly IUnitOfWork _unitOfWork;

    public EstudianteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<EstudianteDto>> GetAllEstudiantesAsync()
    {
        var estudiantes = await _unitOfWork.Repository<Estudiante>().GetAllAsync();
        return estudiantes.Select(e => new EstudianteDto
        {
            IdEstudiante = e.IdEstudiante,
            Nombre = e.Nombre,
            Edad = e.Edad,
            Direccion = e.Direccion,
            Correo = e.Correo,
            Telefono = e.Telefono
        }).ToList();
    }

    public async Task<EstudianteDto?> GetEstudianteByIdAsync(int id)
    {
        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
        if (estudiante == null) return null;

        return new EstudianteDto
        {
            IdEstudiante = estudiante.IdEstudiante,
            Nombre = estudiante.Nombre,
            Edad = estudiante.Edad,
            Direccion = estudiante.Direccion,
            Correo = estudiante.Correo,
            Telefono = estudiante.Telefono
        };
    }

    public async Task<EstudianteDto> CreateEstudianteAsync(CreateEstudianteDto createEstudianteDto)
    {
        var estudiante = new Estudiante
        {
            Nombre = createEstudianteDto.Nombre,
            Edad = createEstudianteDto.Edad,
            Direccion = createEstudianteDto.Direccion,
            Correo = createEstudianteDto.Correo,
            Telefono = createEstudianteDto.Telefono
        };

        await _unitOfWork.Repository<Estudiante>().AddAsync(estudiante);
        await _unitOfWork.Complete(); 

        return new EstudianteDto { IdEstudiante = estudiante.IdEstudiante, Nombre = estudiante.Nombre, Edad = estudiante.Edad, Correo = estudiante.Correo, Direccion = estudiante.Direccion, Telefono = estudiante.Telefono};
    }

    public async Task<bool> UpdateEstudianteAsync(int id, CreateEstudianteDto updateEstudianteDto)
    {
        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
        if (estudiante == null) return false;

        estudiante.Nombre = updateEstudianteDto.Nombre;
        estudiante.Edad = updateEstudianteDto.Edad;
        estudiante.Direccion = updateEstudianteDto.Direccion;
        estudiante.Correo = updateEstudianteDto.Correo;
        estudiante.Telefono = updateEstudianteDto.Telefono;

        _unitOfWork.Repository<Estudiante>().Update(estudiante);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<bool> DeleteEstudianteAsync(int id)
    {
        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
        if (estudiante == null) return false;

        _unitOfWork.Repository<Estudiante>().Delete(estudiante);
        await _unitOfWork.Complete();
        return true;
    }
}