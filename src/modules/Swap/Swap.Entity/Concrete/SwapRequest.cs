using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Entity.Concrete;
using Books.Entity.Concrete;
using Swap.Entity.Enums; 
using Core.Entities; 
using System.ComponentModel.DataAnnotations;

namespace Swap.Entity.Concrete
{
    public class SwapRequest : IEntity
    {
        [Key] 
        public int RequestId { get; set; }

        [Required]
        public int RequesterId { get; set; }

        [Required]
        public int RequestedBookId { get; set; }

        [Required]
        public int OfferedBookId { get; set; }
        public SwapStatus Status { get; set; } = SwapStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        
        public User Requester { get; set; }

        
        public Book RequestedBook { get; set; }

        
        public Book OfferedBook { get; set; }
    }
}