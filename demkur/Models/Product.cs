using System;
using System.Collections.Generic;

namespace demkur.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ArticleNumber { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string UnitOfMeasure { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal MaxDiscount { get; set; }

    public decimal CurrentDiscount { get; set; }

    public int QuantityInStock { get; set; }

    public string? Description { get; set; }

    public string? ImagePath { get; set; }

    public int CategoryId { get; set; }

    public int ManufacturerId { get; set; }

    public int SupplierId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
