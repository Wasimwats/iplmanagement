#region Packages
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IplPlayersClubDetailsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

#endregion

namespace IplPlayersClubDetailsApp.Controllers
{

    #region PlayersMvcWebApiController

    /// <summary>
    /// Date Modified: 23rd Feb 2022
    /// Change Description: Created PlayersMvcWebApiController class to do crud operations in an Mvc
    /// </summary>
    public class PlayersMvcWebApiController : Controller
    {
        private readonly IplManagementContext _context;
        HttpClient client = new HttpClient();
        string url = "https://localhost:44370/api/PlayersWebApi/";


        private readonly ILogger<PlayersMvcWebApiController> _logger;

        #region Constructer
        public PlayersMvcWebApiController(IplManagementContext context, ILogger<PlayersMvcWebApiController> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        #region IndexMethod

        /// <summary>
        /// Description :  Index method is used to display all records in players Db
        /// Date Modified :23rd Feb 2022
        /// </summary>
        // GET: PlayersMvcWebApi
        public async Task<IActionResult> Index()
        {
            try
            {
                // _logger.LogInformation("Details are viewwnd in mvc web api controler");
                return View(JsonConvert.DeserializeObject<List<Player>>(await client.GetStringAsync(url)).ToList());
            }
            catch (SqlException se)
            {
                ViewBag.title = "An unknown error Ocuured inthe database";
                _logger.LogInformation("Sorry!,Error Occured While displaying a records in PlayersMvcWebApiController..Exception is: {0}", se.Message);
                return View("~/Views/Shared/NotFound.cshtml");

            }
            catch (HttpRequestException he)
            {
                ViewBag.title = "Please check the url you are refering";
                _logger.LogInformation("Sorry!,an Error Occured While processing your request in PlayersMvcWebApiController..Exception is: {0}", he.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }

            catch (Exception ex)
            {
                ViewBag.title = "An Unkown error has occured";
                _logger.LogInformation("Sorry!,An error was occured while processing your request in PlayersMvcWebApiController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
        }
        #endregion

        #region DetailsMethod
        /// <summary>
        /// Description :  Details method is used to display one records in players 
        /// Date Modified :23rd Feb 2022
        /// </summary>
        // GET: PlayersMvcWebApi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception();
                }

                var player = JsonConvert.DeserializeObject<Player>(await client.GetStringAsync(url + id));
                if (player == null)
                {
                    throw new Exception();
                }
                _logger.LogInformation("Details are viewed in mvc web api controler");
                return View(player);
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured in the database";
                _logger.LogInformation("Sorry!,Error Occured While displaying record in PlayersMvcWebApiController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (HttpRequestException he)
            {
                ViewBag.title = $"Player Id: {id} you are trying to search is not available ";
                _logger.LogInformation("Sorry!,Error Occured While requesting to displaying records It may occured due Wrong Url in PlayersMvcWebApiController..Exception is: {0}", he.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = "You are refercing to null values...Please don't refer to null values";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in PlayersMvcWebApiController..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  Unknown error was occured while processing your request";
                _logger.LogInformation("Sorry!,An  error was occured while processing your request in PlayersMvcWebApiController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
        }
        #endregion

        #region CreateMethod
        /// <summary>
        /// Description :  Create method is used to create  records in player 
        /// Date Modified :23rd Feb 2022
        /// </summary>
        // GET: PlayersMvcWebApi/Create
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (HttpRequestException he)
            {
                ViewBag.title = "Please check the url that you are refering";
                _logger.LogInformation("Sorry!,Error Occured While requesting to Creating records It may Occure in PlayersMvcWebApiController..Exception is: {0}", he.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your reques ";
                _logger.LogInformation("Sorry!,An  error was occured while processing your reques in PlayersMvcWebApiController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
        }

        // POST: PlayersMvcWebApi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerId,FirstName,LastName,ContactNo,DateOfJoining,ClubId")] Player player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await client.PostAsJsonAsync<Player>(url, player);

                    return RedirectToAction(nameof(Index));
                }
                ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", player.ClubId);
                return View(player);
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured in the database ";
                _logger.LogInformation("Sorry!,Error Occured While creating a record in PlayersMvcWebApiController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (HttpRequestException he)
            {
                ViewBag.title = "Please check the url you are refering";
                _logger.LogInformation("Sorry!,Error Occured While requesting to Creating records It may Occure in PlayersMvcWebApiController..Exception is: {0}", he.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = "You are refercing to null values...Please don't refer to null values";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in PlayersMvcWebApiController..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your request";
                _logger.LogInformation("Sorry!,An  error was occured while processing your reques in PlayersMvcWebApiController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }

        }
        #endregion

        #region EditMethod
        /// <summary>
        /// Description :  Edit method is used  to Update records in players Db
        /// Date Modified :23rd Feb 2022
        /// </summary>
        // GET: PlayersMvcWebApi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception();
                }
                var player = JsonConvert.DeserializeObject<Player>(await client.GetStringAsync(url + id));
                if (player == null)
                {
                    throw new HttpRequestException();
                }
                return View(player);
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured in the database ";
                _logger.LogInformation("Sorry!,Error Occured While Updating a record in PlayersMvcWebApiController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (HttpRequestException he)
            {
                ViewBag.title = $"Player Id: {id} you are trying to edit is not available ";
                _logger.LogInformation("Sorry!,Error Occured While requesting to Updating records in PlayersMvcWebApiController..Exception is: {0}", he.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = "You are refercing to null values...Please don't refer to null values";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in PlayersMvcWebApiController..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your request";
                _logger.LogInformation("Sorry!,An  error was occured while processing your reques in PlayersMvcWebApiController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }

        }

        // POST: PlayersMvcWebApi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerId,FirstName,LastName,ContactNo,DateOfJoining,ClubId")] Player player)
        {
            try
            {
                if (id != player.PlayerId)
                {
                    throw new Exception();
                }

                if (ModelState.IsValid)
                {
                    try
                    {

                        await client.PutAsJsonAsync<Player>(url + id, player);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PlayerExists(player.PlayerId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }

                return View(player);
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured inthe database maybe You are";
                _logger.LogInformation("Sorry!,Error Occured While Editing a record in PlayersMvcWebApiController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/NotFound.cshtml");

            }
            catch (HttpRequestException he)
            {
                ViewBag.title = "Please check the url you are refering";
                _logger.LogInformation("Sorry!,Error Occured While requesting to Editing record in PlayersMvcWebApiController..Exception is: {0}", he.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = "You are refercing to null values...Please don't refer to null values";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in PlayersMvcWebApiController..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (Exception ex)
            {

                ViewBag.title = "Sorry!,An  error was occured while processing your request";
                _logger.LogInformation("Sorry!,An  error was occured while processing your reques in PlayersMvcWebApiController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }

        }
        #endregion

        #region DeleteMethod
        /// <summary>
        /// Description :  Delete method is used to delete records in players Db 
        /// Date Modified : 23rd Feb 2022
        /// </summary>
        // GET: PlayersMvcWebApi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            try
            {
                if (id == null)
                {
                    _logger.LogInformation("There is no record related to this id");
                    throw new Exception();
                }
                var player = JsonConvert.DeserializeObject<Player>(await client.GetStringAsync(url + id));

                return View(player);
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured inthe database";
                _logger.LogInformation("Sorry!,Error Occured While deleting a record in PlayersMvcWebApiController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (HttpRequestException he)
            {
                ViewBag.title = $"Player Id: {id} you are trying to delete is not available ";
                _logger.LogInformation("Sorry!,Error Occured While requesting to Deleting records in PlayersMvcWebApiController..Exception is: {0}", he.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = "You are referncing to null values...Please don't refer to null values";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in PlayersMvcWebApiController..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your request ";
                _logger.LogInformation("Sorry!,An  error was occured while processing your request in PlayersMvcWebApiController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }

        }

        // POST: PlayersMvcWebApi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await client.DeleteAsync(url + id);
                return RedirectToAction(nameof(Index));
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured inthe database";
                _logger.LogInformation("Sorry!,Error Occured While deleting a record in PlayersMvcWebApiController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (HttpRequestException he)
            {
                ViewBag.title = $"Player Id: {id} you are trying to delete is not available ";
                _logger.LogInformation("Sorry!,Error Occured While requesting to Deleting records in PlayersMvcWebApiController..Exception is: {0}", he.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = "You are referncing to null values...Please don't refer to null values";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in PlayersMvcWebApiController..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your request ";
                _logger.LogInformation("Sorry!,An  error was occured while processing your request in PlayersMvcWebApiController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/NotFound.cshtml");
            }
        }
        #endregion

        #region PlayerExistsMethod
        /// <summary>
        /// Description :  playerExists method is used to check whether player existed or not
        /// Date Modified : 23rd Feb 2022
        /// </summary>
        private bool PlayerExists(int id)
        {
            try
            {
                return _context.Players.Any(e => e.PlayerId == id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Sorry!,An  error was occured while processing your request..Exception is: {0}", ex);
            }
            return false;
        }
    }
    #endregion

    #endregion
}
