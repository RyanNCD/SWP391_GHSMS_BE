using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public decimal Amount { get; set; }

    public string Method { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? TransactionTime { get; set; }

    public int? WalletId { get; set; }

    public virtual Ewallet? Wallet { get; set; }
}
