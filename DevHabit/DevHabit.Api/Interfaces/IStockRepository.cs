using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHabit.Api.Dtos.Stock;
using DevHabit.Api.Helpers;
using DevHabit.Api.Models;

namespace DevHabit.Api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIDAsync(int id);
        Task<Stock?> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockDto);
         Task<Stock?> DeleteAsync(int id);
         Task<bool> StockExists(int id);
    }
}