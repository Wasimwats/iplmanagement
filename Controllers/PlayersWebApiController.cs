using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IplPlayersClubDetailsApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace IplPlayersClubDetailsApp.Controllers
{
    #region PlayersWebApiController
    /// <summary>
    /// Author :  
    /// Description :  Created PlayerWebApiController class to do crud operations in an Mvc.
    /// Date Modified : 23rd Feb 2022
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class PlayersWebApiController : ControllerBase
    {
        private readonly IplManagementContext _context;
        private readonly ILogger<PlayersWebApiController> _logger;

        #region Constructer
        public PlayersWebApiController(IplManagementContext context, ILogger<PlayersWebApiController> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        #region GetPlayersMethod

        /// <summary>
        /// Description :  GetPlayers method is used to display all records in players Db
        /// Date Modified :23rd Feb 2022
        /// </summary>
        // GET: api/PlayersWebApi

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            try
            {
                return await _context.Players.ToListAsync();
            }
            catch (SqlException se)
            {
                _logger.LogInformation("Sorry!,Error Occured While displaying a records in PlayersWebApiController ..Exception is: {0}", se.Message);
            }
            catch (HttpRequestException he)
            {
                _logger.LogInformation("Sorry!,Error Occured While processing your request in PlayersWebApiController..Exception is: {0}", he.Message);
            }

            catch (Exception ex)
            {
                _logger.LogInformation("Sorry!,An  error was occured while processing your request in PlayersWebApiController..Exception is: {0}", ex.Message);
            }
            return NotFound();
        }
        #endregion

        #region GetOnePlayer
        /// <summary>
        /// Description :  GetPlayer method is used to display one records in players 
        /// Date Modified :23rd Feb 2022
        /// </summary>
        // GET: api/PlayersWebApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            try
            {
                var player = await _context.Players.FindAsync(id);

                if (player == null)
                {
                    throw new NullReferenceException();
                }

                return player;
            }
            catch (SqlException sx)
            {
                _logger.LogInformation("Sorry!,Error Occured While displaying record in PlayersWebApiController..Exception is: {0}", sx.Message);
            }
            catch (HttpRequestException he)
            {
                _logger.LogInformation("Sorry!,Error Occured While requesting to displaying records It may Occuree It may occured due Wrong Url in PlayersWebApiController..Exception is: {0}", he.Message);

            }
            catch (NullReferenceException ne)
            {
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering in PlayersWebApiController..Exception is: {0}", ne.Message);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Sorry!,An  error was occured while processing your reques in PlayersWebApiController..Exception is: {0}", ex.Message);
            }

            return NotFound();
        }
        #endregion

        #region PutPlayer

        /// <summary>
        /// Description :  PutPlayer method is used to create  records using postman
        /// Date Modified :23rd Feb 2022
        /// </summary>
        // PUT: api/PlayersWebApi/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            try
            {
                if (id != player.PlayerId)
                {
                    return BadRequest();
                }

                _context.Entry(player).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return NoContent();
            }
            catch (HttpRequestException hr)
            {
                _logger.LogInformation("Sorry!,Error Occured While requesting to displaying records It may Occure in PlayersWebApiController..Exception is: {0}", hr.Message);
            }
            catch (SqlException sx)
            {
                _logger.LogInformation("Sorry!,Error Occured While Creating record in PlayersMvcWebApiController..Exception is: {0}", sx.Message);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Sorry!,An  error was occured while processing your request in PlayersWebApiController..Exception is: {0}", ex.Message);
            }
            return NotFound();
        }
        #endregion

        #region PostPlayer

        /// <summary>
        /// Description :  PostPlayer method is used  to Update records in players Db
        /// Date Modified :23rd Feb 2022
        /// </summary>

        // POST: api/PlayersWebApi

        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {

            try
            {
                _context.Players.Add(player);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPlayer", new { id = player.PlayerId }, player);
            }

            catch (HttpRequestException he)
            {
                _logger.LogInformation("Sorry!,Error Occured While requesting to displaying records It may Occure due to incorrect url in PlayersMvcWebApiController..Exception is: {0}", he.Message);

            }
            catch (SqlException sx)
            {
                _logger.LogInformation("Sorry!,Error Occured While Creating record in PlayersMvcWebApiController..Exception is: {0}", sx.Message);

            }
            catch (NullReferenceException ne)
            {
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering..Exception is: {0}", ne.Message);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Sorry!,An  error was occured while processing your request..Exception is: {0}", ex.Message);
            }
            return NoContent();
        }
        #endregion

        #region DeletePlayer

        /// <summary>
        /// Description :  DeletePlayer method is used to delete records in players Db 
        /// Date Modified : 23rd Feb 2022
        /// </summary>
        // DELETE: api/PlayersWebApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> DeletePlayer(int id)
        {
            try
            {
                var player = await _context.Players.FindAsync(id);
                if (player == null)
                {
                    throw new NullReferenceException();
                }

                _context.Players.Remove(player);
                await _context.SaveChangesAsync();

                return player;
            }
            catch (SqlException sx)
            {
                _logger.LogInformation("Sorry!,Error Occured While Upateing record..Exception is: {0}", sx.Message);

            }
            catch (HttpRequestException he)
            {
                _logger.LogInformation("Sorry!,Error Occured While requesting to Updating records ..Exception is: {0}", he.Message);

            }
            catch (NullReferenceException ne)
            {
                _logger.LogInformation("Sorry!,refering to the Null values Please check the url your refering..Exception is: {0}", ne.Message);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Sorry!,An  error was occured while processing your request..Exception is: {0}", ex.Message);
            }
            return NoContent();
        }
        #endregion

        #region PlayerExists
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
                _logger.LogInformation("Sorry!,An  error was occured while processing your request..Exception is: {0}", ex.Message);
            }
            return false;
        }
        #endregion
    }
    #endregion
}
