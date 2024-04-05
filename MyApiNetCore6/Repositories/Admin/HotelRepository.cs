using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Hotel;

namespace MyApiNetCore6.Repositories.Admin
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public HotelRepository(AppDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;

        }

        public async Task<Response> changeStatusHostAccountAsync(string userId)
        {
            var response = _context.Users!.SingleOrDefault(b => b.Id == userId);
            if (response != null)
            {
                response.Disabled = !response.Disabled;
                _context.Users!.Update(response);
                await _context.SaveChangesAsync();
                return (new Response
                {
                    Success = true,
                    Message = response.Disabled ? "Hủy kích hoạt thành công" : "Đã kích hoạt thành công",
                    Data = null
                });
            }
            return (new Response
            {
                Success = false,
                Message = "Không tìm thấy",
            });
        }

        public async Task<Response> changeStatusHotelAsync(int hotelId)
        {
            var response = _context.Hotels!.SingleOrDefault(b => b.Id == hotelId);
            if (response != null)
            {
                response.Disabled = !response.Disabled;
                _context.Hotels!.Update(response);
                await _context.SaveChangesAsync();
                return (new Response
                {
                    Success = true,
                    Message = response.Disabled ? "Ẩn thành công" : "Hiện thành công",
                    Data = null
                });
            }
            return (new Response
            {
                Success = false,
                Message = "Không tìm thấy",
            });
        }

        public async Task<Response> GetAllAsync()
        {
            var response = await _context.Hotels!.Include(i => i.Users).ToListAsync();
            var data = _mapper.Map<List<InfoHotelViewModel>>(response);
            foreach (var item in data)
            {
                foreach (var user in item.Users)
                {
                    var record = await _userManager.FindByNameAsync(user.Email);
                    var roles = await _userManager.GetRolesAsync(record);
                    foreach (var role in roles)
                    {
                        if (role == "HostHotel")
                        {
                            item.HotelAccount = user;
                            item.HotelAccount.Role = role;

                        }
                    }
                }
            }
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = data
            });
        }

        public async Task<Response> GetAsync(int hotelId)
        {
            var response = await _context.Hotels!.Include(i => i.Users).FirstOrDefaultAsync(x => x.Id == hotelId);
            var data = _mapper.Map<InfoHotelViewModel>(response);

            foreach (var user in data.Users)
            {
                var record = await _userManager.FindByNameAsync(user.Email);
                var roles = await _userManager.GetRolesAsync(record);
                foreach (var role in roles)
                {
                    if (role == "HostHotel")
                    {

                        data.HotelAccount = user;
                        data.HotelAccount.Role = role;
                    }
                }
            }

            if (response != null)
            {
                return (new Response
                {
                    Success = true,
                    Message = "Thành công",
                    Data = data
                });
            }
            return (new Response
            {
                Success = false,
                Message = "Không tìm thấy",
                Data = null
            });
        }
    }
}
