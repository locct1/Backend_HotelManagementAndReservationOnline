using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;

namespace MyApiNetCore6.Repositories.Admin
{
    public class AdminDashBoardRepository:IAdminDashBoardRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AdminDashBoardRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response> GetAllAsync()
        {
            var amountOfFacilityTypes = _context.FacilityTypes!.Count();
            var amountOfFacilities = _context.Facilities!.Count();
            var amountOfBedTypes = _context.BedTypes!.Count();
            var amountOfDevices = _context.Devices!.Count();
            var amountOfBookingOnlines = _context.BookingOnlines!.Count();
            var amountOfUser = _context.Users.Count();
            var amountOfHotels=_context.Hotels!.Count(); 
            var amountOfHotelActives = _context.Hotels!.Where(h=>h.Disabled==false).Count();
            var totalBookingOnline = _context.BookingOnlines!.Where(r => r.StatusId == 6).Sum(x => x.Total);
           
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = new
                {
                  
                    AmountOfBookingOnlines = amountOfBookingOnlines,
                    AmountOfUser = amountOfUser,
                    AmountOfFacilityTypes = amountOfFacilityTypes,
                    AmountOfFacilities = amountOfFacilities,
                    AmountOfBedTypes = amountOfBedTypes,
                    AmountOfDevices=amountOfDevices,
                    AmountOfHotels= amountOfHotels,
                    AmountOfHotelActives= amountOfHotelActives,
                    TotalBookingOnline = totalBookingOnline,

                }
            });
        }
    }
}
