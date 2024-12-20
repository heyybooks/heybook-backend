using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swap.Business.DTOs
{
    public class RatingDto
    {
        public int RatingId { get; set; }
        public int SwapRequestId { get; set; }
        public int RatedByUserId { get; set; }
        public int RatedUserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RatedByUserName { get; set; }
        public string RatedUserName { get; set; }
    }
}