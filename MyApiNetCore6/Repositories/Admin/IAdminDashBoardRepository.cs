using MyApiNetCore6.Models;

namespace MyApiNetCore6.Repositories.Admin
{
    public interface IAdminDashBoardRepository
    {
        public Task<Response> GetAllAsync();
    }
}
