﻿using Marketing.Domain.Models;
using Marketing.Domain.Models.Data;

namespace Marketing.API.Interfaces
{
    public interface IPlatesHandler
    {
        public Task<IEnumerable<Plate>> GetPaginationPlates(int pageNumber, int pageSize);

        public Task<IEnumerable<Plate>> GetFilteredPlates(string letters);
    }
}