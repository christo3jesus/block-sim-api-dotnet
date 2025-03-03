using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlockSimApi.Context;
using BlockSimApi.Models;
using BlockSimApi.Dtos;

namespace BlockSimApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions()
        {
            var transactions = await _context.Transactions
                .OrderByDescending(t => t.Id)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    Sender = t.Sender,
                    Receiver = t.Receiver,
                    Amount = t.Amount,
                    Timestamp = t.Timestamp,
                    BlockId = t.BlockId,
                })
                .ToListAsync();

            return Ok(transactions);
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions
                .Where(t => t.Id == id)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    Sender = t.Sender,
                    Receiver = t.Receiver,
                    Amount = t.Amount,
                    Timestamp = t.Timestamp,
                    BlockId = t.BlockId,
                })
                .FirstOrDefaultAsync();

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // POST: api/Transactions
        [HttpPost]
        public async Task<ActionResult<TransactionDto>> CreateTransaction(CreateTransactionDto createTransactionDto)
        {
            if (string.IsNullOrWhiteSpace(createTransactionDto.Sender) || string.IsNullOrWhiteSpace(createTransactionDto.Receiver))
            {
                return BadRequest("The sender and receiver cannot be empty.");
            }

            if (createTransactionDto.Amount <= 0)
            {
                return BadRequest("The amount must be greater than 0.");
            }

            var lastBlock = await _context.Blocks
                .OrderByDescending(b => b.Id)
                .FirstOrDefaultAsync();

            if (lastBlock == null)
            {
                return BadRequest("No blocks are available to add transactions.");
            }

            var transaction = new Transaction
            {
                Sender = createTransactionDto.Sender,
                Receiver = createTransactionDto.Receiver,
                Amount = createTransactionDto.Amount,
                BlockId = lastBlock.Id,
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, new TransactionDto
            {
                Id = transaction.Id,
                Sender = transaction.Sender,
                Receiver = transaction.Receiver,
                Amount = transaction.Amount,
                Timestamp = transaction.Timestamp,
                BlockId = transaction.BlockId
            });
        }

        // GET: api/Transactions/Blocks/5
        [HttpGet("Blocks/{blockId}")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactionsByBlock(int blockId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.BlockId == blockId)
                .OrderByDescending(t => t.Id)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    Sender = t.Sender,
                    Receiver = t.Receiver,
                    Amount = t.Amount,
                    Timestamp = t.Timestamp,
                    BlockId = blockId
                })
                .ToListAsync();

            if (!transactions.Any())
            {
                return NotFound($"No transactions for block with ID {blockId}.");
            }

            return Ok(transactions);
        }
    }
}
