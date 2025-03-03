namespace BlockSimApi.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Sender { get; set; } = null!;
        public string Receiver { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public int BlockId { get; set; }
    }
}
