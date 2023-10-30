using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? UserName { get; set; }

    public virtual ICollection<Participante> Participantes { get; set; } = new List<Participante>();
}
