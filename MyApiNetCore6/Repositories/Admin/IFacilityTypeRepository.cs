﻿using MyApiNetCore6.Models.BedType;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.TypeFacility;
using MyApiNetCore6.Models.Facility;

namespace MyApiNetCore6.Repositories.Admin
{
    public interface IFacilityTypeRepository
    {
        public Task<Response> GetAllAsync();
        public Task<Response> GetAsync(int id);
        public Task<Response> AddAsync(AddFacilityTypeModel model);
        public Task<Response> UpdateAsync(int id, UpdateFacilityTypeModel model);
        public Task<Response> DeleteAsync(int id);

    }
}
