namespace Presentation.Models
{
    public class Participante
    {
        // Propiedad que representa la columna IdParticipante
        public int IdParticipante { get; set; }

        // Propiedad que representa la columna IdUsuario
        public int IdUsuario { get; set; }

        // Propiedad que representa la columna Campeon
        public string Campeon { get; set; }

        // Propiedad que representa la columna Minions
        public byte Minions { get; set; }

        // Propiedad que representa la columna Side
        public string Side { get; set; }

        // Propiedad que representa la columna Ganador
        public bool Ganador { get; set; }

        // Propiedad de navegación que representa la relación muchos a uno con la tabla Usuario
        public virtual Usuario Usuario { get; set; }
    }
}