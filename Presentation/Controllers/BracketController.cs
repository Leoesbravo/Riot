using DL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Presentation.Models;

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
            return View();
        }
        public ActionResult GenerarRonda()
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
            Models.Enfrentamiento enfrentamiento = new Enfrentamiento();
            enfrentamiento.Enfrentamientos = new List<object>();

            for (int i = 0; i < usuarios.Count; i += 2)
            {
                enfrentamiento.Enfrentamientos.Add(new Enfrentamiento
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
            ViewBag.descanso = jugadorDescanso.UserName;
            enfrentamiento.JuadorDescansa = jugadorDescanso.UserName;

            return View(enfrentamiento);
        }


        //Acción para confirmar la ronda
        [HttpPost]
        public IActionResult ConfirmarRonda()
        {
            // Recupera la información temporal de los enfrentamientos
            var enfrentamientos = TempData["Enfrentamientos"] as List<Enfrentamiento>;
            var idJugadorDescanso = TempData["IdJugadorDescanso"] as int?;

            // Guarda la información en la base de datos (Partida, DetallesPartida, etc.)
            // Implementa la lógica de guardado aquí

            return RedirectToAction("Index"); // Redirige a la página principal del torneo
        }

        private bool EnfrentamientoDuplicado(Enfrentamiento enfrentamiento, IQueryable<Partidum> partidas)
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

