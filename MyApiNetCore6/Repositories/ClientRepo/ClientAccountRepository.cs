using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.ClientAuth;
using MyApiNetCore6.Models.HostAuth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApiNetCore6.Repositories.ClientRepo
{
    public class ClientAccountRepository : IClientAccountRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        public ClientAccountRepository(AppDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }

        public async Task<Response> GetInfoClientAsync(string Id)
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

        public async Task<ResponseToken> SignInAsync(SignInClientModel model)
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
            if (user.Disabled)
            {
                return (new ResponseToken
                {
                    Success = false,
                    Message = "Vui lòng đợi quản trị viên kích hoạt tài khoản",
                    AccessToken = null
                });

            }
            var authClaims = new List<Claim>();
            authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            authClaims.Add(new Claim("Email", user.Email));
            authClaims.Add(new Claim("FullName", user.FullName));
            authClaims.Add(new Claim("Id", user.Id));
            foreach (var role in roles)
            {
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

        public async Task<Response> SignUpAsync(SignUpClientModel model)
        {
            var user = _mapper.Map<ApplicationUser>(model);
            user.UserName = model.Email;
            user.Disabled = false;
            var result = await userManager.CreateAsync(user, model.Password);
            var resultRole = await userManager.AddToRoleAsync(user, "Client");
            if (result.Succeeded && resultRole.Succeeded)
            {
                return (new Response
                {
                    Success = true,
                    Message = "Tạo tài khoản đặt phòng thành công.Vui lòng đăng nhập để xác nhận.",
                    Data = null
                });
            }
            else
            {
                List<IdentityError> errorList = result.Errors.ToList();
                var errors = string.Join(" ", errorList.Select(e => e.Description));
                return (new Response
                {
                    Success = false,
                    Message = errors,
                    Data = null
                });
            }
            return (new Response
            {
                Success = true,
                Message = "Thành công"
            });

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

        public async Task<Response> UpdateAsync(UpdateClientModel model)
        {
            var user = await userManager.FindByNameAsync(model.Email);
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;
            user.FullName = model.FullName;
            if (model.Password != null)
            {
                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, model.Password);
            }
            await userManager.UpdateAsync(user);
            return (new Response
            {
                Success = true,
                Message = "Cập nhật thông tin thành công"
            });

        }
    }
}
