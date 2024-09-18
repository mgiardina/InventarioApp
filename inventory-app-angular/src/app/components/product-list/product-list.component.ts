import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [MatTableModule, MatButton],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products = [];
  displayedColumns: string[] = ['productID', 'name', 'description', 'actions'];

  constructor(private productService: ProductService, private router: Router) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe((data: any) => {
      this.products = data;
    });
  }

  addProduct(): void {
    this.router.navigate(['/add-product']);
  }

  editProduct(id: number): void {
    this.router.navigate(['/edit-product', id]);
  }

  deleteProduct(id: number): void {
    if (confirm('Confirma?')) {
      this.productService.deleteProduct(id).subscribe(() => {
        this.loadProducts();
      });
    }
  }
}
