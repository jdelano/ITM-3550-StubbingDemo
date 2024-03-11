using System;
using System.Collections.Generic;

namespace StubbingDemoPrep2024.Database.Models;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
