using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Swap.Business.DTOs
{
    public class CreateSwapRequestDto
    {
        [Required]
        public int RequesterId { get; set; }

        [Required]
        public int RequestedBookId { get; set; }

        [Required]
        public int OfferedBookId { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}