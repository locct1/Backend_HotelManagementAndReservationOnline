using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Helpers;
using MyApiNetCore6.Models;
using MyApiNetCore6.Repositories.Host;

namespace MyApiNetCore6.Controllers
{
    [Route("api/hotel-dashboards")]
    [ApiController]
    public class HotelDashBoardsController : ControllerBase
    {
        private readonly IHotelDashBoardRepository _repo;
        private readonly IGetValueToken _getValueToken;

        public HotelDashBoardsController(IHotelDashBoardRepository repo, IGetValueToken getValueToken)
        {
            _repo = repo;
            _getValueToken = getValueToken;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var hotelId = _getValueToken.GetClaimValue(HttpContext, "HotelId");
                Response response = await _repo.GetAllAsync(Int32.Parse(hotelId));
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
