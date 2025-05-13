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
        
    }
}