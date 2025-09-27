using System;
using System.Collections.Generic;

namespace LAB5_RodrigoApaza.Infrastructure.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Rol { get; set; } = null!;
}
