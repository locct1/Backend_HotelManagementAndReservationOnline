using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.AdminAuth;
using MyApiNetCore6.Models.HostAuth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApiNetCore6.Repositories.Admin
{
    public class AdminAccountRepository : IAdminAccountRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        public AdminAccountRepository(AppDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser>
            signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }

        public async Task<Response> GetInfoHostAsync(string Id)
        {
            var response = await _context.Users!.FirstOrDefaultAsync(x => x.Id == Id);
            var data = _mapper.Map<HostViewModel>(response);
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = data

            });
        }

        public async Task<ResponseToken> SignInAsync(SignInAdminModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
            {
                return (new ResponseToken
                {
                    Success = false,
                    Message = "Email hoặc mật khẩu không đúng",
                    AccessToken = null
                });
            }
            var user = await userManager.FindByNameAsync(model.Email);
            var roles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>();
            authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            authClaims.Add(new Claim("Email", user.Email));
            authClaims.Add(new Claim("FullName", user.FullName));
            authClaims.Add(new Claim("Id", user.Id));
            foreach (var role in roles)
            {
                if (role == "Client" || role=="HostHotel")
                {
                    return (new ResponseToken
                    {
                        Success = false,
                        Message = "Bạn không có quyền truy cập vào trang này",
                        AccessToken = null
                    });
                }
                authClaims.Add(new Claim("role", role));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );
            return (new ResponseToken
            {
                Success = true,
                Message = "Thành công",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            });

        }
    }
}
