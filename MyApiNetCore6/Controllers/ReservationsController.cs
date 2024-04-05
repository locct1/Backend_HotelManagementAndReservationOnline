using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Helpers;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Reservation;
using MyApiNetCore6.Models.Status;
using MyApiNetCore6.Repositories.Host;

namespace MyApiNetCore6.Controllers
{
    [Route("api/hotel-reservations")]
    [ApiController]
    [Authorize(Roles = "HostHotel")]

    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _repo;
        private readonly IGetValueToken _getValueToken;

        public ReservationsController(IReservationRepository repo, IGetValueToken getValueToken)
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
        [HttpPost("add-reservation")]
        public async Task<IActionResult> AddReservation(AddReservationModel model)
        {
            try
            {
                TimeSpan timeOfDay = DateTime.Now.TimeOfDay;
                model.AddReservation.StartDate = model.AddReservation.StartDate.Date.Add(timeOfDay);
                model.AddReservation.EndDate = model.AddReservation.EndDate.Date.Add(new TimeSpan(12, 00, 00));
                Response response = await _repo.AddReservationAsync(model);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
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
        [HttpPut("update-reservation/{id}")]
        public async Task<IActionResult> UpdateReservation(int id, UpdateReservationModel model)
        {

            try
            {
                model.UpdateReservation.EndDate = model.UpdateReservation.EndDate.AddHours(7);
                model.UpdateReservation.EndDate = model.UpdateReservation.EndDate.Date.Add(new TimeSpan(12, 00, 00));
                model.UpdateReservation.StartDate = model.UpdateReservation.StartDate.AddHours(7);
                Response response = await _repo.UpdateReservationAsync(model);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
                return Ok(model);

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
        [HttpPut("update-reservation-status/{id}")]
        public async Task<IActionResult> UpdateReservationStatus(int id, UpdateReservationStatusModel updateReservationStatusModel)
        {
            try
            {
                Response response = await _repo.UpdateReservationStatusAsync(id, updateReservationStatusModel.StatusId);
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
        [HttpPost("check-room-availability")]
        public async Task<IActionResult> CheckRoomAvailability(CheckRoomAvailabilityModel checkRoomAvailabilityModel)
        {
            try
            {
                checkRoomAvailabilityModel.StartDate = checkRoomAvailabilityModel.StartDate.AddHours(7);
                var hotelId = _getValueToken.GetClaimValue(HttpContext, "HotelId");
                Response response = await _repo.CheckRoomAvailabilityAsync(checkRoomAvailabilityModel, Int32.Parse(hotelId));
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
