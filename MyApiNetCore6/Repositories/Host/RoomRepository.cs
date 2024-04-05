using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Room;

namespace MyApiNetCore6.Repositories.Host
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RoomRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response> AddAsync(AddRoomModel model)
        {
            var record = _mapper.Map<Room>(model);
            _context.Rooms!.Add(record);
            await _context.SaveChangesAsync();
            return (new Response
            {
                Success = true,
                Message = "Thêm thành công",
            });
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var response = _context.Rooms!.SingleOrDefault(b => b.Id == id);
            if (response != null)
            {
                _context.Rooms!.Remove(response);
                await _context.SaveChangesAsync();
                return (new Response
                {
                    Success = true,
                    Message = "Xóa thành công",
                    Data = null
                });
            }
            return (new Response
            {
                Success = false,
                Message = "Không tìm thấy",
            });
        }

        public async Task<Response> ChangeStatusRoomAsync(int id)
        {
            var response = _context.Rooms!.SingleOrDefault(b => b.Id == id);
            if (response != null)
            {
                if (response.Status == 1)
                {
                    response.Status = 0;
                }
                else if (response.Status == 0)
                {
                    response.Status = 1;
                }
                _context.Rooms!.Update(response);
                await _context.SaveChangesAsync();
                return (new Response
                {
                    Success = true,
                    Message = "Cập nhật trạng thái thành công",
                    Data = null
                });
            }
            return (new Response
            {
                Success = false,
                Message = "Không tìm thấy",
            });
        }

        public async Task<Response> GetAllAsync(int HotelId)
        {
            var response = await _context.Rooms.Include(i => i.RoomType).ThenInclude(b => b.BedType)
                                               .Where(r => r.RoomType.HotelId == HotelId).ToListAsync();
            var data = _mapper.Map<List<RoomModel>>(response);
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = data

            });
        }

        public async Task<Response> GetAsync(int id)
        {
            var response = await _context.Rooms!.Include(i => i.RoomType).FirstOrDefaultAsync(x => x.Id == id);
            var data = _mapper.Map<RoomModel>(response);
            if (data != null)
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
                Data = data
            });
        }

        public async Task<Response> UpdateAsync(int id, UpdateRoomModel model)
        {
            var response = _mapper.Map<Room>(model);
            _context.Rooms!.Update(response);
            var check = await _context.SaveChangesAsync();
            if (check > 0)
            {
                return (new Response
                {
                    Success = true,
                    Message = "Cập nhật thành công",
                });
            }
            return (new Response
            {
                Success = false,
                Message = "Cập nhật thành công",
            });

        }
    }
}
