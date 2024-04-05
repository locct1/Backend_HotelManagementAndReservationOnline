using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;

namespace MyApiNetCore6.Repositories.Admin
{
    public class AdminBookingOnlineRepository:IAdminBookingOnlineRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AdminBookingOnlineRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response> GetAllAsync()
        {
            var response = await _context.BookingOnlines!
                                    .Include(c => c.Client)
                                    .Include(c => c.MethodPayment)
                                    .Include(c => c.Status)
                                    .Include(u => u.User)
                                    .Include(c => c.BookingOnlineDetails)
                                    .ToListAsync();
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = response
            });
        }

        public async Task<Response> UpdateStatusAsync(int bookingOnlineId, int statusId)
        {
            var response = _context.BookingOnlines!.SingleOrDefault(b => b.Id == bookingOnlineId);

            response.StatusId = statusId;
            response.UpdatedAt = DateTime.Now;
            _context.BookingOnlines!.Update(response);
            var check = await _context.SaveChangesAsync();
            if (check > 0)
            {
                return (new Response
                {
                    Success = true,
                    Message = "Cập nhật trạng thái thành công",
                    Data = response
                });
            }
            return (new Response
            {
                Success = false,
                Message = "Có lỗi xảy ra, vui lòng chờ trong giây lát",
                Data = response
            });
        }
        public async Task<Response> GetAsync(int bookingOnlinetId)
        {
            var response = await _context.BookingOnlines!
                                    .Include(c => c.Client)
                                    .Include(c => c.Status)
                                    .Include(c => c.MethodPayment)
                                    .Include(u => u.User)
                                    .Include(c => c.BookingOnlineDetails)
                                    .FirstOrDefaultAsync(x => x.Id == bookingOnlinetId);
            //var data = _mapper.Map<CategoryModel>(response);
            if (response != null)
            {
                return (new Response
                {
                    Success = true,
                    Message = "Thành công",
                    Data = response
                });
            }
            return (new Response
            {
                Success = false,
                Message = "Không tìm thấy",
                Data = response
            });
        }
    }
}
