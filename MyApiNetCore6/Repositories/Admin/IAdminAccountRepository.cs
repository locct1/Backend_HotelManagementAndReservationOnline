using MyApiNetCore6.Models.HostAuth;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.AdminAuth;

namespace MyApiNetCore6.Repositories.Admin
{
    public interface IAdminAccountRepository
    {
        public Task<ResponseToken> SignInAsync(SignInAdminModel model);
        public Task<Response> GetInfoHostAsync(string Id);
    }
}
