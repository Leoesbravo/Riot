using System;
using System.Collections.Generic;

namespace DL;

public partial class DetallesPartidum
{
    public int IdDetallesPartida { get; set; }

    public int? IdPartida { get; set; }

    public int? IdParticipanteUno { get; set; }

    public int? IdParticipanteDos { get; set; }

    public virtual Participante? IdParticipanteDosNavigation { get; set; }

    public virtual Participante? IdParticipanteUnoNavigation { get; set; }

    public virtual Partidum? IdPartidaNavigation { get; set; }
}
