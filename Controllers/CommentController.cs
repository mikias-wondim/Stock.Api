using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.Dto.Comment;
using Stock.Api.Interfaces;
using Stock.Api.Mappers;

namespace Stock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController(ICommentRepository commentRepository, IStockRepository stockRepository) : ControllerBase
    {
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IStockRepository _stockRepository = stockRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentsDto = comments.Select(c => c.ToCommentDtoFromModel());

            return Ok(commentsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
                return NotFound("Comment Not Found");

            return Ok(comment);
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create(int stockId, [FromBody] CreateCommentRequestDto commentDto)
        {

            Console.WriteLine($"The stock Id is: {stockId}");

            if (!await _stockRepository.IsStockExist(stockId))
            {
                return BadRequest("Stock doesn't exist");
            }

            var comment = await _commentRepository.CreateAsync(commentDto.ToCommentFromCreateDto(stockId));
            if (comment == null)
            {
                return Problem("Comment create failed!");
            }

            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDtoFromModel());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            var comment = await _commentRepository.UpdateAsync(id, commentDto.ToCommentFromUpdateDto());
            if (comment == null)
                return NotFound("Comment not found!");

            return Ok(comment.ToCommentDtoFromModel());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepository.DeleteAsync(id);
            if (comment == null)
                return NotFound("Comment not found!");

            return NoContent();
        }
    }
}