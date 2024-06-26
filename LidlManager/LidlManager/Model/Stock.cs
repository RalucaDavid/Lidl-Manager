﻿using System;
using System.Collections.Generic;

namespace LidlManager.Model;

public partial class Stock
{
    public int Id { get; set; }

    public double Amount { get; set; }

    public string Unit { get; set; } = null!;

    public DateOnly? ExpirationDate { get; set; }

    public DateOnly SupplyDate { get; set; }

    public double PuchasePrice { get; set; }

    public double SellingPrice { get; set; }

    public int IdProduct { get; set; }

    public bool IsActive { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual ICollection<StockReceipt> StockReceipts { get; set; } = new List<StockReceipt>();
}
