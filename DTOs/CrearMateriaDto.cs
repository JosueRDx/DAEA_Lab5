using System.ComponentModel.DataAnnotations;

namespace LAB5_RodrigoApaza.DTOs;

public class CrearMateriaDto
{
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    public string? Descripcion { get; set; }

    [Required]
    public int IdCurso { get; set; }
}