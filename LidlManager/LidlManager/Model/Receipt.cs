using System;
using System.Collections.Generic;

namespace LidlManager.Model;

public partial class Receipt
{
    public int Id { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public double FloatTotalSum { get; set; }

    public int IdUser { get; set; }

    public bool IsActive { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<StockReceipt> StockReceipts { get; set; } = new List<StockReceipt>();
}
