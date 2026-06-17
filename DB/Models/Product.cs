using System;
using System.Collections.Generic;

namespace DB.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Article { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public short Discount { get; set; }

    public string Unit { get; set; } = null!;

    public int Quantity { get; set; }

    public short? Size { get; set; }

    public string? Color { get; set; }

    public string Description { get; set; } = null!;

    public int? ManufacturerId { get; set; }

    public int? SupplierId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
