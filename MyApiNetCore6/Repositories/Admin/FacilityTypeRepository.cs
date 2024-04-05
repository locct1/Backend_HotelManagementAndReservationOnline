using AutoMapper;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models.TypeFacility;
using MyApiNetCore6.Models;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Models.TypeFacility;
using MyApiNetCore6.Models.Facility;
using MyApiNetCore6.Models.HotelFacility;

namespace MyApiNetCore6.Repositories.Admin
{
    public class FacilityTypeRepository : IFacilityTypeRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FacilityTypeRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response> AddAsync(AddFacilityTypeModel model)
        {
            var record = _mapper.Map<FacilityType>(model);
            _context.FacilityTypes!.Add(record);
            await _context.SaveChangesAsync();
            return (new Response
            {
                Success = true,
                Message = "Thêm thành công",
            });
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var response = _context.FacilityTypes!.SingleOrDefault(b => b.Id == id);
            if (response != null)
            {
                _context.FacilityTypes!.Remove(response);
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
            var response = await _context.FacilityTypes!.ToListAsync();
            var data = _mapper.Map<List<FacilityTypeModel>>(response);
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = data
            });
        }
        public async Task<Response> GetAllTypesAndFacilitiesAsync()
        {
            var response = await _context.FacilityTypes!.Include(i => i.Facilities).ToListAsync();
            var data = _mapper.Map<List<HotelFacilityModel>>(response);
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = data
            });
        }
        public async Task<Response> GetAsync(int id)
        {
            var response = await _context.FacilityTypes!.FindAsync(id);
            var data = _mapper.Map<FacilityTypeModel>(response);
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

        public async Task<Response> UpdateAsync(int id, UpdateFacilityTypeModel model)
        {

            var response = _mapper.Map<FacilityType>(model);
            _context.FacilityTypes!.Update(response);
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
