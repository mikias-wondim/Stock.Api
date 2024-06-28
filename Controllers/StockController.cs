using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.Dto.Stock;
using Stock.Api.Helper;
using Stock.Api.Interfaces;
using Stock.Api.Mappers;
using Stock.Api.Models;

namespace Stock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StockController(IStockRepository stockRepository) : ControllerBase
    {
        private readonly IStockRepository _stockRepository = stockRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] StockQueryObject query)
        {
            var stocks = await _stockRepository.GetAllAsync(query);
            var stocksDto = stocks.Select(s => s.ToStockDto()).ToList();
            return Ok(stocksDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound("Stock not found!");
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = stockDto.ToStockModelFromCreateDto();

            var createdStock = await _stockRepository.CreateAsync(stockModel);

            if (createdStock == null)
                return Problem("Stock create failed!");

            return Ok(createdStock.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepository.UpdateAsync(id, stockDto.ToStockModelFromUpdateDto());
            if (stock == null)
                return Problem();

            return Ok(stock.ToStockDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepository.DeleteAsync(id);

            if (stock == null)
                return Problem();

            return NoContent();
        }
    }
}
