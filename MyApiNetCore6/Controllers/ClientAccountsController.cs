using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.ClientAuth;
using MyApiNetCore6.Repositories.ClientRepo;
using System.Security.Claims;

namespace MyApiNetCore6.Controllers
{
    [Route("api/client-accounts")]

    [ApiController]
    public class ClientAccountsController : ControllerBase
    {
        private readonly IClientAccountRepository _repo;
        public ClientAccountsController(IClientAccountRepository repo)
        {
            _repo = repo;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpClientModel signUpModel)
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
        public async Task<IActionResult> SignIn(SignInClientModel signInClientModel)
        {
            try
            {
                var result = await _repo.SignInAsync(signInClientModel);

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
        [HttpGet("get-info-client")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetInfoClient()
        {
            try
            {
                //var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var Id = GetClaimValue(HttpContext, "Id");
                var result = await _repo.GetInfoClientAsync(Id);

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
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id, UpdateClientModel updateClientModel)
        {
            try
            {
                var result = await _repo.UpdateAsync(updateClientModel);

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
