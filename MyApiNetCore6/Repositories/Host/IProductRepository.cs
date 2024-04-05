using MyApiNetCore6.Models.CategoryService;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Service;

namespace MyApiNetCore6.Repositories.Host
{
    public interface IProductRepository
    {
        public Task<Response> GetAllAsync(int hotelId);
        public Task<Response> GetAsync(int id);
        public Task<Response> AddAsync(AddProductModel model);
        public Task<Response> UpdateAsync(int id, UpdateProductModel model);
        public Task<Response> DeleteAsync(int id);
    }
}
