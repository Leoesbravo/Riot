namespace Presentation.Models
{
    public class Enfrentamiento
    {
        public int IdUsuario1 { get; set; }
        public int IdUsuario2 { get; set; }
        public int Ronda { get; set; }
        public string NombreUsuario1 { get; set; }
        public string NombreUsuario2 { get; set; }
        public string JuadorDescansa { get; set; }
        public int IdJugadorDescansa { get; set; }
        public List<Enfrentamiento> Enfrentamientos { get; set; }
    }
}
