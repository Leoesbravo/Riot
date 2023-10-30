namespace Presentation.Models
{
    public class Partida
    {
        // Propiedad que representa la columna IdPartida
        public int IdPartida { get; set; }

        // Propiedad que representa la columna Duracion
        public TimeSpan Duracion { get; set; }

        // Propiedad que representa la columna Ronda
        public byte Ronda { get; set; }

        // Propiedad de navegación que representa la relación uno a muchos con la tabla DetallesPartida
        public DetallesPartida DetallesPartidas { get; set; }
    }
}
