using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevHabit.Api.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage ="Title must be 5 characters")]
        [MaxLength(200, ErrorMessage ="Title must be less than or equal to 200 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage ="Content must be 5 characters")]
        [MaxLength(200, ErrorMessage ="Content must be less than or equal to 200 characters")]
        public string Content { get; set; } = string.Empty;
    }
}