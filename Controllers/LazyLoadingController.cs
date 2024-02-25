using IplPlayersClubDetailsApp.Models;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IplPlayersClubDetailsApp.Controllers
{
    public class LazyLoadingController : Controller
    {
        /// <summary>
        /// Author : Kranthi Pedamajji
        /// Description :  playerExists method is used to check whether player existed or not
        /// Date Modified : 23rd Feb 2022
        /// </summary>
        public IActionResult Index()
        {
            try
            {
                IplManagementContext context = new IplManagementContext();
                var clubs = context.Clubs.ToList();
                List<PlayerDetails> playerDetails = new List<PlayerDetails>();
                if (clubs.Count > 0)
                {
                    foreach (var club in clubs)
                    {
                        var players = club.Players.ToList();
                        foreach (var player in players)
                        {
                            PlayerDetails playerDetail = new PlayerDetails() { Club = club, Player = player };
                            playerDetails.Add(playerDetail);
                        }
                    }
                    return View(playerDetails);
                }
                return View();
            }
            catch (Exception exception)
            {
                ViewBag.title = $"An Unkown error has occured{exception.Message}";
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }
    }
}
