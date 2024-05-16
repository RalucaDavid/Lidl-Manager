using System;
using System.Collections.Generic;

namespace LidlManager.Model;

public partial class StockReceipt
{
    public int Id { get; set; }

    public int IdStock { get; set; }

    public int IdReceipt { get; set; }

    public double Amount { get; set; }

    public double Subtotal { get; set; }

    public virtual Receipt IdReceiptNavigation { get; set; } = null!;

    public virtual Stock IdStockNavigation { get; set; } = null!;
}
