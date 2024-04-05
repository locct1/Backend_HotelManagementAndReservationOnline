using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.RoomType;

namespace MyApiNetCore6.Repositories.Host
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RoomTypeRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response> AddAsync(AddRoomTypeModel model)
        {
            var record = _mapper.Map<RoomType>(model);
            _context.RoomTypes!.Add(record);
            await _context.SaveChangesAsync();
            return (new Response
            {
                Success = true,
                Message = "Thêm thành công",
            });
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var response = _context.RoomTypes!.SingleOrDefault(b => b.Id == id);
            if (response != null)
            {
                _context.RoomTypes!.Remove(response);
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

        public async Task<Response> GetAllAsync(int HotelId)
        {
            var response = await _context.RoomTypes!.Where(c => c.HotelId == HotelId)
                                                    .Include(i => i.BedType).Include(i => i.DeviceRoomTypes)
                                                    .ThenInclude(i => i.Device).ToListAsync();
            var data = _mapper.Map<List<RoomTypeModel>>(response);
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = data

            });
        }

        public async Task<Response> GetAsync(int id)
        {
            var response = await _context.RoomTypes!.Include(i => i.BedType).FirstOrDefaultAsync(x => x.Id == id);
            var data = _mapper.Map<RoomTypeModel>(response);
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

        public async Task<Response> UpdateAsync(int id, UpdateRoomTypeModel model)
        {
            var response = _mapper.Map<RoomType>(model);
            _context.RoomTypes!.Update(response);
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
