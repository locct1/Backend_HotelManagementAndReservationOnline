using AutoMapper;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models.AdminAuth;
using MyApiNetCore6.Models.BedType;
using MyApiNetCore6.Models.Booking;
using MyApiNetCore6.Models.CategoryService;
using MyApiNetCore6.Models.ClientAuth;
using MyApiNetCore6.Models.Device;
using MyApiNetCore6.Models.DeviceRoomType;
using MyApiNetCore6.Models.Facility;
using MyApiNetCore6.Models.HostAuth;
using MyApiNetCore6.Models.Hotel;
using MyApiNetCore6.Models.HotelFacility;
using MyApiNetCore6.Models.Image;
using MyApiNetCore6.Models.Room;
using MyApiNetCore6.Models.RoomType;
using MyApiNetCore6.Models.Service;
using MyApiNetCore6.Models.TypeFacility;

namespace MyApiNetCore6.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Facility, AddFacilityModel>().ReverseMap();
            CreateMap<Facility, FacilityModel>().ReverseMap();
            CreateMap<Facility, UpdateFacilityModel>().ReverseMap();

            CreateMap<FacilityType, AddFacilityTypeModel>().ReverseMap();
            CreateMap<FacilityType, FacilityTypeModel>().ReverseMap();
            CreateMap<FacilityType, UpdateFacilityTypeModel>().ReverseMap();

            CreateMap<FacilityType, HotelFacilityModel>().ReverseMap();

            CreateMap<Category, AddCategoryModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Category, UpdateCategoryModel>().ReverseMap();

            CreateMap<Product, AddProductModel>().ReverseMap();
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Product, UpdateProductModel>().ReverseMap();

            CreateMap<BedType, AddBedTypeModel>().ReverseMap();
            CreateMap<BedType, BedTypeModel>().ReverseMap();
            CreateMap<BedType, UpdateBedTypeModel>().ReverseMap();

            CreateMap<Device, AddDeviceModel>().ReverseMap();
            CreateMap<Device, DeviceModel>().ReverseMap();
            CreateMap<Device, UpdateDeviceModel>().ReverseMap();

            CreateMap<RoomType, AddRoomTypeModel>().ReverseMap();
            CreateMap<RoomType, RoomTypeModel>().ReverseMap();
            CreateMap<RoomType, UpdateRoomTypeModel>().ReverseMap();

            CreateMap<Room, AddRoomModel>().ReverseMap();
            CreateMap<Room, RoomModel>().ReverseMap();
            CreateMap<Room, UpdateRoomModel>().ReverseMap();

            CreateMap<ApplicationUser, HostAccountModel>().ReverseMap();
            CreateMap<Hotel, AddHotelModel>().ReverseMap();
            CreateMap<Hotel, HotelViewModel>().ReverseMap();
            CreateMap<ApplicationUser, HostViewModel>().ReverseMap();
            CreateMap<ApplicationUser, AdminViewModel>().ReverseMap();

            CreateMap<HotelFacility, HotelAddFacilityModel>().ReverseMap();
            CreateMap<HotelFacility, HotelFacilityViewModel>().ReverseMap();

            CreateMap<RoomTypePhoto, RoomTypePhotoModel>().ReverseMap();
            CreateMap<HotelPhoto, HotelPhotoModel>().ReverseMap();
            CreateMap<Hotel, HotelAvatarModel>().ReverseMap();

            CreateMap<Hotel, InfoHotelViewModel>().ReverseMap();
            CreateMap<ApplicationUser, HotelAccount>().ReverseMap();

            CreateMap<DeviceRoomType, AddDeviceRoomTypeModel>().ReverseMap();
            CreateMap<DeviceRoomType, DeviceRoomTypeViewModel>().ReverseMap();
            CreateMap<DeviceRoomType, DeviceRoomTypeModel>().ReverseMap();

            CreateMap<ApplicationUser, SignUpClientModel>().ReverseMap();
            CreateMap<ApplicationUser, ClientViewModel>().ReverseMap();

            CreateMap<Client, InfoClient>().ReverseMap();

            CreateMap<Hotel, UpdateHotelModel>().ReverseMap();



        }
    }
}
