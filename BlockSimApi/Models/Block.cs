using System.ComponentModel.DataAnnotations;

namespace BlockSimApi.Models
{
    public class Block
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; private set; } = DateTime.UtcNow;

        [MaxLength(64)]
        public string Hash { get; private set; } = null!;

        [MaxLength(64)]
        public string PreviousHash { get; private set; } = null!;

        public virtual ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();
    }
}
