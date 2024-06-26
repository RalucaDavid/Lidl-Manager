﻿using System;
using System.Collections.Generic;

namespace LidlManager.Model;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Type { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}
