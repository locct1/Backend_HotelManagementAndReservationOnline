using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Helpers;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.TypeFacility;
using MyApiNetCore6.Repositories.Admin;

namespace MyApiNetCore6.Controllers
{
    [Route("api/facilitytypes")]
    [ApiController]
    public class FacilityTypesController : ControllerBase
    {
        private readonly IFacilityTypeRepository _repo;
        private readonly IGetValueToken _getValueToken;

        public FacilityTypesController(IFacilityTypeRepository repo, IGetValueToken getValueToken)
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
        public async Task<IActionResult> Add(AddFacilityTypeModel model)
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
        public async Task<IActionResult> Update(int id, UpdateFacilityTypeModel model)
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
            try
            {
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
                throw;
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
