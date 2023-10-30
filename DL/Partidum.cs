using System;
using System.Collections.Generic;

namespace DL;

public partial class Partidum
{
    public int IdPartida { get; set; }

    public TimeSpan? Duracion { get; set; }

    public byte? Ronda { get; set; }

    public virtual ICollection<DetallesPartidum> DetallesPartida { get; set; } = new List<DetallesPartidum>();

    public virtual ICollection<Participante> Participantes { get; set; } = new List<Participante>();
}
