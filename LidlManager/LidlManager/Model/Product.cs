using System;
using System.Collections.Generic;

namespace LidlManager.Model;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Barcode { get; set; } = null!;

    public int IdCategory { get; set; }

    public int IdProducer { get; set; }

    public bool IsActive { get; set; }

    public virtual Category IdCategoryNavigation { get; set; } = null!;

    public virtual Producer IdProducerNavigation { get; set; } = null!;

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
