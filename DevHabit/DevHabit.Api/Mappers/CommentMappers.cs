using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHabit.Api.Dtos.Comment;
using DevHabit.Api.Models;

namespace DevHabit.Api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId,
                CreatedBy = commentModel.AppUser.UserName
            };

        }

        public static Comment ToCommentFromCommentDto(this CreateCommentRequestDto commentDto, int stockId)
        {
            return new Comment 
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }
        
        // public static Comment ToCommentFromUpdateCommentDto(this UpdateCommentRequestDto commentDto)
        // {
        //     return new Comment 
        //     {
        //         Title = commentDto.Title,
        //         Content = commentDto.Content,
        //     };
        // }
        
    }
}