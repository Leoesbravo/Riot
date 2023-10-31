using DL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;
using System.Linq;

namespace Presentation.Controllers
{
    public class BracketController : Controller
    {
        private DL.RiotContext _context; // Reemplaza con tu DbContext

        public BracketController(DL.RiotContext context)
        {
            _context = context;
        }
        public ActionResult Rondas()
        {
            var enfrentamientosPrevios = _context.Enfrentamientos.ToList();

            // Luego generas la nueva ronda
            Models.Enfrentamiento newenfrentamiento = GenerarRonda(enfrentamientosPrevios);

            ViewBag.Enfrentamientos = newenfrentamiento.Enfrentamientos;
            ViewBag.descanso = newenfrentamiento.JuadorDescansa;
            return View(newenfrentamiento);
        }
        public Models.Enfrentamiento GenerarRonda(List<DL.Enfrentamiento>? enfrentamientosPrevios)
        {
            // Obtén la lista de todos los usuarios registrados
            var usuarios = _context.Usuarios.ToList();

            // Verifica si el número de usuarios es par o impar
            var numeroUsuarios = usuarios.Count;
            var esImpar = numeroUsuarios % 2 != 0;

            // Elige un jugador para descansar si es impar
            var jugadorDescanso = new DL.Usuario(); // Inicializa con un usuario nulo
            if (esImpar)
            {
                // Si es impar, asigna un jugador para descansar
                int jugadorDescansoIndex = new Random().Next(usuarios.Count);
                jugadorDescanso = usuarios[jugadorDescansoIndex];
                usuarios.RemoveAt(jugadorDescansoIndex);
            }

            // Baraja la lista de usuarios para generar enfrentamientos aleatorios
            usuarios = usuarios.OrderBy(_ => Guid.NewGuid()).ToList();

            // Crea una lista de enfrentamientos con nombres de usuarios
            Models.Enfrentamiento enfrentamiento = new Models.Enfrentamiento();
            enfrentamiento.Enfrentamientos = new List<Models.Enfrentamiento>();

            for (int i = 0; i < usuarios.Count; i += 2)
            {
                enfrentamiento.Enfrentamientos.Add(new Models.Enfrentamiento
                {
                    NombreUsuario1 = usuarios[i].UserName,
                    NombreUsuario2 = usuarios[i + 1].UserName,
                    IdUsuario1 = usuarios[i].IdUsuario,
                    IdUsuario2 = usuarios[i + 1].IdUsuario
                });
            }

            // Guarda la información temporalmente para mostrar al administrador
            //TempData["Enfrentamientos"] = enfrentamiento.Enfrentamientos;
            //TempData["IdJugadorDescanso"] = jugadorDescanso.IdUsuario;

            enfrentamiento.JuadorDescansa = jugadorDescanso.UserName;

            return enfrentamiento;
        }
        [HttpPost]
        public IActionResult ConfirmarRonda(Models.Enfrentamiento enfrentamiento)
        {

            using (var db = new DL.RiotContext()) // Reemplaza 'TuContextoDeBaseDeDatos' con el nombre de tu propio contexto de base de datos
            {
                // Itera sobre los enfrentamientos y llama al procedimiento almacenado para cada uno
                foreach (Models.Enfrentamiento item in enfrentamiento.Enfrentamientos)
                {
                    // Llama al procedimiento almacenado
                    db.Database.ExecuteSqlRaw("CrearPartida {0}, {1}, {2}",
                        enfrentamiento.Ronda, item.IdUsuario1, item.IdUsuario2);
                }
            }

            return RedirectToAction("GenerarRonda"); 
        }

        private bool EnfrentamientoDuplicado(Models.Enfrentamiento enfrentamiento, IQueryable<Partidum> partidas)
        {
            // Obtén los IDs de los participantes involucrados en el enfrentamiento
            int idUsuario1 = enfrentamiento.IdUsuario1;
            int idUsuario2 = enfrentamiento.IdUsuario2;

            // Verifica si existe una partida donde ambos participantes se enfrentaron
            return partidas.Any(p =>
                p.DetallesPartida.Any(dp =>
                    (dp.IdParticipanteUno == idUsuario1 && dp.IdParticipanteDos == idUsuario2) ||
                    (dp.IdParticipanteUno == idUsuario2 && dp.IdParticipanteDos == idUsuario1)
                )
            );
        }


    }
}

