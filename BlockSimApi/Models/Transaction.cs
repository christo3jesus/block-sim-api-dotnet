﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlockSimApi.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required, MaxLength(42)]
        public string Sender { get; set; } = null!;

        [Required, MaxLength(42)]
        public string Receiver { get; set; } = null!;

        [Precision(18, 4)]
        public decimal Amount { get; set; }

        public DateTime Timestamp { get; private set; } = DateTime.UtcNow;

        public int BlockId { get; set; }

        public virtual Block Block { get; set; } = null!;
    }
}
