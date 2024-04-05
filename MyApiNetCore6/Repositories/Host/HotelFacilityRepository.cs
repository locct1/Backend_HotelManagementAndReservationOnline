using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models.Facility;
using MyApiNetCore6.Models;
using AutoMapper;
using MyApiNetCore6.Models.HotelFacility;

namespace MyApiNetCore6.Repositories.Host
{
    public class HotelFacilityRepository : IHotelFacilityRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HotelFacilityRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<Response> GetHotelFaticitiesAsync(int hotelId)
        {
            var response = await _context.HotelFacilities!.Where(i => i.HotelId == hotelId).ToListAsync();
            var data = _mapper.Map<List<HotelFacilityViewModel>>(response);
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = data
            });
        }
        public async Task<Response> AddFacilitiesForHotelAsync(HotelAddFacilitiesModel model)
        {
            _context.HotelFacilities.RemoveRange(_context.HotelFacilities.Where(x => x.HotelId == model.HotelId));
            await _context.SaveChangesAsync();
            foreach (int FacilityId in model.Facilities)
            {
                HotelAddFacilityModel addModel = new HotelAddFacilityModel();
                addModel.FacilityId = FacilityId;
                addModel.HotelId = model.HotelId;
                var record = _mapper.Map<HotelFacility>(addModel);
                _context.HotelFacilities!.Add(record);
                var check = await _context.SaveChangesAsync();

            }
            return (new Response
            {
                Success = true,
                Message = "Cập nhật thành công",
            });
        }
    }
}
