namespace BlockSimApi.Dtos
{
    public class BlockDto
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Sentence { get; set; } = null!;
        public string Hash { get; set; } = null!;
        public string PreviousHash { get; set; } = null!;
        public int TransactionCount { get; set; }
    }
}
