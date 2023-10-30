using Microsoft.AspNetCore.Mvc;
using RiotSharp;
using RiotSharp.Misc;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Detalles()
        {
            Account cuenta = new Account();
            var api = RiotApi.GetDevelopmentInstance("RGAPI-02bfb9ca-d6b8-450b-b9f5-9118154e2b6d");
            try
            {
                var summoner = api.Summoner.GetSummonerByNameAsync(Region.Lan, "heraldicleo").Result;
                cuenta.Nombre = summoner.Name;
                cuenta.Nivel = summoner.Level;
                cuenta.IdIcono = summoner.ProfileIconId;
            }
            catch (RiotSharpException ex)
            {
            }
            return View(cuenta);
        }
    }
}
