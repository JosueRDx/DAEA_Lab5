using System.ComponentModel.DataAnnotations;

namespace LAB5_RodrigoApaza.DTOs;

public class CrearMatriculaDto
{
    [Required]
    public int IdEstudiante { get; set; }

    [Required]
    public int IdCurso { get; set; }

    [Required]
    [StringLength(20)]
    public string Semestre { get; set; }
}