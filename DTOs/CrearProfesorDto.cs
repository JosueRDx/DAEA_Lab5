using System.ComponentModel.DataAnnotations;

namespace LAB5_RodrigoApaza.DTOs;

public class CrearProfesorDto
{
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [StringLength(100)]
    public string? Especialidad { get; set; }

    [EmailAddress]
    public string? Correo { get; set; }
}