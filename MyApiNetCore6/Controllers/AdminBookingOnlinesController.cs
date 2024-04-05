using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Helpers;
using MyApiNetCore6.Models.Status;
using MyApiNetCore6.Models;
using System.Data;
using MyApiNetCore6.Repositories.Admin;

namespace MyApiNetCore6.Controllers
{
    [Route("api/admin-manage-booking-online")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminBookingOnlinesController : ControllerBase
    {
        private readonly IAdminBookingOnlineRepository _repo;
        private readonly IGetValueToken _getValueToken;

        public AdminBookingOnlinesController(IAdminBookingOnlineRepository repo, IGetValueToken getValueToken)
        {
            _repo = repo;
            _getValueToken = getValueToken;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int statusId)
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
