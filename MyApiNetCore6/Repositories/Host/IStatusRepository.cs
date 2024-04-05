using MyApiNetCore6.Models;

namespace MyApiNetCore6.Repositories.Host
{
    public interface IStatusRepository
    {
        public Task<Response> GetAllAsync();

    }
}
