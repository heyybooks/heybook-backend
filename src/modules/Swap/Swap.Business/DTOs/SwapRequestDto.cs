using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swap.Entity.Enums;

namespace Swap.Business.DTOs
{
    public class SwapRequestDto
    {
        public int RequestId { get; set; }
        public int RequesterId { get; set; }
        public int RequestedBookId { get; set; }
        public int OfferedBookId { get; set; }
        public SwapStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Notes { get; set; }

    }
}