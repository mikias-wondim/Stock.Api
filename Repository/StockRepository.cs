using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stock.Api.Data;
using Stock.Api.Helper;
using Stock.Api.Interfaces;
using Stock.Api.Models;

namespace Stock.Api.Repository
{
    public class StockRepository(ApplicationDbContext context) : IStockRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<StockModel> CreateAsync(StockModel stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<StockModel?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(stock => stock.Id == id);

            if (stock == null)
                return null;

            _context.Remove(stock);
            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<List<StockModel>> GetAllAsync(StockQueryObject query)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol)); ;
            }

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (string.Equals("Symbol", query.SortBy, StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
                else if (string.Equals("CompanyName", query.SortBy, StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.CompanyName) : stocks.OrderBy(s => s.CompanyName);
                }
            }

            var skip = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skip).Take(query.PageSize).ToListAsync();
        }

        public async Task<StockModel?> GetByIdAsync(int id)
        {
            var stock = await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return null;
            }

            return stock;
        }

        public async Task<bool> IsStockExist(int id)
        {
            return await _context.Stocks.AnyAsync(c => c.Id == id);
        }

        public async Task<StockModel?> UpdateAsync(int id, StockModel stock)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (existingStock == null)
            {
                return null;
            }

            existingStock.Symbol = stock.Symbol;
            existingStock.CompanyName = stock.CompanyName;
            existingStock.Purchase = stock.Purchase;
            existingStock.LastDiv = stock.LastDiv;
            existingStock.Industry = stock.Industry;
            existingStock.MarketCap = stock.MarketCap;

            await _context.SaveChangesAsync();

            return existingStock;
        }
    }
}