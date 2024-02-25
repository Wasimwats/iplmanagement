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

#endregion

namespace IplPlayersClubDetailsApp.Controllers
{
    #region PlayersMvcController
    /// <summary>
    /// Description :  Created PlayersMvcController class to do crud operations in an Mvc.
    /// Date Modified : 23rd Feb 2022
    /// </summary>
    public class PlayersMvcController : Controller
    {
        private readonly    IplManagementContext _context;
        private readonly ILogger<PlayersMvcController> _logger;

        #region Contructers
        public PlayersMvcController(IplManagementContext context, ILogger<PlayersMvcController> logger)
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
        // GET: PlayersMvc
        public async Task<IActionResult> Index()
        {
            try
            {
                var playerManagementContext = _context.Players.Include(p => p.Club);
                return View("Index", await playerManagementContext.ToListAsync());
            }
            catch (SqlException se)
            {
                ViewBag.title = "An unknown error Ocuured in the database";
                _logger.LogInformation("Sorry!,Error Occured While displaying a records in PlayersMvcController..Exception is: {0}", se.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "An Unkown error has occured";
                _logger.LogInformation("Sorry!,An error was occured while processing your request in PlayersMvcController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }
        #endregion

        #region DetailsMethod
        /// <summary>
        /// Description :  Details method is used to display one records in players in mvc 
        /// Date Modified :23rd Feb 2022
        /// </summary>
        // GET: PlayersMvc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new NullReferenceException();
                }
                var player = await _context.Players
                    .Include(p => p.Club)
                    .FirstOrDefaultAsync(m => m.PlayerId == id);
                if (player == null)
                {
                    throw new NullReferenceException();
                }
                return View("Details", player);
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured in the database";
                _logger.LogInformation("Sorry!,Error Occured While displaying record in PlayersMvcController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = $"Player Id: {id} you are trying to search is not available ";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in PlayersMvcController..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  Unknown error was occured while processing your request";
                _logger.LogInformation("Sorry!,An  error was occured while processing your request in PlayersMvcController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }
        #endregion

        #region CreateMethod
        /// <summary>
        /// Description :  Create method is used to create  record in player record
        /// Date Modified :23rd Feb 2022
        /// </summary>
        // GET: PlayersMvc/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId");
                return View("Create");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your reques ";
                _logger.LogInformation("Sorry!,An  error was occured while processing your reques in PlayersMvcController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }
        // POST: PlayersMvc/Create
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
                    if (ModelState.IsValid)
                    {
                        _context.Add(player);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", player.ClubId);
                    return View(player);
                }
                ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", player.ClubId);
                return View(player);
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured in the database ";
                _logger.LogInformation("Sorry!,Error Occured While creating a record in PlayersMvcController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = "You are refercing to null values...Please don't refer to null values";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in PlayersMvcController..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your request";
                _logger.LogInformation("Sorry!,An  error was occured while processing your reques in PlayersMvcController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }
        #endregion

        #region EditMethod
        /// <summary>
        /// Description :  Edit method is used  to Update records in players Db
        /// Date Modified :23rd Feb 2022
        /// </summary>
        // GET: PlayersMvc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new NullReferenceException();
                }

                var player = await _context.Players.FindAsync(id);
                if (player == null)
                {
                    throw new NullReferenceException();
                }
                ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", player.ClubId);
                return View("Edit", player);
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured in the database ";
                _logger.LogInformation("Sorry!,Error Occured While Updating a record in PlayersMvcController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = $"Player Id: {id} you are trying to edit is not available ";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in PlayersMvcController..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your request";
                _logger.LogInformation("Sorry!,An  error was occured while processing your reques in PlayersMvcController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }

        // POST: PlayersMvc/Edit/5
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
                        _context.Update(player);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PlayerExists(player.PlayerId))
                        {
                            throw new HttpRequestException();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", player.ClubId);
                return View(player);

            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured inthe database maybe You are";
                _logger.LogInformation("Sorry!,Error Occured While Editing a record in PlayersMvcController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");

            }
            catch (Exception ex)
            {

                ViewBag.title = "Sorry!,An  error was occured while processing your request";
                _logger.LogInformation("Sorry!,An  error was occured while processing your reques in PlayersMvcController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }
        #endregion

        #region DeleteMethod
        /// <summary>
        /// Description :  Delete method is used to delete records in players Db 
        /// Date Modified : 23rd Feb 2022
        /// </summary>
        // GET: PlayersMvc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception();
                }
                var player = await _context.Players
                    .Include(p => p.Club)
                    .FirstOrDefaultAsync(m => m.PlayerId == id);
                if (player == null)
                {
                    throw new NullReferenceException();
                }
                return View("Delete", player);
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured inthe database";
                _logger.LogInformation("Sorry!,Error Occured While deleting a record in PlayersMvcController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = $"Player Id: {id} you are trying to delete is not available ";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in PlayersMvcController..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your request ";
                _logger.LogInformation("Sorry!,An  error was occured while processing your request in PlayersMvcController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }
        // POST: PlayersMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var player = await _context.Players.FindAsync(id);
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured inthe database";
                _logger.LogInformation("Sorry!,Error Occured While deleting a record in PlayersMvcController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your request ";
                _logger.LogInformation("Sorry!,An  error was occured while processing your request in PlayersMvcController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
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
