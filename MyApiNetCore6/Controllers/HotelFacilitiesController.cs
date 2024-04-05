using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Helpers;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.CategoryService;
using MyApiNetCore6.Models.Facility;
using MyApiNetCore6.Models.HotelFacility;
using MyApiNetCore6.Repositories.Admin;
using MyApiNetCore6.Repositories.Host;
using System.Data;

namespace MyApiNetCore6.Controllers
{
    [Route("api/hotel-facilities")]
    [ApiController]
    [Authorize(Roles = "HostHotel,Admin")]

    public class HotelFacilitiesController : ControllerBase
    {
        private readonly IHotelFacilityRepository _repo;
        private readonly IGetValueToken _getValueToken;

        public HotelFacilitiesController(IHotelFacilityRepository repo, IGetValueToken getValueToken)
        {
            _repo = repo;
            _getValueToken = getValueToken;
        }
        [HttpGet("get-all-types-and-facilities")]
        public async Task<IActionResult> GetAllFacilities()
        {
            try
            {
                Response response = await _repo.GetAllTypesAndFacilitiesAsync();
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
        [HttpGet("get-hotel-faticities")]
        public async Task<IActionResult> GetHotelFaticities()
        {
            try
            {
                var hotelId = _getValueToken.GetClaimValue(HttpContext, "HotelId");
                Response response = await _repo.GetHotelFaticitiesAsync(Int32.Parse(hotelId));
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
        [HttpPost("add-facilities-for-hotel")]
        public async Task<IActionResult> AddFacilitiesForHotel(HotelAddFacilitiesModel model)
        {

            try
            {
                var hotelId = _getValueToken.GetClaimValue(HttpContext, "HotelId");
                model.HotelId = Int32.Parse(hotelId);
                Response response = await _repo.AddFacilitiesForHotelAsync(model);
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
    }
}
