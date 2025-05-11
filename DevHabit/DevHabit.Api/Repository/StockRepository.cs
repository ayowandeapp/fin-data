using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHabit.Api.Data;
using DevHabit.Api.Dtos.Stock;
using DevHabit.Api.Interfaces;
using DevHabit.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DevHabit.Api.Repository
{
    public class StockRepository(ApplicationDBContext context) : IStockRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Stock?> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> GetByIDAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockDto)
        {
            
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            stockModel.Symbol = updateStockDto.Symbol;
            stockModel.CompanyName = updateStockDto.CompanyName;
            stockModel.Purchase = updateStockDto.Purchase;
            stockModel.LastDiv = updateStockDto.LastDiv;
            stockModel.Industry = updateStockDto.Industry;
           
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(s=> s.Id == id);
        }
    }
}