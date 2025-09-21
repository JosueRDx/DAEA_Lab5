namespace LAB5_RodrigoApaza.DTOs;

public class MatriculaDto
{
    public int IdMatricula { get; set; }

    public int IdEstudiante { get; set; }
    public string? NombreEstudiante { get; set; }

    public int IdCurso { get; set; }
    public string? NombreCurso { get; set; }

    public string Semestre { get; set; }
}