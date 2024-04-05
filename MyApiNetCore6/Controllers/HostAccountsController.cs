using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.HostAuth;
using MyApiNetCore6.Models.Hotel;
using MyApiNetCore6.Repositories.Host;
using System.Security.Claims;

namespace MyApiNetCore6.Controllers
{
    [Route("api/host-accounts")]
    [ApiController]
    public class HostAccountsController : ControllerBase
    {
        private readonly IHostAccountRepository _repo;
        public HostAccountsController(IHostAccountRepository repo)
        {
            _repo = repo;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpHostModel signUpModel)
        {
            try
            {
                Response response = await _repo.SignUpAsync(signUpModel);
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
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInHostModel signInHostModel)
        {
            try
            {
                var result = await _repo.SignInAsync(signInHostModel);

                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return Ok(result);
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
        [HttpGet("get-info-host")]
        [Authorize(Roles = "HostHotel")]
        public async Task<IActionResult> GetInfoHost()
        {
            try
            {
                //var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var Id = GetClaimValue(HttpContext, "Id");
                var result = await _repo.GetInfoHostAsync(Id);

                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return Ok(result);
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
        [HttpPut("update-hotel/{id}")]
        public async Task<IActionResult> UpdateHotel(int id, UpdateHotelModel updateHotelModel)
        {

            try
            {
                if (id != updateHotelModel.Id)
                {
                    return NotFound(
                        new Response
                        {
                            Success = false,
                            Message = "Không tìm thấy"
                        }
                        );
                }
                Response response = await _repo.UpdateHotelAsync(id, updateHotelModel);
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
        public static string GetClaimValue(HttpContext httpContext, string valueType)
        {
            if (string.IsNullOrEmpty(valueType)) return null;
            var identity = httpContext.User.Identity as ClaimsIdentity;
            var valueObj = identity == null ? null : identity.Claims.FirstOrDefault(x => x.Type == valueType);
            return valueObj == null ? null : valueObj.Value;
        }
        public static List<string> GetRoleClaimValue(HttpContext httpContext)
        {
            List<Claim> roleClaims = httpContext.User.FindAll(ClaimTypes.Role).ToList();
            var roles = new List<string>();

            foreach (var role in roleClaims)
            {
                roles.Add(role.Value);
            }
            return roles;
        }
    }
}
