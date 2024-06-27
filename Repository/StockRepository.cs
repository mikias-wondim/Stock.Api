using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stock.Api.Data;
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
            var stock = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if (stock == null)
                return null;

            _context.Remove(stock);
            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<List<StockModel>> GetAllAsync()
        {
            var stocks = await _context.Stocks.ToListAsync();

            return stocks;
        }

        public async Task<StockModel?> GetByIdAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return null;
            }

            return stock;
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