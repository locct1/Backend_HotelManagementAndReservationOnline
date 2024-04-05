using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Helpers;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.RoomType;
using MyApiNetCore6.Repositories.Host;

namespace MyApiNetCore6.Controllers
{
    [Route("api/roomtypes")]
    [ApiController]
    [Authorize(Roles = "HostHotel")]
    public class RoomTypesController : ControllerBase
    {
        private readonly IRoomTypeRepository _repo;
        private readonly IGetValueToken _getValueToken;


        public RoomTypesController(IRoomTypeRepository repo, IGetValueToken getValueToken)
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
        [HttpPost]
        public async Task<IActionResult> Add(AddRoomTypeModel model)
        {
            try
            {
                var hotelId = _getValueToken.GetClaimValue(HttpContext, "HotelId");
                model.HotelId = Int32.Parse(hotelId);
                Response response = await _repo.AddAsync(model);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRoomTypeModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return NotFound(
                        new Response
                        {
                            Success = false,
                            Message = "Không tìm thấy"
                        }
                        );
                }
                var hotelId = _getValueToken.GetClaimValue(HttpContext, "HotelId");
                model.HotelId = Int32.Parse(hotelId);
                Response response = await _repo.UpdateAsync(id, model);
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
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Response response = await _repo.DeleteAsync(id);
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
