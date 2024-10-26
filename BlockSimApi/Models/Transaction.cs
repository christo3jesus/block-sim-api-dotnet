using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlockSimApi.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [MaxLength(42)]
        public string Sender { get; set; } = null!;

        [MaxLength(42)]
        public string Receiver { get; set; } = null!;

        [Precision(18, 4)]
        public decimal Amount { get; set; }

        public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
    }
}
