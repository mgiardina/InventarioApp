﻿namespace InventoryApp.Api.Models;
public class Product
{
    public int ProductID { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }

    public ICollection<ProductCategory> ProductCategories { get; set; }
}
