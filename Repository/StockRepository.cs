using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stock.Api.Interfaces;

namespace Stock.Api.Repository
{
    public class StockRepository : IStockRepository
    {
        public Task<Stock> CreateAsync(Stock item)
        {
            throw new NotImplementedException();
        }

        public Task<Stock> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Stock>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Stock> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Stock> UpdateAsync(int id, Stock item)
        {
            throw new NotImplementedException();
        }
    }
}