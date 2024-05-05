using System;
using System.Collections.Generic;

namespace LidlManager.Model;

public partial class ProductReceipt
{
    public int Id { get; set; }

    public int IdProduct { get; set; }

    public int IdReceipt { get; set; }

    public double Amount { get; set; }

    public double Subtotal { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual Receipt IdReceiptNavigation { get; set; } = null!;
}
