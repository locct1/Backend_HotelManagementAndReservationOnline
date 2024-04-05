using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Helpers.Mail;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Booking;
using MyApiNetCore6.Models.Page;
using System.Data;

namespace MyApiNetCore6.Repositories.ClientRepo
{
    public class PageRepository : IPageRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public PageRepository(AppDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public async Task<Response> GetAllHotelsWithRoomTypesAsync()
        {
            var response = await _context.Hotels!.Include(i => i.RoomTypes.OrderBy(x => x.Price))
                                                 .Where(x => x.Disabled == false)
                                                 .ToListAsync();
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = response
            });
        }
        public async Task<Response> GetHotelDetailAsync(int hotelId)
        {
            var response = await _context.Hotels!
                .Include(p => p.HotelPhotos)
                .Include(f => f.HotelFacilities)
                .ThenInclude(x => x.Facility)
                .ThenInclude(x => x.FacilityType)
                .Include(i => i.RoomTypes.OrderBy(x => x.Price))
                .ThenInclude(k => k.RoomTypePhotos)
                .FirstOrDefaultAsync(x => x.Id == hotelId);
            var listFacilityTypes = await _context.FacilityTypes!.Include(i => i.Facilities).ToListAsync();

            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = new
                {
                    hotel = response,
                    listFacilities = listFacilityTypes
                }
            });
        }
        public async Task<Response> GetBedTypeByRoomTypeAsync(int roomTypeId)
        {
            var response = await _context.RoomTypes!
                .Include(b => b.BedType)
                .Include(b => b.DeviceRoomTypes)
                .ThenInclude(b => b.Device)
                .Where(r => r.Id == roomTypeId)
                .FirstOrDefaultAsync();
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = response
            });
        }

        public async Task<Response> BookingNowAsync(BookingNowModel bookingNowModel, string userId)
        {

            var client = _mapper.Map<Client>(bookingNowModel.InfoClient);
            var clientDbs = await _context.Clients!.Where(c => c.FullName == client.FullName)
                                        .Where(c => c.Email == client.Email)
                                        .Where(c => c.PhoneNumber == client.PhoneNumber)
                                        .Where(c => c.Address == client.Address)
                                       .FirstOrDefaultAsync();
            var checkClient = 0;
            if (clientDbs == null)
            {
                _context.Clients!.Add(client);

                checkClient = await _context.SaveChangesAsync();
            }
            else
            {
                checkClient = 1;
            }
            var bookingOnline = new BookingOnline()
            {
                HotelId = bookingNowModel.BookingOnline.HotelId,
                HotelName = bookingNowModel.BookingOnline.HotelName,
                HotelAddress = bookingNowModel.BookingOnline.HotelAddress,
                HotelPhoneNumber = bookingNowModel.BookingOnline.HotelPhoneNumber,
                Total = bookingNowModel.BookingOnline.Total,
                AmountOfPeople = bookingNowModel.BookingOnline.AmountOfPeople,
                AmountOfNight = bookingNowModel.BookingOnline.AmountOfNight,
                Note = bookingNowModel.BookingOnline.Note,
                StatusId = 1,
                MethodPaymentId = bookingNowModel.BookingOnline.MethodPaymentId,
                StartDate = bookingNowModel.BookingOnline.StartDate,
                EndDate = bookingNowModel.BookingOnline.EndDate,
                CreatedAt = bookingNowModel.BookingOnline.CreatedAt,
                UpdatedAt = bookingNowModel.BookingOnline.UpdatedAt,
                ClientId = clientDbs == null ? client.Id : clientDbs.Id,
                UserId = userId,
            };
            if (bookingNowModel.BookingOnline.MethodPaymentId == 2 || bookingNowModel.BookingOnline.MethodPaymentId == 3)
            {
                bookingOnline.Onl_Amount = bookingNowModel.BookingOnline.Onl_Amount;
                bookingOnline.Onl_BankCode = bookingNowModel.BookingOnline.Onl_BankCode;
                bookingOnline.Onl_OrderInfo = bookingNowModel.BookingOnline.Onl_OrderInfo;
                bookingOnline.Onl_PayDate = bookingNowModel.BookingOnline.Onl_PayDate;
                bookingOnline.Onl_TransactionStatus = bookingNowModel.BookingOnline.Onl_TransactionStatus;
                bookingOnline.Onl_SecureHash = bookingNowModel.BookingOnline.Onl_SecureHash;
                bookingOnline.Onl_TransactionNo = bookingNowModel.BookingOnline.Onl_TransactionNo;
                bookingOnline.Onl_OrderId = bookingNowModel.BookingOnline.Onl_OrderId;
            }
            _context.BookingOnlines!.Add(bookingOnline);
            var checkBookingOnline = await _context.SaveChangesAsync();
            var bookingOnlineDetail = new BookingOnlineDetail()
            {
                RoomTypeName = bookingNowModel.BookingOnline.RoomTypeBookingOnline.RoomTypeName,
                Price = bookingNowModel.BookingOnline.RoomTypeBookingOnline.Price,
                BedTypeName = bookingNowModel.BookingOnline.RoomTypeBookingOnline.BedTypeName,
                AmountOfRoom = bookingNowModel.BookingOnline.RoomTypeBookingOnline.AmountOfRoom,
                BookingOnlineId = bookingOnline.Id,
                RoomTypeId = bookingNowModel.BookingOnline.RoomTypeBookingOnline.RoomTypeId,
            };
            _context.BookingOnlineDetails!.Add(bookingOnlineDetail);
            var checkBookingOnlineDetail = await _context.SaveChangesAsync();
            await _emailSender.SendEmailAsync("kingkongct2001@gmail.com",
                        "Xác nhận đơn đặt phòng FastTravel",
                        @$"<h3>Xin chào {bookingNowModel.InfoClient.FullName},</h3>
                            <p>Bạn nhận được email này vì đã đặt phòng trên Web FastTravel</p>
                            <h3>Thông tin đặt phòng của bạn:</h3>
                            <div><b>Thông tin người nhận phòng:</b></div>  
                            <div><b>Người nhận phòng:</b> {bookingNowModel.InfoClient.FullName}</div>
                            <div><b>Email: </b>{bookingNowModel.InfoClient.Email}</div>
                            <div><b>Số điện thoại: </b>{bookingNowModel.InfoClient.PhoneNumber}</div>
                            <div><b>Địa chỉ:</b> {bookingNowModel.InfoClient.Address}</div>
                            
                            <div><b>Thông tin phòng đặt:</b></div>  
                            <div><b>Khách sạn:</b> {bookingNowModel.BookingOnline.HotelName}</div>
                            <div><b>Địa chỉ:</b> {bookingNowModel.BookingOnline.HotelAddress}</div>
                            <div><b>Số điện thoại:</b> {bookingNowModel.BookingOnline.HotelPhoneNumber}</div>
                            <div><b>Tổng tiền:</b> {bookingNowModel.BookingOnline.Total.ToString("N0")}đ</div>
                            <div><b>Số người:</b> {bookingNowModel.BookingOnline.AmountOfPeople} khách</div>
                            <div><b>Số đêm:</b> {bookingNowModel.BookingOnline.AmountOfNight} đêm</div>
                            <div><b>Ngày nhận phòng:</b> {bookingNowModel.BookingOnline.StartDate}</div>
                            <div><b>Ngày trả phòng:</b> {bookingNowModel.BookingOnline.EndDate}</div>
                            
                            <div><b>Thông tin chi tiết loại phòng:</b></div>  
                            <div><b>Tên loại phòng:</b> {bookingNowModel.BookingOnline.RoomTypeBookingOnline.RoomTypeName}</div>
                            <div><b>Kiểu giường:</b> {bookingNowModel.BookingOnline.RoomTypeBookingOnline.BedTypeName}</div>
                            <div><b>Giá:</b> {bookingNowModel.BookingOnline.RoomTypeBookingOnline.Price.ToString("N0")}đ</div>
                            <div><b>Số lượng phòng:</b> {bookingNowModel.BookingOnline.RoomTypeBookingOnline.AmountOfRoom} phòng</div>
                            
                            <div>Xin chân thành cảm ơn!</div>");
            if (checkClient > 0 && checkBookingOnline > 0 && checkBookingOnlineDetail > 0)
            {
                return (new Response
                {
                    Success = true,
                    Message = "Đặt phòng thành công",
                });
            }
            return (new Response
            {
                Success = false,
                Message = "Đã có lỗi xảy ra vui lòng thử lại trong giây lát",
            });
        }

        public async Task<Response> CheckRoomTypeAvailabilityAsync(CheckRoomTypeAvailabilityModel model)
        {
            var response = _context.BookingOnlineDetails!
                                    .Where(x => x.RoomTypeId == model.RoomTypeId)
                                    .Where(x => x.BookingOnline.EndDate > model.StartDate)
                                    .Where(x => x.BookingOnline.StatusId == 1 || x.BookingOnline.StatusId == 2)
                                    .Sum(r => r.AmountOfRoom);

            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = response
            });

        }

        public async Task<Response> GetBookingByUserAsync(string userId)
        {
            var response = await _context.BookingOnlines!
                                    .Include(c => c.Client)
                                    .Include(c => c.Status)
                                    .Include(u => u.User)
                                    .Include(c => c.BookingOnlineDetails)
                                    .Where(c => c.UserId == userId).ToListAsync();
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = response
            });
        }
        public async Task<Response> GetBookingDetailsByIdAsync(int bookingOnlinetId)
        {
            var response = await _context.BookingOnlines!
                                    .Include(c => c.Client)
                                    .Include(c => c.MethodPayment)
                                    .Include(c => c.Status)
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
                Message = "Đã có lỗi xảy ra, vui lòng thử lại trong giây lát",
                Data = response
            });
        }
        public async Task<Response> CancelBookingAsync(int bookingOnlineId)
        {
            var response = _context.BookingOnlines!.SingleOrDefault(b => b.Id == bookingOnlineId);

            response.StatusId = 3;
            response.UpdatedAt = DateTime.Now;
            _context.BookingOnlines!.Update(response);
            var check = await _context.SaveChangesAsync();
            if (check > 0)
            {
                return (new Response
                {
                    Success = true,
                    Message = "Đã gửi yêu cầu hủy phòng, đang chờ xác nhận",
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
    }
}
