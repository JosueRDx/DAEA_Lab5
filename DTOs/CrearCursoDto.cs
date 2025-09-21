using System.ComponentModel.DataAnnotations;

namespace LAB5_RodrigoApaza.DTOs;

public class CrearCursoDto
{
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    public string? Descripcion { get; set; }

    [Required]
    [Range(1, 10)]
    public int Creditos { get; set; }
}