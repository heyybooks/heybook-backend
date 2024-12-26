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
    public class SwapRating : IEntity
    {
        [Key] 
        public int RatingId { get; set; }
        public int SwapRequestId { get; set; }
        public int RatedByUserId { get; set; }
        public int RatedUserId { get; set; }
        
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }  = DateTime.UtcNow;
        
        // Navigation properties 
        public virtual SwapRequest SwapRequest { get; set; }

        
        public virtual User RatedByUser { get; set; }

      
        public virtual User RatedUser { get; set; }
    }
}