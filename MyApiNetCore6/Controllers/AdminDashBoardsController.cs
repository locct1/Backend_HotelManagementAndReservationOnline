using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Helpers;
using MyApiNetCore6.Models;
using MyApiNetCore6.Repositories.Admin;
using MyApiNetCore6.Repositories.Host;
using System.Data;

namespace MyApiNetCore6.Controllers
{
    [Route("api/admin-dashboards")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class AdminDashBoardsController : ControllerBase
    {
        private readonly IAdminDashBoardRepository _repo;
        private readonly IGetValueToken _getValueToken;

        public AdminDashBoardsController(IAdminDashBoardRepository repo, IGetValueToken getValueToken)
        {
            _repo = repo;
            _getValueToken = getValueToken;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                Response response = await _repo.GetAllAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = "Internal server error",
                });
                throw;
            }
        }
    }
}
