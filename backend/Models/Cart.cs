using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Cart
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = "anonymous"; // Şimdilik anonim kullanıcılar için
        
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
} 