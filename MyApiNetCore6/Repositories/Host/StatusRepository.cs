using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;

namespace MyApiNetCore6.Repositories.Host
{
    public class StatusRepository : IStatusRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StatusRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response> GetAllAsync()
        {
            var response = await _context.Statuses!.ToListAsync();
            //var data = _mapper.Map<List<CategoryModel>>(response);
            return (new Response
            {
                Success = true,
                Message = "Thành công",
                Data = response
            });
        }
    }
}
