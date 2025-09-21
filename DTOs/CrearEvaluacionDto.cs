using System.ComponentModel.DataAnnotations;

namespace LAB5_RodrigoApaza.DTOs;

public class CrearEvaluacionDto
{
    [Required]
    [Range(0.0, 100.0)]
    public decimal Calificacion { get; set; }

    [Required]
    public DateOnly? Fecha { get; set; }

    [Required]
    public int IdCurso { get; set; }

    [Required]
    public int IdEstudiante { get; set; }
}