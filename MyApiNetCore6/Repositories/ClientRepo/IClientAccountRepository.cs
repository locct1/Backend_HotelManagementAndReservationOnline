using MyApiNetCore6.Models;
using MyApiNetCore6.Models.ClientAuth;

namespace MyApiNetCore6.Repositories.ClientRepo
{
    public interface IClientAccountRepository
    {
        public Task<Response> SignUpAsync(SignUpClientModel model);
        public Task<ResponseToken> SignInAsync(SignInClientModel model);
        public Task<Response> GetInfoClientAsync(string Id);
        public Task<Response> UpdateAsync(UpdateClientModel updateClientModel);
    }
}
