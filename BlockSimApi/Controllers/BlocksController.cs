using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlockSimApi.Context;
using BlockSimApi.Models;

namespace BlockSimApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlocksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlocksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Blocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Block>>> GetBlocks()
        {
            return await _context.Blocks.ToListAsync();
        }

        // GET: api/Blocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Block>> GetBlock(int id)
        {
            var block = await _context.Blocks.FindAsync(id);

            if (block == null)
            {
                return NotFound();
            }

            return block;
        }

        // POST: api/Blocks
        [HttpPost]
        public async Task<ActionResult<Block>> PostBlock(Block block)
        {
            _context.Blocks.Add(block);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlock), new { id = block.Id }, block);
        }

        private bool BlockExists(int id)
        {
            return _context.Blocks.Any(e => e.Id == id);
        }
    }
}
