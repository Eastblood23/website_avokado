using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvocadosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AvocadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Avocados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Avocado>>> GetAvocados()
        {
            return await _context.Avocados.ToListAsync();
        }

        // GET: api/Avocados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Avocado>> GetAvocado(int id)
        {
            var avocado = await _context.Avocados.FindAsync(id);

            if (avocado == null)
            {
                return NotFound();
            }

            return avocado;
        }

        // GET: api/Avocados/types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<string>>> GetAvocadoTypes()
        {
            return await _context.Avocados
                .Select(a => a.Type)
                .Distinct()
                .ToListAsync();
        }

        // GET: api/Avocados/type/Hass
        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<Avocado>>> GetAvocadosByType(string type)
        {
            return await _context.Avocados
                .Where(a => a.Type == type)
                .ToListAsync();
        }
    }
} 