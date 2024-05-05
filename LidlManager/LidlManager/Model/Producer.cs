using System;
using System.Collections.Generic;

namespace LidlManager.Model;

public partial class Producer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? CountryOfOrigin { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
