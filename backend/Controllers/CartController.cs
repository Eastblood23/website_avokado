using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.Models.DTOs;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cart
        [HttpGet]
        public async Task<ActionResult<CartResponseDto>> GetCart()
        {
            const string userId = "anonymous"; // Şimdilik sabit kullanıcı ID kullanıyoruz

            // Kullanıcının sepetini bul veya oluştur
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Avocado)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                // Sepet yoksa oluştur
                return new CartResponseDto
                {
                    UserId = userId,
                    Items = new List<CartItemResponseDto>(),
                    TotalPrice = 0,
                    TotalItems = 0
                };
            }

            // Sepet varsa DTO'ya dönüştür
            var cartDto = new CartResponseDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = cart.Items.Select(item => new CartItemResponseDto
                {
                    Id = item.Id,
                    AvocadoId = item.AvocadoId,
                    AvocadoName = item.Avocado.Name,
                    ImageUrl = item.Avocado.ImageUrl,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    TotalPrice = item.Price * item.Quantity
                }).ToList(),
                TotalPrice = cart.Items.Sum(item => item.Price * item.Quantity),
                TotalItems = cart.Items.Sum(item => item.Quantity)
            };

            return cartDto;
        }

        // POST: api/Cart
        [HttpPost]
        public async Task<ActionResult<CartResponseDto>> AddToCart(CartItemDto itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            const string userId = "anonymous"; // Şimdilik sabit kullanıcı ID kullanıyoruz

            // Avokado ürününü kontrol et
            var avocado = await _context.Avocados.FindAsync(itemDto.AvocadoId);
            if (avocado == null)
            {
                return NotFound($"Avocado with ID {itemDto.AvocadoId} not found");
            }

            // Kullanıcının sepetini bul veya oluştur
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                // Sepet yoksa yeni sepet oluştur
                cart = new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            // Sepette aynı ürün var mı kontrol et
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(i => i.CartId == cart.Id && i.AvocadoId == itemDto.AvocadoId);

            if (existingItem != null)
            {
                // Varsa miktarı güncelle
                existingItem.Quantity += itemDto.Quantity;
                _context.Entry(existingItem).State = EntityState.Modified;
            }
            else
            {
                // Yoksa yeni ürün ekle
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    AvocadoId = itemDto.AvocadoId,
                    Quantity = itemDto.Quantity,
                    Price = avocado.Price,
                    CreatedAt = DateTime.UtcNow
                };
                _context.CartItems.Add(newItem);
            }

            // Sepet güncelleme tarihini güncelle
            cart.UpdatedAt = DateTime.UtcNow;
            _context.Entry(cart).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            // Güncellenmiş sepeti getir
            return await GetCart();
        }

        // PUT: api/Cart/items/5
        [HttpPut("items/{id}")]
        public async Task<ActionResult<CartResponseDto>> UpdateCartItem(int id, CartItemDto itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            const string userId = "anonymous"; // Şimdilik sabit kullanıcı ID kullanıyoruz

            // Sepet öğesini bul
            var cartItem = await _context.CartItems
                .Include(i => i.Cart)
                .FirstOrDefaultAsync(i => i.Id == id && i.Cart.UserId == userId);

            if (cartItem == null)
            {
                return NotFound($"Cart item with ID {id} not found");
            }

            // Miktar güncelleme
            if (itemDto.Quantity <= 0)
            {
                // Miktar 0 veya negatifse öğeyi sil
                _context.CartItems.Remove(cartItem);
            }
            else
            {
                // Miktarı güncelle
                cartItem.Quantity = itemDto.Quantity;
                _context.Entry(cartItem).State = EntityState.Modified;
            }

            // Sepet güncelleme tarihini güncelle
            cartItem.Cart.UpdatedAt = DateTime.UtcNow;
            _context.Entry(cartItem.Cart).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            // Güncellenmiş sepeti getir
            return await GetCart();
        }

        // DELETE: api/Cart/items/5
        [HttpDelete("items/{id}")]
        public async Task<ActionResult<CartResponseDto>> RemoveCartItem(int id)
        {
            const string userId = "anonymous"; // Şimdilik sabit kullanıcı ID kullanıyoruz

            // Sepet öğesini bul
            var cartItem = await _context.CartItems
                .Include(i => i.Cart)
                .FirstOrDefaultAsync(i => i.Id == id && i.Cart.UserId == userId);

            if (cartItem == null)
            {
                return NotFound($"Cart item with ID {id} not found");
            }

            // Öğeyi sil
            _context.CartItems.Remove(cartItem);

            // Sepet güncelleme tarihini güncelle
            cartItem.Cart.UpdatedAt = DateTime.UtcNow;
            _context.Entry(cartItem.Cart).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            // Güncellenmiş sepeti getir
            return await GetCart();
        }

        // DELETE: api/Cart
        [HttpDelete]
        public async Task<ActionResult<CartResponseDto>> ClearCart()
        {
            const string userId = "anonymous"; // Şimdilik sabit kullanıcı ID kullanıyoruz

            // Kullanıcının sepetini bul
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.Items.Any())
            {
                // Sepet yoksa veya boşsa boş sepet döndür
                return await GetCart();
            }

            // Sepet öğelerini temizle
            _context.CartItems.RemoveRange(cart.Items);

            // Sepet güncelleme tarihini güncelle
            cart.UpdatedAt = DateTime.UtcNow;
            _context.Entry(cart).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            // Güncellenmiş sepeti getir
            return await GetCart();
        }
    }
} 