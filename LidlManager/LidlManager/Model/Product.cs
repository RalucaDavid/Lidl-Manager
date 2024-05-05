using System;
using System.Collections.Generic;

namespace LidlManager.Model;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Barcode { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public string Category { get; set; } = null!;

    public int IdProducer { get; set; }

    public virtual Producer IdProducerNavigation { get; set; } = null!;

    public virtual ICollection<ProductReceipt> ProductReceipts { get; set; } = new List<ProductReceipt>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
