namespace InventoryApp.Api.Models;
public class ProductDto
{
    public int ProductID { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public List<CategoryDto> Categories { get; set; }
}
