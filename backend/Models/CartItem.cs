using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        
        [Required]
        public int CartId { get; set; }
        
        [ForeignKey("CartId")]
        public Cart Cart { get; set; }
        
        [Required]
        public int AvocadoId { get; set; }
        
        [ForeignKey("AvocadoId")]
        public Avocado Avocado { get; set; }
        
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; } = 1;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
} 