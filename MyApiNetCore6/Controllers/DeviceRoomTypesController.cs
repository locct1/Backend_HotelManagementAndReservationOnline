using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Helpers;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.DeviceRoomType;
using MyApiNetCore6.Repositories.Host;

namespace MyApiNetCore6.Controllers
{
    [Route("api/device-roomtypes")]
    [ApiController]
    public class DeviceRoomTypesController : ControllerBase
    {
        private readonly IDeviceRoomTypeRepository _repo;
        private readonly IGetValueToken _getValueToken;

        public DeviceRoomTypesController(IDeviceRoomTypeRepository repo, IGetValueToken getValueToken)
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
        [HttpPost]
        public async Task<IActionResult> Add(AddDeviceRoomTypesModel model)
        {

            try
            {
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

        [HttpGet("get-all-device-roomtype/{id}")]
        public async Task<IActionResult> GetAllDeviceRoomTypeAsync(int id)
        {
            try
            {
                Response response = await _repo.GetAllDeviceRoomTypeAsync(id);
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
