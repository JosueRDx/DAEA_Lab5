using System.ComponentModel.DataAnnotations;

namespace LAB5_RodrigoApaza.DTOs;

public class CreateAsistenciaDto
{
    [Required]
    public DateOnly Fecha { get; set; }

    [Required]
    [StringLength(20)]
    public string Estado { get; set; }

    [Required]
    public int IdEstudiante { get; set; }

    [Required]
    public int IdCurso { get; set; }
}