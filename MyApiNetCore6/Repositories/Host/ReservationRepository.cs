using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Helpers.Mail;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Reservation;

namespace MyApiNetCore6.Repositories.Host
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ReservationRepository(AppDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public async Task<Response> AddReservationAsync(AddReservationModel model)
        {
            var clientOffline = new ClientOffline()
            {
                FullName = model.InfoClientOffline.FullName,
                PhoneNumber = model.InfoClientOffline.PhoneNumber,
                Address = model.InfoClientOffline.Address,
                Email = model.InfoClientOffline.Email,
            };
            _context.ClientOfflines!.Add(clientOffline);
            await _context.SaveChangesAsync();
            var reservation = new Reservation()
            {
                HotelId = model.AddReservation.HotelId,
                HotelName = model.AddReservation.HotelName,
                HotelAddress = model.AddReservation.HotelAddress,
                HotelPhoneNumber = model.AddReservation.HotelPhoneNumber,
                Total = model.AddReservation.Total,
                AmountOfPeople = model.AddReservation.AmountOfPeople,
                AmountOfNight = model.AddReservation.AmountOfNight,
                Note = model.AddReservation.Note,
                StatusId = 7,
                StartDate = model.AddReservation.StartDate,
                EndDate = model.AddReservation.EndDate,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ClientOfflineId = clientOffline.Id,
            };
            _context.Reservations!.Add(reservation);
            await _context.SaveChangesAsync();
            foreach (var room in model.AddReservation.RoomReservations)
            {
                var addRoom = new RoomReservation()
                {
                    RoomName = room.RoomName,
                    RoomTypeName = room.RoomTypeName,
                    BedTypeName = room.BedTypeName,
                    Price = room.Price,
                    RoomId = room.RoomId,
                    ReservationId = reservation.Id,
                };
                _context.RoomReservations!.Add(addRoom);
                await _context.SaveChangesAsync();
                foreach (var product in room.ListProducts)
                {
                    var addProduct = new RoomReservationProduct()
                    {
                        RoomRervationId = addRoom.Id,
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Price = product.Price,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Quantity = product.Quantity,
                        Amount = product.Quantity * product.Price,
                    };
                    _context.RoomReservationProducts!.Add(addProduct);
                    await _context.SaveChangesAsync();
                }
            }
            return (new Response
            {
                Success = true,
                Message = "Tạp đơn đặt phòng thành công",
            });
        }

        public async Task<Response> CheckRoomAvailabilityAsync(CheckRoomAvailabilityModel model, int hotelId)
        {
            var response = await _context.RoomReservations!.Where(x => x.Reservation.EndDate > model.StartDate)
                                    .Where(x => x.Reservation.HotelId == hotelId)
                                   .Where(x => x.Reservation.StatusId == 7 || x.Reservation.StatusId == 8)
                                   .ToListAsync();

            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = response
            });
        }

        public async Task<Response> GetAllAsync(int hotelId)
        {
            var response = await _context.Reservations!
                                   .Include(c => c.ClientOffline)
                                   .Include(c => c.Status)
                                   .Include(c => c.RoomReservations)
                                   .ThenInclude(c => c.RoomReservationProducts)
                                   .Where(c => c.HotelId == hotelId).ToListAsync();
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = response
            });
        }

        public async Task<Response> UpdateReservationAsync(UpdateReservationModel model)
        {
            var clientOffline = _context.ClientOfflines!.SingleOrDefault(b => b.Id == model.InfoClientOffline.Id);
            if (clientOffline != null)
            {
                clientOffline.FullName = model.InfoClientOffline.FullName;
                clientOffline.Address = model.InfoClientOffline.Address;
                clientOffline.PhoneNumber = model.InfoClientOffline.PhoneNumber;
                clientOffline.Email = model.InfoClientOffline.Email;
                _context.ClientOfflines!.Update(clientOffline);
                await _context.SaveChangesAsync();

                var reservation = _context.Reservations!.SingleOrDefault(b => b.Id == model.UpdateReservation.Id);
                if (reservation != null)
                {

                    reservation.AmountOfNight = model.UpdateReservation.AmountOfNight;
                    reservation.StatusId = model.UpdateReservation.StatusId;
                    reservation.AmountOfPeople = model.UpdateReservation.AmountOfPeople;
                    if (reservation.StartDate != model.UpdateReservation.StartDate)
                    {
                        reservation.StartDate = model.UpdateReservation.StartDate;
                    }
                    reservation.EndDate = model.UpdateReservation.EndDate;
                    reservation.Total = model.UpdateReservation.Total;
                    reservation.HotelName = model.UpdateReservation.HotelName;
                    reservation.HotelAddress = model.UpdateReservation.HotelAddress;
                    reservation.HotelPhoneNumber = model.UpdateReservation.HotelPhoneNumber;
                    reservation.Note = model.UpdateReservation.Note;
                    reservation.UpdatedAt = DateTime.Now;
                }
                _context.Reservations!.Update(reservation);
                await _context.SaveChangesAsync();
                foreach (var room in model.UpdateReservation.RoomReservations)
                {
                    var roomReservation = _context.RoomReservations!.SingleOrDefault(b => b.Id == room.Id);
                    roomReservation.BedTypeName = room.BedTypeName;
                    roomReservation.RoomTypeName = room.RoomTypeName;
                    roomReservation.Price = room.Price;
                    roomReservation.RoomName = room.RoomName;
                    if (roomReservation.RoomId != room.RoomId)
                    {
                        var roomUpdate = _context.Rooms!.SingleOrDefault(b => b.Id == room.RoomId);
                        roomUpdate.Status = 0;
                        _context.Rooms!.Update(roomUpdate);
                        await _context.SaveChangesAsync();
                    }
                    roomReservation.RoomId = room.RoomId;
                    _context.RoomReservations!.Update(roomReservation);
                    await _context.SaveChangesAsync();
                    _context.RoomReservationProducts.RemoveRange(_context.RoomReservationProducts.Where(x => x.RoomRervationId == room.Id));
                    await _context.SaveChangesAsync();
                    foreach (var product in room.roomReservationProducts)
                    {
                        var addProduct = new RoomReservationProduct()
                        {
                            RoomRervationId = room.Id,
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            Price = product.Price,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            Quantity = product.Quantity,
                            Amount = product.Quantity * product.Price,
                        };
                        _context.RoomReservationProducts!.Add(addProduct);
                        await _context.SaveChangesAsync();
                    }
                }
                return (new Response
                {
                    Success = true,
                    Message = "Cập nhật thành công",
                    Data = null
                });
            }
            else
            {
                return (new Response
                {
                    Success = false,
                    Message = "Không tìm thấy",
                });
            }
        }

        public async Task<Response> UpdateReservationStatusAsync(int reservationId, int statusId)
        {
            var reservation = _context.Reservations!.Include(x => x.RoomReservations).SingleOrDefault(b => b.Id == reservationId);
            if (statusId == 9)
            {
                reservation.EndDate = DateTime.Now;
                double amountOfNight = reservation.EndDate.ToOADate() - reservation.StartDate.ToOADate();
                int covertAmountOfNight = Convert.ToInt32(amountOfNight);
                if (covertAmountOfNight < 1)
                {
                    covertAmountOfNight = 1;
                }
                reservation.AmountOfNight = covertAmountOfNight;
                double total = 0.0;
                foreach (var room in reservation.RoomReservations)
                {
                    total = total + room.Price * covertAmountOfNight;
                    var roomUpdate = _context.Rooms!.SingleOrDefault(b => b.Id == room.RoomId);
                    roomUpdate.Status = 0;
                    _context.Rooms!.Update(roomUpdate);
                    await _context.SaveChangesAsync();
                }
                reservation.Total = total;
            }
            if (statusId == 8)
            {
                reservation.StartDate = DateTime.Now;

            }
            reservation.StatusId = statusId;
            reservation.UpdatedAt = DateTime.Now;
            _context.Reservations!.Update(reservation);
            var check = await _context.SaveChangesAsync();
            if (check > 0)
            {
                return (new Response
                {
                    Success = true,
                    Message = "Cập nhật trạng thái thành công",
                    Data = reservation
                });
            }
            return (new Response
            {
                Success = false,
                Message = "Có lỗi xảy ra, vui lòng chờ trong giây lát",
                Data = reservation
            });
        }
    }
}
