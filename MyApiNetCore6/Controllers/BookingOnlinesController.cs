using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Helpers;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Status;
using MyApiNetCore6.Repositories.Host;

namespace MyApiNetCore6.Controllers
{
    [Route("api/host-manage-booking-online")]
    [ApiController]
    [Authorize(Roles = "HostHotel")]
    public class BookingOnlinesController : ControllerBase
    {
        private readonly IBookingOnlineRepository _repo;
        private readonly IGetValueToken _getValueToken;

        public BookingOnlinesController(IBookingOnlineRepository repo, IGetValueToken getValueToken)
        {
            _repo = repo;
            _getValueToken = getValueToken;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int statusId)
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateStatusModel updateStatusModel)
        {
            try
            {
                Response response = await _repo.UpdateStatusAsync(id, updateStatusModel.StatusId);
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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Response response = await _repo.GetAsync(id);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound(response);
                }
            }
            catch (Exception)
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
