using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swap.Business.DTOs
{
    public class CreateSwapRequestDto
    {
        public int RequestedBookId { get; set; }
        public int OfferedBookId { get; set; }
        public string Notes { get; set; }
    }
}