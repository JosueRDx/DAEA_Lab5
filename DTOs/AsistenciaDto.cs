namespace LAB5_RodrigoApaza.DTOs;

public class AsistenciaDto
{
    public int IdAsistencia { get; set; }
    public DateOnly? Fecha { get; set; }
    public string? Estado { get; set; }
    public int? IdEstudiante { get; set; }
    public int? IdCurso { get; set; }
}