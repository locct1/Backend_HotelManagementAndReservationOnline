using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Helpers;
using MyApiNetCore6.Models;
using MyApiNetCore6.Repositories.Admin;

namespace MyApiNetCore6.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _repo;
        private readonly IGetValueToken _getValueToken;

        public HotelsController(IHotelRepository repo, IGetValueToken getValueToken)
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
        [HttpPut("change-status-host-account/{id}")]
        public async Task<IActionResult> changeStatusHostAccount(string id)
        {
            try
            {
                Response response = await _repo.changeStatusHostAccountAsync(id);
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
        [HttpPut("change-status-hotel/{id}")]
        public async Task<IActionResult> changeStatusHotel(int id)
        {
            try
            {
                Response response = await _repo.changeStatusHotelAsync(id);
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
    }
}
