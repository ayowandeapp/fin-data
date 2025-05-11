using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevHabit.Api.Data;
using DevHabit.Api.Mappers;
using DevHabit.Api.Dtos.Stock;
using DevHabit.Api.Interfaces;

namespace DevHabit.Api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAllAsync();
            // .Select(s => StockMappers.ToStockDto(s))
            var stockDto = stocks.Select(s => s.ToStockDto());
            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var stock = await _stockRepository.GetByIDAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockRepository.CreateAsync(stockModel);
            // call the GetById() and pass the id and return toStockDto
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockModel = await _stockRepository.UpdateAsync(id, updateStockDto);
            if (stockModel == null)
            {
                return NotFound("Comment not Found");
            }
            return Ok(stockModel?.ToStockDto());
        }        

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockModel = await _stockRepository.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }

            // call the GetById() and pass the id and return toStockDto
            return NoContent();
        }
    }
}