using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHabit.Api.Models;

namespace DevHabit.Api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIDAsync(int id);
        // Task<Stock?> CreateAsync(Stock stockModel);
        // Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockDto);
        //  Task<Comment?> DeleteAsync(int id);

    }
}