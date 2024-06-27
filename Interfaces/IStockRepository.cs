using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stock.Api.Helper;
using Stock.Api.Models;

namespace Stock.Api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<StockModel>> GetAllAsync(StockQueryObject query);
        Task<StockModel?> GetByIdAsync(int id);
        Task<StockModel> CreateAsync(StockModel model);
        Task<StockModel?> UpdateAsync(int id, StockModel model);
        Task<StockModel?> DeleteAsync(int id);
        Task<bool> IsStockExist(int id);
    }
}