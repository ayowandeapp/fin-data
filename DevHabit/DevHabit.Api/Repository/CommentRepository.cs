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
    public class CommentRepository(ApplicationDBContext context) : ICommentRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();  
        }

        public async Task<Comment?> GetByIDAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
           
            return comment;
        }
    }
}