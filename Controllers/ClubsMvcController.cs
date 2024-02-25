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
    /// Description :  Created ClubsMvcController class to do crud operations in an Mvc.
    /// Date Modified : 23rd Feb 2022
    /// </summary>
    public class ClubsMvcController : Controller
    {
        private readonly IplManagementContext _context;
        private readonly ILogger<ClubsMvcController> _logger;
        #region constructers
        public ClubsMvcController(IplManagementContext context, ILogger<ClubsMvcController> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        #region IndexMethod
        /// <summary>
        /// Description :  Index method is used to display all records in Clubs Db
        /// Date Modified :23rd Feb 2022
        /// </summary>
        // GET: ClubsMvc
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.Clubs.ToListAsync());
            }
            catch (SqlException se)
            {
                ViewBag.title = "An unknown error Ocuured in the database";
                _logger.LogInformation("Sorry!,Error Occured While displaying a records in ClubsMvcController..Exception is: {0}", se.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");

            }
            catch (Exception ex)
            {
                ViewBag.title = "An Unkown error has occured";
                _logger.LogInformation("Sorry!,An error was occured while processing your request in ClubsMvcController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }
        #endregion

        #region DetailsMethod
        /// <summary>
        /// Description :  Details method is used to display one records in Clubs in mvc 
        /// Date Modified :23rd Feb 2022
        /// </summary>
        // GET: ClubsMvc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new NullReferenceException();
                }

                var club = await _context.Clubs
                    .FirstOrDefaultAsync(m => m.ClubId == id);
                if (club == null)
                {
                    throw new NullReferenceException();
                }
                return View(club);
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured in the database";
                _logger.LogInformation("Sorry!,Error Occured While displaying record in ClubsMvcController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = $"Club Id: {id} you are trying to search is not available ";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in ClubsMvcController..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  Unknown error was occured while processing your request";
                _logger.LogInformation("Sorry!,An  error was occured while processing your request in ClubsMvcController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }
        #endregion

        #region CreateMethod
        /// <summary>
        /// Description :  Create method is used to create  record in Clubs record
        /// Date Modified :23rd Feb 2022
        /// </summary>
        // GET: ClubsMvc/Create
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your reques ";
                _logger.LogInformation("Sorry!,An  error was occured while processing your reques in ClubsMvcController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }

        // POST: ClubsMvc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClubId,Name")] Club club)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(club);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(club);
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured in the database ";
                _logger.LogInformation("Sorry!,Error Occured While creating a record in ClubssMvcController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = "You are refercing to null values...Please don't refer to null values";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in ClubssMvcController..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your request";
                _logger.LogInformation("Sorry!,An  error was occured while processing your reques in ClubssMvcControllert..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }
        #endregion

        #region EditMethod
        /// <summary>
        /// Description :  Edit method is used  to Update records in Clubs Db
        /// Date Modified :23rd Feb 2022
        /// </summary>
        // GET: ClubsMvc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new NullReferenceException();
                }

                var club = await _context.Clubs.FindAsync(id);
                if (club == null)
                {
                    throw new NullReferenceException();
                }
                return View(club);
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured in the database ";
                _logger.LogInformation("Sorry!,Error Occured While Updating a record in ClubsMvcController ..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = $"Club Id: {id} you are trying to edit is not available ";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in ClubsMvcController ..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your request";
                _logger.LogInformation("Sorry!,An  error was occured while processing your reques in ClubsMvcController ..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }

        // POST: ClubsMvc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClubId,Name")] Club club)
        {
            try
            {
                if (id != club.ClubId)
                {
                    throw new Exception();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(club);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClubExists(club.ClubId))
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
                return View(club);
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured inthe database maybe You are";
                _logger.LogInformation("Sorry!,Error Occured While Editing a record in ClubsMvcController ..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");

            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = $"Club Id: {id} you are trying to edit is not available ";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in ClubsMvcController", ne.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (Exception ex)
            {

                ViewBag.title = "Sorry!,An  error was occured while processing your request";
                _logger.LogInformation("Sorry!,An  error was occured while processing your reques in ClubsMvcController", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }
        #endregion

        #region DeleteMethod
        /// <summary>
        /// Description :  Delete method is used to delete records in Clubs Db 
        /// Date Modified : 23rd Feb 2022
        /// </summary>
        // GET: ClubsMvc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new NullReferenceException();
                }

                var club = await _context.Clubs
                    .FirstOrDefaultAsync(m => m.ClubId == id);
                if (club == null)
                {
                    throw new NullReferenceException();
                }

                return View(club);
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured inthe database";
                _logger.LogInformation("Sorry!,Error Occured While deleting a record in ClubsMvcController ..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = $"Club Id: {id} you are trying to delete is not available ";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in ClubsMvcController..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your request ";
                _logger.LogInformation("Sorry!,An  error was occured while processing your request in ClubsMvcController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }

        // POST: ClubsMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var club = await _context.Clubs.FindAsync(id);
                _context.Clubs.Remove(club);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (SqlException sx)
            {
                ViewBag.title = "An unknown error Ocuured inthe database";
                _logger.LogInformation("Sorry!,Error Occured While deleting a record in ClubsMvcController..Exception is: {0}", sx.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (NullReferenceException ne)
            {
                ViewBag.title = $"Player Id: {id} your trying to delete is not available ";
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in ClubsMvcController..Exception is: {0}", ne.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.title = "Sorry!,An  error was occured while processing your request ";
                _logger.LogInformation("Sorry!,An  error was occured while processing your request in ClubsMvcController..Exception is: {0}", ex.Message);
                return View("~/Views/Shared/ErrorPlayerMvc.cshtml");
            }
        }
        #endregion

        #region ClubsExistsMethod
        /// <summary>
        /// Description :  playerExists method is used to check whether player existed or not
        /// Date Modified : 23rd Feb 2022
        /// </summary>
        private bool ClubExists(int id)
        {
            try
            {
                return _context.Clubs.Any(e => e.ClubId == id);
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

