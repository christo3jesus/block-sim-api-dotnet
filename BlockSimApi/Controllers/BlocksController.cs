using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlockSimApi.Context;
using BlockSimApi.Models;
using BlockSimApi.Dtos;

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
        public async Task<ActionResult<IEnumerable<BlockDto>>> GetBlocks()
        {
            var blocks = await _context.Blocks
                .OrderByDescending(b => b.Id)
                .Select(b => new BlockDto
                {
                    Id = b.Id,
                    Timestamp = b.Timestamp,
                    Sentence = b.Sentence,
                    Hash = b.Hash,
                    PreviousHash = b.PreviousHash,
                    TransactionCount = _context.Transactions.Count(t => t.BlockId == b.Id)
                })
                .ToListAsync();

            return Ok(blocks);
        }

        // GET: api/Blocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlockDto>> GetBlock(int id)
        {
            var block = await _context.Blocks
                .Where(b => b.Id == id)
                .Select(b => new BlockDto
                {
                    Id = b.Id,
                    Timestamp = b.Timestamp,
                    Sentence = b.Sentence,
                    Hash = b.Hash,
                    PreviousHash = b.PreviousHash,
                    TransactionCount = _context.Transactions.Count(t => t.BlockId == b.Id)
                })
                .FirstOrDefaultAsync();

            if (block == null)
            {
                return NotFound();
            }

            return Ok(block);
        }

        // POST: api/Blocks
        [HttpPost]
        public async Task<ActionResult<BlockDto>> CreateBlock(CreateBlockDto createBlockDto)
        {
            if (string.IsNullOrWhiteSpace(createBlockDto.Sentence))
            {
                return BadRequest("Empty is not allowed.");
            }

            var lastBlock = await _context.Blocks.FirstOrDefaultAsync();

            var newBlock = new Block(
                createBlockDto.Sentence,
                lastBlock?.Hash ?? "0".PadLeft(64, '0')
            );

            _context.Blocks.Add(newBlock);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlock), new { id = newBlock.Id }, new BlockDto
            {
                Id = newBlock.Id,
                Timestamp = newBlock.Timestamp,
                Sentence = newBlock.Sentence,
                Hash = newBlock.Hash,
                PreviousHash = newBlock.PreviousHash,
                TransactionCount = 0
            });
        }
    }
}
