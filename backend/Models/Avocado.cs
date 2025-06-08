using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Avocado
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [Range(0.01, 1000.00)]
        public decimal Price { get; set; }
        
        public string? ImageUrl { get; set; }
        
        [StringLength(50)]
        public string? Type { get; set; }
        
        [StringLength(2000)]
        public string? LongDescription { get; set; }
        
        public bool IsAvailable { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
} 