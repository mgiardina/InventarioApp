using InventoryApp.Api.Models;

namespace InventoryApp.Api.Configuration;

public static class DataSeeder
{
    public static void SeedProducts(ApplicationDbContext context)
    {
        if (!context.Products.Any())
        {
            var electronicsCategory = new Category { Name = "Electrónica" };
            var booksCategory = new Category { Name = "Libros" };
            var furnitureCategory = new Category { Name = "Muebles" };

            var products = new List<Product>
            {
                new Product { Name = "Laptop", Description = "Laptop de alta gama", Image = "laptop.png" },
                new Product { Name = "Smartphone", Description = "Modelo más reciente", Image = "smartphone.png" },
                new Product { Name = "Auriculares", Description = "Auriculares con cancelación de ruido", Image = "headphones.png" },
                new Product { Name = "Estantería", Description = "Estantería de madera", Image = "bookshelf.png" },
                new Product { Name = "Libro de Programación", Description = "Libro de programación en C#", Image = "book.png" }
            };

            var productCategories = new List<ProductCategory>
            {
                new ProductCategory { Product = products[0], Category = electronicsCategory },
                new ProductCategory { Product = products[1], Category = electronicsCategory },
                new ProductCategory { Product = products[2], Category = electronicsCategory },
                new ProductCategory { Product = products[2], Category = furnitureCategory },
                new ProductCategory { Product = products[3], Category = furnitureCategory },
                new ProductCategory { Product = products[4], Category = booksCategory },
                new ProductCategory { Product = products[4], Category = electronicsCategory }
            };

            context.Categories.AddRange(electronicsCategory, booksCategory, furnitureCategory);
            context.Products.AddRange(products);
            context.ProductCategories.AddRange(productCategories);

            context.SaveChanges();

            Console.WriteLine("Productos y categorías agregados correctamente.");
        }
    }
}
