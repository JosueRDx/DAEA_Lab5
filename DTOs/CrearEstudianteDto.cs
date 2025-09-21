using System.ComponentModel.DataAnnotations;

namespace LAB5_RodrigoApaza.DTOs;

public class CreateEstudianteDto
{
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required]
    [Range(1, 100)]
    public int Edad { get; set; }

    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
}