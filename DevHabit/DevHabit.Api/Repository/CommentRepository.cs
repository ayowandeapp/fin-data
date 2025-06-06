using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHabit.Api.Data;
using DevHabit.Api.Dtos.Comment;
using DevHabit.Api.Interfaces;
using DevHabit.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DevHabit.Api.Repository
{
    public class CommentRepository(ApplicationDBContext context) : ICommentRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(c=>c.AppUser).ToListAsync();  
        }

        public async Task<Comment?> GetByIDAsync(int id)
        {
            var comment = await _context.Comments.Include(c=>c.AppUser).FirstOrDefaultAsync(x => x.Id == id);
           
            return comment;
        }
        
        public async Task<Comment?> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateCommentDto)
        {
            var comment = await _context.Comments.Include(c=>c.AppUser).FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return null;
            }
            comment.Title = updateCommentDto.Title;
            comment.Content = updateCommentDto.Content;
           
            await _context.SaveChangesAsync();
            return comment;
        }

        public async  Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return null;
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}