using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swap.Business.DTOs
{
    public class CreateRatingDto
    {
        public int SwapRequestId { get; set; }
        public int RatedUserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}