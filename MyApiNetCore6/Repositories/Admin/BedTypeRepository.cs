using AutoMapper;
using MyApiNetCore6.Data;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.BedType;

namespace MyApiNetCore6.Repositories.Admin
{
    public class BedTypeRepository : IBedTypeRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BedTypeRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response> AddAsync(AddBedTypeModel model)
        {
            var record = _mapper.Map<BedType>(model);
            _context.BedTypes!.Add(record);
            await _context.SaveChangesAsync();
            return (new Response
            {
                Success = true,
                Message = "Thêm thành công",
            });
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var response = _context.BedTypes!.SingleOrDefault(b => b.Id == id);
            if (response != null)
            {
                _context.BedTypes!.Remove(response);
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
            var response = await _context.BedTypes!.ToListAsync();
            var data = _mapper.Map<List<BedTypeModel>>(response);
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = data
            });
        }

        public async Task<Response> GetAsync(int id)
        {
            var response = await _context.BedTypes!.FindAsync(id);
            var data = _mapper.Map<BedTypeModel>(response);
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

        public async Task<Response> UpdateAsync(int id, UpdateBedTypeModel model)
        {

            var response = _mapper.Map<BedType>(model);
            _context.BedTypes!.Update(response);
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
