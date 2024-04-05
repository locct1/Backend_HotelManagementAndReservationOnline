using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Helpers;
using MyApiNetCore6.Models.HostAuth;
using MyApiNetCore6.Models;
using MyApiNetCore6.Repositories.Host;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using MyApiNetCore6.Repositories.Admin;
using MyApiNetCore6.Models.AdminAuth;

namespace MyApiNetCore6.Controllers
{
    [Route("api/admin-accounts")]
    [ApiController]
    public class AdminAccountsController : ControllerBase
    {
        private readonly IAdminAccountRepository _repo;
        private readonly IGetValueToken _getValueToken;

        public AdminAccountsController(IAdminAccountRepository repo, IGetValueToken getValueToken)
        {
            _repo = repo;
            _getValueToken = getValueToken;
        }
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInAdminModel signInAdminModel)
        {
            try
            {
                var result = await _repo.SignInAsync(signInAdminModel);

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
        [HttpGet("get-info-admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetInfoHost()
        {
            try
            {
                //var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var Id = _getValueToken.GetClaimValue(HttpContext, "Id");
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
    }
}
