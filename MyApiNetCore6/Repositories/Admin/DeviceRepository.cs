using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Device;

namespace MyApiNetCore6.Repositories.Admin
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DeviceRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response> AddAsync(AddDeviceModel model)
        {
            var record = _mapper.Map<Device>(model);
            _context.Devices!.Add(record);
            await _context.SaveChangesAsync();
            return (new Response
            {
                Success = true,
                Message = "Thêm thành công",
            });
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var response = _context.Devices!.SingleOrDefault(b => b.Id == id);
            if (response != null)
            {
                _context.Devices!.Remove(response);
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

        public async Task<Response> GetAllAsync()
        {
            var response = await _context.Devices!.ToListAsync();
            var data = _mapper.Map<List<DeviceModel>>(response);
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = data
            });
        }

        public async Task<Response> GetAsync(int id)
        {
            var response = await _context.Devices!.FindAsync(id);
            var data = _mapper.Map<DeviceModel>(response);
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

        public async Task<Response> UpdateAsync(int id, UpdateDeviceModel model)
        {

            var response = _mapper.Map<Device>(model);
            _context.Devices!.Update(response);
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
                Message = "Cập nhật thật bại",
            });
        }
    }
}
