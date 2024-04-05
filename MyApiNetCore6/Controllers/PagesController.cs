using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Helpers;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Booking;
using MyApiNetCore6.Models.Page;
using MyApiNetCore6.Repositories.ClientRepo;

namespace MyApiNetCore6.Controllers
{
    [Route("api/pages")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IPageRepository _repo;
        private readonly IGetValueToken _getValueToken;

        public PagesController(IPageRepository repo, IGetValueToken getValueToken)
        {
            _repo = repo;
            _getValueToken = getValueToken;
        }
        [HttpGet("get-all-hotels-with-roomtypes")]
        public async Task<IActionResult> GetAllHotelsWithRoomTypes()
        {
            try
            {
                Response response = await _repo.GetAllHotelsWithRoomTypesAsync();
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
        [HttpGet("get-hotel-detail/{id}")]
        public async Task<IActionResult> GetHotelDetail(int id)
        {
            try
            {
                Response response = await _repo.GetHotelDetailAsync(id);
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
        [HttpGet("get-bedtype-by-roomtype/{id}")]
        public async Task<IActionResult> GetBedTypeByRoomType(int id)
        {
            try
            {
                Response response = await _repo.GetBedTypeByRoomTypeAsync(id);
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
        [HttpPost("booking-now")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> BookingNow(BookingNowModel bookingNowModel)
        {
            try
            {
                string userId = _getValueToken.GetClaimValue(HttpContext, "Id");
                bookingNowModel.BookingOnline.CreatedAt = DateTime.Now;
                bookingNowModel.BookingOnline.UpdatedAt = bookingNowModel.BookingOnline.CreatedAt;
                bookingNowModel.BookingOnline.StartDate = bookingNowModel.BookingOnline.StartDate.Date.Add(new TimeSpan(14, 00, 00));
                bookingNowModel.BookingOnline.EndDate = bookingNowModel.BookingOnline.EndDate.Date.Add(new TimeSpan(12, 00, 00));
                Response response = await _repo.BookingNowAsync(bookingNowModel, userId);
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
        [HttpPost("check-roomtype-availability")]
        public async Task<IActionResult> CheckRoomTypeAvailability(CheckRoomTypeAvailabilityModel checkRoomTypeAvailabilityModel)
        {
            try
            {
                Response response = await _repo.CheckRoomTypeAvailabilityAsync(checkRoomTypeAvailabilityModel);
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
        [HttpGet("get-booking-by-user")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetBookingByUser()
        {
            try
            {
                string userId = _getValueToken.GetClaimValue(HttpContext, "Id");
                Response response = await _repo.GetBookingByUserAsync(userId);
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
        [HttpGet("get-booking-details-by-id/{id}")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetBookingDetailsById(int id)
        {
            try
            {
                Response response = await _repo.GetBookingDetailsByIdAsync(id);
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
        [HttpGet("cancel-booking/{id}")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            try
            {
                Response response = await _repo.CancelBookingAsync(id);
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
