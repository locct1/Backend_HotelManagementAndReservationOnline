using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.CategoryService;

namespace MyApiNetCore6.Repositories.Host
{
    public interface ICategoryRepository
    {
        public Task<Response> GetAllAsync(int hotelId);
        public Task<Response> GetAsync(int id);
        public Task<Response> AddAsync(AddCategoryModel model);
        public Task<Response> UpdateAsync(int id, UpdateCategoryModel model);
        public Task<Response> DeleteAsync(int id);
    }
}
