using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHabit.Api.Interfaces;
using DevHabit.Api.Mappers;
using DevHabit.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DevHabit.Api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController(ICommentRepository commentRepository) : ControllerBase
    {
        public readonly ICommentRepository _commentRepository = commentRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentDto = comments.Select(c=>c.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentRepository.GetByIDAsync(id);
             if(comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

    }
}