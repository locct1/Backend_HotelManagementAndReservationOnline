using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.CategoryService;

namespace MyApiNetCore6.Repositories.Host
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response> AddAsync(AddCategoryModel model)
        {
            var record = _mapper.Map<Category>(model);
            _context.Categories!.Add(record);
            var check = await _context.SaveChangesAsync();
            if (check > 0)
            {
                return (new Response
                {
                    Success = true,
                    Message = "Thêm thành công",
                });
            }
            return (new Response
            {
                Success = false,
                Message = "Thêm thật bại",
            });
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var response = _context.Categories!.SingleOrDefault(b => b.Id == id);
            if (response != null)
            {
                _context.Categories!.Remove(response);
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
            var response = await _context.Categories!.Where(c => c.HotelId == HotelId).ToListAsync();
            var data = _mapper.Map<List<CategoryModel>>(response);
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = data
            });
        }

        public async Task<Response> GetAsync(int id)
        {
            var response = await _context.Categories!.FindAsync(id);
            var data = _mapper.Map<CategoryModel>(response);
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

        public async Task<Response> UpdateAsync(int id, UpdateCategoryModel model)
        {

            var response = _mapper.Map<Category>(model);
            _context.Categories!.Update(response);
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
