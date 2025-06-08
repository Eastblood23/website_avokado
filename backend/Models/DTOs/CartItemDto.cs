using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTOs
{
    public class CartItemDto
    {
        [Required]
        public int AvocadoId { get; set; }
        
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; } = 1;
    }
    
    public class CartItemResponseDto
    {
        public int Id { get; set; }
        public int AvocadoId { get; set; }
        public string AvocadoName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
    
    public class CartResponseDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemResponseDto> Items { get; set; } = new List<CartItemResponseDto>();
        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }
    }
} 