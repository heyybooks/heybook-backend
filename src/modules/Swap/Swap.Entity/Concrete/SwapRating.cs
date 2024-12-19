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
    public class SwapRating
    {
        [Key] 
        public int RatingId { get; set; }
        public int SwapRequestId { get; set; }
        public int RatedByUserId { get; set; }
        public int RatedUserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

       
        public SwapRequest SwapRequest { get; set; }

        
        public User RatedByUser { get; set; }

      
        public User RatedUser { get; set; }
    }
}