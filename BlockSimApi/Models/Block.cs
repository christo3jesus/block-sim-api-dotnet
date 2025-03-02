using System.ComponentModel.DataAnnotations;

namespace BlockSimApi.Models
{
    public class Block
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; private set; } = DateTime.UtcNow;

        [Required, MaxLength(256)]
        public string Sentence { get; set; } = null!;

        [Required, MaxLength(64)]
        public string Hash { get; set; } = null!;

        [Required, MaxLength(64)]
        public string PreviousHash { get; set; } = null!;

        public virtual ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

        public int TransactionCount => Transactions.Count;

        public Block(string sentence, string previousHash)
        {
            Sentence = sentence;
            PreviousHash = previousHash;
            Hash = GenerateHash(sentence, previousHash);
            Timestamp = DateTime.UtcNow;
        }

        private string GenerateHash(string sentence, string previousHash)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var rawData = $"{previousHash}{sentence}{Timestamp}";
                var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawData));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
