using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
using DevHabit.Api.Data;
using DevHabit.Api.Mappers;
using DevHabit.Api.Dtos.Stock;

namespace DevHabit.Api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stock = _context.Stocks.ToList()
            // .Select(s => StockMappers.ToStockDto(s))
            .Select(s=> s.ToStockDto());
            return Ok(stock);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var stock = _context.Stocks.Find(id);
            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());

        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            // call the GetById() and pass the id and return toStockDto
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id}, stockModel.ToStockDto());


        }
        
    }
}