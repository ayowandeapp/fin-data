using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHabit.Api.Extensions;
using DevHabit.Api.Interfaces;
using DevHabit.Api.Models;
using DevHabit.Api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevHabit.Api.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController(
        UserManager<AppUser> userManager,
        IStockRepository stockRepo,
        IPortfolioRepository portfolioRepo
    ) : ControllerBase
    {
        private readonly UserManager<AppUser> _usermanager = userManager;
        private readonly IStockRepository _stockRepo = stockRepo;
        private readonly IPortfolioRepository _portfolioRepo = portfolioRepo;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUSerPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _usermanager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);

            
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _usermanager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);
            if(stock == null) return BadRequest("Stock not found");

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            if(userPortfolio.Any(e=> e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Portfolio Exist");

            var portfolioModel = new Portfolio 
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };
            await _portfolioRepo.CreateAsync(portfolioModel);
            if(portfolioModel == null)
            {
                return StatusCode(500, "Cannot create");
            }
            return Created();
            
        }
        
    }
}