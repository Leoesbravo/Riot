namespace Presentation.Models
{
    public class DetallesPartida
    {
        // Propiedad que representa la columna IdDetallesPartida
        public int IdDetallesPartida { get; set; }

        // Propiedad que representa la columna IdPartida
        public int IdPartida { get; set; }

        // Propiedad que representa la columna IdParticipanteUno
        public int IdParticipanteUno { get; set; }

        // Propiedad que representa la columna IdParticipanteDos
        public int IdParticipanteDos { get; set; }

        // Propiedad de navegación que representa la relación muchos a uno con la tabla Partida
        public virtual Partida Partida { get; set; }

        // Propiedad de navegación que representa la relación muchos a uno con la tabla Participante para el primer participante
        public virtual Participante ParticipanteUno { get; set; }

        // Propiedad de navegación que representa la relación muchos a uno con la tabla Participante para el segundo participante
        public virtual Participante ParticipanteDos { get; set; }
    }
}
