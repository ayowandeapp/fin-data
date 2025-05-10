using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DevHabit.Api.Models;
using DevHabit.Api.Dtos.Stock;

namespace DevHabit.Api.Mappers
{
    public static class StockMappers 
    {
        // "add" methods to existing types, ToStockDto() is an extension for the Stock class
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
            };
        }
        
    }
}