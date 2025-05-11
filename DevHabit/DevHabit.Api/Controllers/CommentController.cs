using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHabit.Api.Dtos.Comment;
using DevHabit.Api.Interfaces;
using DevHabit.Api.Mappers;
using DevHabit.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DevHabit.Api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController(ICommentRepository commentRepository, IStockRepository stockRepository) : ControllerBase
    {
        public readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IStockRepository _stockRepository = stockRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentRepository.GetByIDAsync(id);
            if (comment == null)
            {
                return NotFound("Comment not Found");
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        [Route("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentRequestDto createCommentDto)
        {
            if (!await _stockRepository.StockExists(stockId))
            {
                return BadRequest("stock does not exist");
            }
            var commentModel = createCommentDto.ToCommentFromCommentDto(stockId);
            await _commentRepository.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            var commentModel = await _commentRepository.UpdateAsync(id, updateDto);
            if (commentModel == null)
            {
                return NotFound("Comment not Found");
            }
            return Ok(commentModel?.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var commentModel = await _commentRepository.DeleteAsync(id);
            if (commentModel == null)
            {
                return NotFound("Comment not Found");
            }
            return Ok(commentModel);
        }

    }
}