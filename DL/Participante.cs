using System;
using System.Collections.Generic;

namespace DL;

public partial class Participante
{
    public int IdParticipante { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdPartida { get; set; }

    public string? Campeon { get; set; }

    public byte? Minions { get; set; }

    public string? Side { get; set; }

    public bool? Ganador { get; set; }

    public virtual ICollection<DetallesPartidum> DetallesPartidumIdParticipanteDosNavigations { get; set; } = new List<DetallesPartidum>();

    public virtual ICollection<DetallesPartidum> DetallesPartidumIdParticipanteUnoNavigations { get; set; } = new List<DetallesPartidum>();

    public virtual Partidum? IdPartidaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
