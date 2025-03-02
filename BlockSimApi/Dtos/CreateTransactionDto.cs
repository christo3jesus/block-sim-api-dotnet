namespace BlockSimApi.Dtos
{
    public class CreateTransactionDto
    {
        public string Sender { get; set; } = null!;
        public string Receiver { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
