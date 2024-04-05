using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;

namespace MyApiNetCore6.Repositories.Host
{
    public class HotelDashBoardRepository : IHotelDashBoardRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HotelDashBoardRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response> GetAllAsync(int HotelId)
        {
            var amountOfFacilities = _context.HotelFacilities!.Where(c => c.HotelId == HotelId).Count();
            var amountOfCategory = _context.Categories!.Where(c => c.HotelId == HotelId).Count();
            var amountOfProduct = _context.Products!.Where(c => c.Category.HotelId == HotelId).Count();
            var amountOfRoomType = _context.RoomTypes!.Where(c => c.HotelId == HotelId).Count();
            var amountOfRoom = _context.Rooms!.Where(c => c.RoomType.HotelId == HotelId).Count();
            var amountOfBookingOnline = _context.BookingOnlines!.Where(c => c.HotelId == HotelId).Count();
            var amountOfReservation = _context.Reservations!.Where(c => c.HotelId == HotelId).Count();
            var amountOfClientOffline = _context.Reservations!.Where(c => c.HotelId == HotelId).Select(x => x.ClientOfflineId).Distinct().Count();
            var totalReservation = _context.Reservations!.Where(c => c.HotelId == HotelId).Where(r => r.StatusId == 9).Sum(x => x.Total);
            var reservations = await _context.Reservations!.Where(c => c.HotelId == HotelId).Where(r => r.StatusId == 9).
                                                     Include(x => x.RoomReservations)
                                                     .ThenInclude(x => x.RoomReservationProducts).ToListAsync();
            var totalService = 0.0;
            foreach (var reservation in reservations)
            {
                foreach (var room in reservation.RoomReservations)
                {
                    foreach (var product in room.RoomReservationProducts)
                    {
                        totalService = totalService + product.Price * product.Quantity;
                    }
                }
            }
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = new
                {
                    AmountOfCategory = amountOfCategory,
                    AmountOfProduct = amountOfProduct,
                    AmountOfRoom = amountOfRoom,
                    AmountOfBookingOnline = amountOfBookingOnline,
                    AmountOfReservation = amountOfReservation,
                    AmountOfClientOffline = amountOfClientOffline,
                    AmountOfFacilities = amountOfFacilities,
                    AmountOfRoomType = amountOfRoomType,
                    TotalReservation = totalReservation,
                    TotalService = totalService,

                }
            });
        }
    }
}
