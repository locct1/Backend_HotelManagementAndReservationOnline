using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Device;
using MyApiNetCore6.Models.DeviceRoomType;

namespace MyApiNetCore6.Repositories.Host
{
    public class DeviceRoomTypeRepository : IDeviceRoomTypeRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DeviceRoomTypeRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> AddAsync(AddDeviceRoomTypesModel model)
        {
            _context.DeviceRoomTypes.RemoveRange(_context.DeviceRoomTypes.Where(x => x.RoomTypeId == model.RoomTypeId));
            await _context.SaveChangesAsync();
            foreach (int DeviceId in model.Devices)
            {
                AddDeviceRoomTypeModel addModel = new AddDeviceRoomTypeModel();
                addModel.DeviceId = DeviceId;
                addModel.RoomTypeId = model.RoomTypeId;
                var record = _mapper.Map<DeviceRoomType>(addModel);
                _context.DeviceRoomTypes!.Add(record);
                var check = await _context.SaveChangesAsync();

            }
            return (new Response
            {
                Success = true,
                Message = "Cập nhật thành công",
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
        public async Task<Response> GetAllDeviceRoomTypeAsync(int roomTypeId)
        {
            var response = await _context.DeviceRoomTypes!.Where(i => i.RoomTypeId == roomTypeId).ToListAsync();
            var data = _mapper.Map<List<DeviceRoomTypeViewModel>>(response);
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = data
            });

        }
    }
}
