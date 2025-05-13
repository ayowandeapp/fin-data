using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHabit.Api.Data;
using DevHabit.Api.Interfaces;
using DevHabit.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DevHabit.Api.Repository
{
    public class PortfolioRepository(ApplicationDBContext context) : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio?> DeletePortfolio(AppUser appUser, string symbol)
        {
            var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(c => c.AppUserId == appUser.Id && c.Stock.Symbol.ToLower() == symbol.ToLower());
            if(portfolioModel == null)
            {
                return null;
            }
            _context.Portfolios.Remove(portfolioModel);
            _context.SaveChanges();
            return portfolioModel;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
                .Select(s => new Stock
                {
                    Id = s.StockId,
                    Symbol = s.Stock.Symbol,
                    CompanyName = s.Stock.CompanyName,
                    Purchase = s.Stock.Purchase,
                    LastDiv = s.Stock.LastDiv,
                    Industry = s.Stock.Industry,
                    MarketCap = s.Stock.MarketCap
                }).ToListAsync();
        }
    }
}