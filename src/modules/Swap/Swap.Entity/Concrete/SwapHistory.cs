using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using UserManagement.Entity.Concrete;
using Books.Entity.Concrete;
using Swap.Entity.Enums; 
using System.ComponentModel.DataAnnotations;


namespace Swap.Entity.Concrete
{
    public class SwapHistory : IEntity
    {
        [Key]        
        public int HistoryId { get; set; }
        public int SwapRequestId { get; set; }
        public SwapStatus StatusFrom { get; set; }
        public SwapStatus StatusTo { get; set; }
        public int ChangedByUserId { get; set; }
        public DateTime ChangedAt { get; set; }
        public string Notes { get; set; }

        // Navigation properties
        public SwapRequest SwapRequest { get; set; }
        public User ChangedByUser { get; set; }
    }
}