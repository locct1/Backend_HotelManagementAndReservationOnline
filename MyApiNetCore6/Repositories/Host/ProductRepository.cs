using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Service;

namespace MyApiNetCore6.Repositories.Host
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response> AddAsync(AddProductModel model)
        {
            var record = _mapper.Map<Product>(model);
            _context.Products!.Add(record);
            await _context.SaveChangesAsync();
            return (new Response
            {
                Success = true,
                Message = "Thêm thành công",
            });
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var response = _context.Products!.SingleOrDefault(b => b.Id == id);
            if (response != null)
            {
                _context.Products!.Remove(response);
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

        public async Task<Response> GetAllAsync(int HotelId)
        {
            var response = await _context.Products!.Include(i => i.Category).Where(c => c.Category.HotelId == HotelId).ToListAsync();
            var data = _mapper.Map<List<ProductModel>>(response);
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = data

            });
        }

        public async Task<Response> GetAsync(int id)
        {
            var response = await _context.Products!.Include(i => i.Category).FirstOrDefaultAsync(x => x.Id == id);
            var data = _mapper.Map<ProductModel>(response);
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

        public async Task<Response> UpdateAsync(int id, UpdateProductModel model)
        {
            var response = _mapper.Map<Product>(model);
            _context.Products!.Update(response);
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
                Message = "Cập nhật thành công",
            });

        }
    }
}
